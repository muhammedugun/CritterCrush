using Samples.Purchasing.Core.BuyingConsumables;

using UnityEngine;

/// <summary>
/// Reklamlar� kald�rma i�lemlerini y�neten s�n�f.
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
    /// Reklamlar� kald�rmak i�in sat�n alma i�lemini ba�lat�r.
    /// </summary>
    public void BuyRemoveAds()
    {
        _buyingConsumables.BuyNonConsumable();
        UpdateUI();
    }

    /// <summary>
    /// UI ��elerini g�nceller.
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
