using UnityEngine;
using YG;

namespace Match3
{
    public static class LevelStars
    {
        // Yıldızları kaydetmek için bir yardımcı sınıf
        public static void SaveStars(int levelIndex, int stars)
        {
            if (stars > GetStars(levelIndex))
            {
                YandexGame.savesData.levelStars[levelIndex + 1] = stars;
                YandexGame.NewLeaderboardScores("Leaderboard", GetTotalStarCount());
                YandexGame.SaveProgress();
            }
        }

        public static int GetStars(int levelIndex)
        {
            return YandexGame.savesData.levelStars[levelIndex + 1];
        }

        /// <summary>
        /// Tüm levellerdeki toplam yıldız sayısını döndürür
        /// </summary>
        /// <returns></returns>
        public static int GetTotalStarCount()
        {
            int totalStars = 0;
            foreach (var starCount in YandexGame.savesData.levelStars)
            {
                totalStars += starCount;
            }
            return totalStars;
        }
    }
}
