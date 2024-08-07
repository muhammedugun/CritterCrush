#if UNITY_ANDROID
using Samples.Purchasing.Core.BuyingConsumables;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdManager : MonoBehaviour
{

    [SerializeField] private GameObject _buyButton, _buyedButton, _oldPrice;
#if UNITY_ANDROID
    private BuyingConsumables _buyingConsumables;

    private void Start()
    {
        _buyingConsumables = FindObjectOfType<BuyingConsumables>();
        UpdateUI();
    }
#endif
    public void BuyRemoveAds()
    {
#if UNITY_ANDROID
        _buyingConsumables.BuyNonConsumable();
        UpdateUI();
#endif
    }

    private void UpdateUI()
    {
#if UNITY_ANDROID

        if (AdManager.GetIsAdsRemoved())
        {
            _buyButton.SetActive(false);
            _oldPrice.SetActive(false);
            _buyedButton.SetActive(true);
        }
        else
        {
            _buyButton.SetActive(true);
            _oldPrice.SetActive(true);
            _buyedButton.SetActive(false);
        }
#endif
    }

}