using Ricimi;
using UnityEngine;
using UnityEngine.UI;

public class LivesShopPopup : MonoBehaviour
{
    [SerializeField] private Text _buyButtonText;
    [SerializeField] private PopupOpener _starShopPopupOpener;

    private void Start()
    {
        
    }

    public void BuyLive()
    {
        int starCount = StarManager.GetStarCount();
        int livePrice = int.Parse(_buyButtonText.text);

        if(livePrice>starCount)
        {
            _starShopPopupOpener.OpenPopup();
        }
        else
        {
            StarManager.AddStarCount(-livePrice);
        }
    }
}
