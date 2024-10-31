using UnityEngine;
using YG;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Verilen seviye numaras� i�in skoru d�nd�r�r.
    /// </summary>
    /// <param name="levelNumber">Seviye numaras�</param>
    /// <returns>Seviyenin skoru</returns>
    public static int GetLevelScore(int levelNumber)
    {
        return YandexGame.savesData.levelScores[levelNumber];
    }

    /// <summary>
    /// Verilen seviye numaras� i�in skoru ayarlar.
    /// E�er yeni skor mevcut skordan y�ksekse, skoru g�nceller.
    /// </summary>
    /// <param name="levelNumber">Seviye numaras�</param>
    /// <param name="score">Yeni skor</param>
    public static void SetLevelScore(int levelNumber, int score)
    {
        int currentScore = YandexGame.savesData.levelScores[levelNumber];
        if (score > currentScore)
        {
            YandexGame.savesData.levelScores[levelNumber] = score;
            YandexGame.SaveProgress();
        }
            
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
            totalScore += YandexGame.savesData.levelScores[i];
        }
        return totalScore;
    }
}
