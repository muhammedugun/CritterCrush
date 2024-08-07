#if UNITY_ANDROID 
using GoogleMobileAds.Api; 
#endif
using Ricimi;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LivesShopPopup : MonoBehaviour
{
    [SerializeField] private GameObject _clockPrefab;
    [SerializeField] private RuntimeAnimatorController _pulseAC;
    [SerializeField] private Sprite _lifeSprite;
    [SerializeField] private GameObject _lives;

    [SerializeField] private Text _buyButtonText, _timeText;
    [SerializeField] private PopupOpener _starShopPopupOpener;

    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;
    [SerializeField] private Image _notificationImage;
    [SerializeField] private Slider _notificationSlider;

    private bool isInit = true;
    private void Start()
    {
        LifeManager.LifeLoadControl();
#if UNITY_ANDROID
        if(AndroidNotifications.CheckSendNotifications())
        {
            _notificationSlider.value = 1;
        }
        else
        {
            _notificationSlider.value = 0;
        }
#endif
        UpdateLiveCountUI();

        EventBus.Subscribe(EventType.LifeCountChanged, UpdateLiveCountUI);
    }

    private void Update()
    {
        if (LifeManager.GetLifeCount() < 5)
        {
            int remainingMinutes = LifeManager.GetRemainingMinutes();
            int remainingSeconds = LifeManager.GetRemainingSeconds();

            if (remainingMinutes >= 0 && remainingSeconds >= 0)
            {
                _timeText.text = remainingMinutes.ToString() + ":" + remainingSeconds.ToString();
            }
        }
    }

    private void UpdateLiveCountUI()
    {
        int lifeCount = LifeManager.GetLifeCount();

        for (int i = 0; i < lifeCount; i++)
        {
            _lives.transform.GetChild(i).GetComponent<Image>().sprite = _lifeSprite;
        }
        if (lifeCount < 5)
        {
            var animator = _lives.transform.GetChild(lifeCount).AddComponent<Animator>();
            animator.runtimeAnimatorController = _pulseAC;

            Instantiate(_clockPrefab, _lives.transform.GetChild(lifeCount));
        }
    }

    public void DecreaseHealthOne()
    {
        LifeManager.DecreaseHealthOne();
        LifeManager.LifeLoadControl();
    }

    public void AddLifeCount(int count)
    {
        LifeManager.AddLifeCount(count);
        LifeManager.LifeLoadControl();
    }

    public void BuyLive()
    {
        int starCount = StarManager.GetStarCount();
        int livePrice = int.Parse(_buyButtonText.text);

        if (livePrice > starCount)
        {
            _starShopPopupOpener.OpenPopup();
        }
        else
        {
            StarManager.AddStarCount(-livePrice);
        }

        LifeManager.AddLifeCount(5);
    }

    
    public void NotificationButton()
    {
        if(isInit)
        {
#if UNITY_ANDROID
            isInit = false;

            if (_notificationSlider.value == 1)
            {
                AndroidNotifications.EnableNotifications();
                _notificationImage.sprite = _enabledSprite;
                Debug.Log("Bildirim açýldý");
            }
            else
            {
                AndroidNotifications.DisableNotifications();
                _notificationImage.sprite = _disabledSprite;
                Debug.Log("Bildirim kapatýldý");
            }
#endif
        }
        else
        {
#if UNITY_ANDROID
            if (AndroidNotifications.CheckSendNotifications())
            {
                AndroidNotifications.DisableNotifications();
                _notificationImage.sprite = _disabledSprite;
                Debug.Log("Bildirim kapatýldý");
            }
            else
            {
                AndroidNotifications.EnableNotifications();
                _notificationImage.sprite = _enabledSprite;
                Debug.Log("Bildirim açýldý");
            }
#endif
        }
        
    }


    public void FreeLiveButton()
    {
#if UNITY_ANDROID
        var adManager = FindObjectOfType<AdManager>();

        if(adManager.rewardedAd!=null)
        {
            adManager.ShowRewardedAd((Reward reward) =>
            {
                LifeManager.AddLifeCount(+3);
            });
        }
        else
        {
            adManager.LoadRewardedAd((RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded ad failed to load: " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded successfully!");

                adManager.ShowRewardedAd((Reward reward) =>
                {
                    LifeManager.AddLifeCount(+3);
                });
            });
        }
#endif

#if UNITY_WEBGL
        YandexGame.Instance._RewardedShow(1);

#endif

    }

}