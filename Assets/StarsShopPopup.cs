using GoogleMobileAds.Api;
using Samples.Purchasing.Core.BuyingConsumables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class StarsShopPopup : MonoBehaviour
{
    private BuyingConsumables _buyingConsumables;
    private void Start()
    {
        _buyingConsumables = FindObjectOfType<BuyingConsumables>();
    }

    public void FreeStarsButton()
    {
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
    }

    public void BuyStars(int index)
    {
        _buyingConsumables.BuyConsumable(index);
    }
}
