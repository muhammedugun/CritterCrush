using Samples.Purchasing.Core.BuyingConsumables;

using UnityEngine;

/// <summary>
/// Reklamlarý kaldýrma iþlemlerini yöneten sýnýf.
/// </summary>
public class RemoveAdManager : MonoBehaviour
{
    [SerializeField] private GameObject _buyButton, _buyedButton, _oldPrice;

    private BuyingConsumables _buyingConsumables;

    private void Start()
    {
        _buyingConsumables = FindObjectOfType<BuyingConsumables>();
        UpdateUI();
    }

    /// <summary>
    /// Reklamlarý kaldýrmak için satýn alma iþlemini baþlatýr.
    /// </summary>
    public void BuyRemoveAds()
    {
        _buyingConsumables.BuyNonConsumable();
        UpdateUI();
    }

    /// <summary>
    /// UI öðelerini günceller.
    /// </summary>
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
