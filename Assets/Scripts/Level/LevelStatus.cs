using UnityEngine;
using YG;

namespace Match3
{
    public static class LevelStatus
    {
        // Bir seviyeyi açar ve bu bilgiyi saklar
        public static void UnlockLevel(int levelIndex)
        {
            YandexGame.savesData.openLevels[levelIndex+1] = true;
            YandexGame.SaveProgress();
        }

        // Bir seviyenin açık olup olmadığını kontrol eder
        public static bool IsLevelUnlocked(int levelIndex)
        {
            if (levelIndex == 0) return true; // İlk seviye her zaman açık
            return YandexGame.savesData.openLevels[levelIndex+1] == true;
        }
    }
}
