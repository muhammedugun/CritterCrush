using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    [SerializeField] private bool _useTestAds;

    [SerializeField] private string _interstitialId;
    [SerializeField] private string _interstitialTestId;

    [SerializeField] private string _rewardedId;
    [SerializeField] private string _rewardedTestId;

    private string _interstitialIdUsed;
    private string _rewardedIdUsed;

    public InterstitialAd interstitialAd;
    public RewardedAd rewardedAd;

    private float _lastAdShowTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    public void Start()
    {
        if(String.IsNullOrWhiteSpace(_interstitialId)  || String.IsNullOrWhiteSpace(_interstitialTestId) || 
           String.IsNullOrWhiteSpace(_rewardedId) || String.IsNullOrWhiteSpace(_rewardedTestId))
        {
            Debug.LogError("Reklam id'lerinden biri boþ býrakýlmýþ");
            return;
        }
        
        if(_useTestAds)
        {
            _interstitialIdUsed = _interstitialTestId;
            _rewardedIdUsed = _rewardedTestId;
        }
            
        else
        {
            _interstitialIdUsed = _interstitialId;
            _rewardedIdUsed = _rewardedId;
        }
            
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

        if(!GetIsAdsRemoved())
            EventBus.Subscribe(EventType.AllGoalCompleted, InterstitialAdShowControlInvoke);

        
    }

    /// <summary>
    /// Reklamlar silindi mi?
    /// </summary>
    /// <returns></returns>
    public static bool GetIsAdsRemoved()
    {
        if (PlayerPrefs.GetInt("GetIsAdsRemoved", 0) == 0)
            return false;
        else return true;
    }

    public static void RemoveAd()
    {
        PlayerPrefs.SetInt("GetIsAdsRemoved", 1);
        PlayerPrefs.Save();
    }

    private void InterstitialAdShowControlInvoke()
    {
        if (!GetIsAdsRemoved())
            Invoke(nameof(InterstitialAdShowControl), 2f);
    }

    private void InterstitialAdShowControl()
    {
        if((_lastAdShowTime == 0f && Time.time >= 240f) || Time.time - _lastAdShowTime >= 240f)
        {
            if (interstitialAd != null)
            {
                ShowInterstitialAd();
            }
            else
            {
                LoadInterstitialAd((InterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null)
                    {
                        Debug.LogError("Rewarded ad failed to load: " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded successfully!");

                    ShowInterstitialAd();

                });
            }
        }
    }


    /// <summary>
    /// Reklamý yükle
    /// </summary>
    /// <param name="onAdLoaded">Ýsteðe baðlý callback fonksiyonu</param>
    public void LoadInterstitialAd(Action<InterstitialAd, LoadAdError> onAdLoaded = null)
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_interstitialIdUsed, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    // Ýsteðe baðlý callback çaðrýlýr
                    onAdLoaded?.Invoke(null, error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;

                if (interstitialAd != null)
                    RegisterReloadHandler(interstitialAd);

                // Ýsteðe baðlý callback çaðrýlýr
                onAdLoaded?.Invoke(ad, null);
            });
    }


    /// <summary>
    /// Reklamý ekranda kullanýcýya göster
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _lastAdShowTime = Time.time;
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }


    /// <summary>
    /// Bir sonraki reklamý önceden yükleyin/hazýrlayýn
    /// </summary>
    /// <param name="interstitialAd">En son yüklenmiþ olan reklamý gönderin</param>
    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }


    /// <summary>
    /// Reklam eventlerini dinleyin
    /// </summary>
    /// <param name="interstitialAd">Dinlemek istediðiniz reklam</param>
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }


    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    /// <param name="onAdLoaded">Ýsteðe baðlý callback fonksiyonu</param>
    public void LoadRewardedAd(Action<RewardedAd, LoadAdError> onAdLoaded = null)
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedIdUsed, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    // Ýsteðe baðlý callback çaðrýlýr
                    onAdLoaded?.Invoke(null, error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;

                if (rewardedAd != null)
                    RegisterReloadHandler(rewardedAd);

                // Ýsteðe baðlý callback çaðrýlýr
                onAdLoaded?.Invoke(ad, null);
            });
    }



    public void ShowRewardedAd(Action<Reward> userRewardEarnedCallback)
    {
        //const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            /*
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
            */

            _lastAdShowTime = Time.time;

            rewardedAd.Show(userRewardEarnedCallback);
        }
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }


}