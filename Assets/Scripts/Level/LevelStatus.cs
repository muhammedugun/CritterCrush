using UnityEngine;

namespace Match3
{
    public static class LevelStatus
    {
        // Bir seviyeyi açar ve bu bilgiyi saklar
        public static void UnlockLevel(int levelIndex)
        {
            PlayerPrefs.SetInt($"Level_{levelIndex}_Unlocked", 1);
        }

        // Bir seviyenin açık olup olmadığını kontrol eder
        public static bool IsLevelUnlocked(int levelIndex)
        {
            if (levelIndex == 0) return true; // İlk seviye her zaman açık
            return PlayerPrefs.GetInt($"Level_{levelIndex}_Unlocked", 0) == 1;
        }
    }
}
