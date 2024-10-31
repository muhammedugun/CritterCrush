using Ricimi;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoosterShopUI : MonoBehaviour
    {

        [SerializeField] private PopupOpener _popupOpener;
        [SerializeField] private GameObject _areYouSureBuyShop;
        public void BuyBooster(int boosterIndex)
        {
            int starCount = StarManager.GetStarCount();
            int price = BoosterManager.GetBoosterPrice(boosterIndex);
            
            if (starCount<price)
            {
                _popupOpener.popupPrefab = _areYouSureBuyShop;
                _popupOpener.OpenPopup();
            }
            else
            {
                StarManager.AddStarCount(-price);
                BoosterManager.AddBoosterCount(boosterIndex, 1);
            }
        }
    }
}