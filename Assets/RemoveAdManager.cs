using Samples.Purchasing.Core.BuyingConsumables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdManager : MonoBehaviour
{
    [SerializeField] private GameObject _buyButton, _buyedButton, _oldPrice; 
    private BuyingConsumables _buyingConsumables;
    private void Start()
    {
        _buyingConsumables = FindObjectOfType<BuyingConsumables>();
        UpdateUI();


    }

    public void BuyRemoveAds()
    {
        _buyingConsumables.BuyNonConsumable();
        UpdateUI();
    }

    private void UpdateUI()
    {
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
    }
}
