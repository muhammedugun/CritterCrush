using UnityEngine;

namespace Match3
{
    public static class LevelCompletion
    {
        public static void CompleteLevel(int levelIndex, int starsEarned)
        {
            LevelStars.SaveStars(levelIndex, starsEarned);

            if (starsEarned > 0)
            {
                LevelStatus.UnlockLevel(levelIndex + 1); // Bir sonraki seviyeyi aç
            }
        }
    }
}
