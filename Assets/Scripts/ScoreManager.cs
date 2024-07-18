using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    public static int GetLevelScore(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "Score", 0);
    }

    public static void SetLevelScore(int levelNumber, int score)
    {
        int maxScore = PlayerPrefs.GetInt("Level" + levelNumber + "Score", 0);
        if (score > maxScore)
            PlayerPrefs.SetInt("Level" + levelNumber + "Score", score);
    }

    public static int GetTotalScore(int levelCount)
    {
        int totalScore = 0;
        for (int i = 1; i <= levelCount; i++)
        {
            totalScore += PlayerPrefs.GetInt("Level" + i + "Score", 0);
        }
        return totalScore;
    }

}