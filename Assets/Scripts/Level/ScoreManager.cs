using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Verilen seviye numaras� i�in skoru d�nd�r�r.
    /// </summary>
    /// <param name="levelNumber">Seviye numaras�</param>
    /// <returns>Seviyenin skoru</returns>
    public static int GetLevelScore(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "Score", 0);
    }

    /// <summary>
    /// Verilen seviye numaras� i�in skoru ayarlar.
    /// E�er yeni skor mevcut skordan y�ksekse, skoru g�nceller.
    /// </summary>
    /// <param name="levelNumber">Seviye numaras�</param>
    /// <param name="score">Yeni skor</param>
    public static void SetLevelScore(int levelNumber, int score)
    {
        int maxScore = PlayerPrefs.GetInt("Level" + levelNumber + "Score", 0);
        if (score > maxScore)
            PlayerPrefs.SetInt("Level" + levelNumber + "Score", score);
    }

    /// <summary>
    /// Verilen seviye say�s�na kadar olan t�m seviyelerin skorlar�n� toplar.
    /// </summary>
    /// <param name="levelCount">Toplanacak seviyelerin say�s�</param>
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
