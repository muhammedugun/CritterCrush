using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "ShopItemBonusItem", menuName = "2D Match/Shop Items/Bonus Item")]
    public class ShopItemBonusItem : ShopSetting.ShopItem
    {
        public BoosterItem BonusItem;
    
        public override void Buy()
        {
            GameManager.Instance.AddBoosterItem(BonusItem);
         
        }
    }
}