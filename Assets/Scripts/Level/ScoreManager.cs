using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Verilen seviye numarasý için skoru döndürür.
    /// </summary>
    /// <param name="levelNumber">Seviye numarasý</param>
    /// <returns>Seviyenin skoru</returns>
    public static int GetLevelScore(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "Score", 0);
    }

    /// <summary>
    /// Verilen seviye numarasý için skoru ayarlar.
    /// Eðer yeni skor mevcut skordan yüksekse, skoru günceller.
    /// </summary>
    /// <param name="levelNumber">Seviye numarasý</param>
    /// <param name="score">Yeni skor</param>
    public static void SetLevelScore(int levelNumber, int score)
    {
        int maxScore = PlayerPrefs.GetInt("Level" + levelNumber + "Score", 0);
        if (score > maxScore)
            PlayerPrefs.SetInt("Level" + levelNumber + "Score", score);
    }

    /// <summary>
    /// Verilen seviye sayýsýna kadar olan tüm seviyelerin skorlarýný toplar.
    /// </summary>
    /// <param name="levelCount">Toplanacak seviyelerin sayýsý</param>
    /// <returns>Toplam skor</returns>
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
