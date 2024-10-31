using Match3;
using UnityEngine;
using YG;

public class StarManager : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += AddStarsForReward;
    }
    
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= AddStarsForReward;
    }
    
    /// <summary>
    /// Y�ld�z say�s�n� belirtilen miktarda art�r�r ve g�ncellenen y�ld�z say�s�n� kaydeder.
    /// </summary>
    /// <param name="count">Eklemek istenen y�ld�z miktar�.</param>
    public static void AddStarCount(int count)
    {
        int currentStarCount = GetStarCount();
        currentStarCount += count;

        YandexGame.savesData.starCount = currentStarCount;
        YandexGame.SaveProgress();

        // Y�ld�z say�s� de�i�ti�inde EventBus'e bildirim g�nderir.
        EventBus.Publish(EventType.StarCountChanged);
    }

    /// <summary>
    /// Mevcut y�ld�z say�s�n� al�r.
    /// </summary>
    /// <returns>Mevcut y�ld�z say�s�.</returns>
    public static int GetStarCount()
    {
        return YandexGame.savesData.starCount;
    }
    
    private void AddStarsForReward(int adID)
    {
        if (adID == (int)AdManager.RewardID.Star)
        {
            AddStarCount(20);
        }
    }
}
