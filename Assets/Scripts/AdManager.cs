using UnityEngine;
using YG;

namespace Match3
{
    public class AdManager : MonoBehaviour
    {
        public enum RewardID{
            Live=1,
            Star,
            Move
        }
        
        public static void OpenLivesRewardAd()
        {
            OpenRewardAd((int)RewardID.Live);
        }
        
        public static void OpenStarsRewardAd()
        {
            OpenRewardAd((int)RewardID.Star);
        }

        public static void OpenMoveRewardAd()
        {
            OpenRewardAd((int)RewardID.Move);
        }
        
        public static void OpenFullscreenAd()
        {
            YandexGame.FullscreenShow();
        }
        
        private static void OpenRewardAd(int id)
        {
            YandexGame.RewVideoShow(id);
        }


    }
}