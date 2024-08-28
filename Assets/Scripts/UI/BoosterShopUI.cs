using Ricimi;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoosterShopUI : MonoBehaviour
    {

        [SerializeField] private PopupOpener _popupOpener;
        [SerializeField] private GameObject _areYouSureBuyShop;
        public void CheckStarCount(int price)
        {
            int starCount = StarManager.GetStarCount();
            if (starCount<price)
            {
                _popupOpener.popupPrefab = _areYouSureBuyShop;
                _popupOpener.OpenPopup();
            }
            else
            {
                StarManager.AddStarCount(-price);
                Debug.LogWarning("Booster satın alındı ve yıldız sayısı güncellendi");
            }
        }
    }
}