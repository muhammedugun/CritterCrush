using UnityEngine;

namespace Match3
{
    public static class LevelStars
    {
        // Yıldızları kaydetmek için bir yardımcı sınıf
        public static void SaveStars(int levelIndex, int stars)
        {
            PlayerPrefs.SetInt($"Level_{levelIndex}_Stars", stars);
        }

        public static int GetStars(int levelIndex)
        {
            return PlayerPrefs.GetInt($"Level_{levelIndex}_Stars", 0);
        }
    }
}
