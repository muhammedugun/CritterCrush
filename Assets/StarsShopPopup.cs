#if UNITY_ANDROID
using GoogleMobileAds.Api;
using Samples.Purchasing.Core.BuyingConsumables;
using UnityEngine.Purchasing;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_WEBGL
using YG;
#endif
public class StarsShopPopup : MonoBehaviour
{
    #if UNITY_ANDROID
    private BuyingConsumables _buyingConsumables;
    private void Start()
    {
        _buyingConsumables = FindObjectOfType<BuyingConsumables>();
    }
    #endif

    public void FreeStarsButton()
    {
#if UNITY_ANDROID
        var adManager = FindObjectOfType<AdManager>();

        
        if (adManager.rewardedAd != null)
        {
            adManager.ShowRewardedAd((Reward reward) =>
            {
                StarManager.AddStarCount(10);
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
                    StarManager.AddStarCount(10);
                });
            });
        }

#endif

#if UNITY_WEBGL
        YandexGame.Instance._RewardedShow(0);
        
#endif

    }


    public void BuyStars(int index)
    {
#if UNITY_ANDROID
        _buyingConsumables.BuyConsumable(index);
#endif
    }


}
