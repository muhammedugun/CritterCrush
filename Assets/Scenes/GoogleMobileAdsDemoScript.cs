using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    [SerializeField] private bool _useTestAds;

    private string _adUnitId;

    [SerializeField] private string _interstitialId;
    [SerializeField] private string _interstitialTestId;


    public void Start()
    {
        if(String.IsNullOrWhiteSpace(_interstitialId)  || String.IsNullOrWhiteSpace(_interstitialTestId))
        {
            Debug.LogError("Reklam id'lerinden biri boþ býrakýlmýþ");
            return;
        }
        
        if(_useTestAds)
            _adUnitId = _interstitialTestId;
        else
            _adUnitId = _interstitialId;
        

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // Bu callback MobileAds SDK baþlatýldýktan sonra çaðrýlýr
            LoadInterstitialAd();
        });
    }



    private InterstitialAd _interstitialAd;

    /// <summary>
    /// Reklamý yükle
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;

                if (_interstitialAd != null)
                    RegisterReloadHandler(_interstitialAd);
            });

        
    }


    /// <summary>
    /// Reklamý ekranda kullanýcýya göster
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
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

    




}