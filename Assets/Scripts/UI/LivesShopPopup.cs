using System;
using Match3;
using Ricimi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Ya�am d�kkan�n�n pop-up aray�z� i�in kullan�lan bile�enleri tan�mlar.
/// </summary>
public class LivesShopPopup : MonoBehaviour
{
    [SerializeField] private GameObject _clockPrefab;
    [SerializeField] private RuntimeAnimatorController _pulseAC;
    [SerializeField] private Sprite _lifeSprite;
    [SerializeField] private GameObject _lives;

    [SerializeField] private Text _buyButtonText, _timeText; 
    [SerializeField] private PopupOpener _popupOpener;
    [SerializeField, Tooltip("Prefab ataması yap")] private GameObject _livesIsFullPopup, _starsIsNotEnoughPopup;

    private bool isInit = true;

    private void Start()
    {
        LifeManager.LifeLoadControl();
        UpdateLiveCountUI();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.LifeCountChanged, UpdateLiveCountUI);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.LifeCountChanged, UpdateLiveCountUI);
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

    /// <summary>
    /// Ya�am say�s�n� g�nceller ve eksik ya�amlar� g�stermek i�in animasyon ekler.
    /// </summary>
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

    /// <summary>
    /// Bir ya�am say�s�n� azalt�r.
    /// </summary>
    public void DecreaseHealthOne()
    {
        LifeManager.DecreaseHealthOne();
        LifeManager.LifeLoadControl();
    }

    /// <summary>
    /// Ya�am say�s�n� belirtilen miktarda art�r�r.
    /// </summary>
    /// <param name="count">Art�r�lacak ya�am say�s�.</param>
    public void AddLifeCount(int count)
    {
        LifeManager.AddLifeCount(count);
        LifeManager.LifeLoadControl();
    }

    /// <summary>
    /// Ya�am sat�n al�r ve y�ld�z say�s�n� g�nceller.
    /// </summary>
    public void BuyLive()
    {
        int starCount = StarManager.GetStarCount();
        int livePrice = int.Parse(_buyButtonText.text);

        int lifeCount = LifeManager.GetLifeCount();
        
        if (livePrice > starCount)
        {
            _popupOpener.popupPrefab = _starsIsNotEnoughPopup;
            _popupOpener.OpenPopup();
        }
        else if (lifeCount >= 5)
        {
            _popupOpener.popupPrefab = _livesIsFullPopup;
            _popupOpener.OpenPopup();
        }
        else
        {
            StarManager.AddStarCount(-livePrice);
            LifeManager.AddLifeCount(5);
        }
        
    }

    /// <summary>
    /// Reklam izleyerek bedava live kazandırır.
    /// </summary>
    public void FreeLiveButton()
    {
        int lifeCount = LifeManager.GetLifeCount();
        if (lifeCount >= 5)
        {
            _popupOpener.popupPrefab = _livesIsFullPopup;
            _popupOpener.OpenPopup();
        }
        else
            AdManager.OpenLivesRewardAd();
        
    }
}
