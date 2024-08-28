using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_WEBGL
using YG;
#endif

public class StarManager : MonoBehaviour
{
    /// <summary>
    /// Yýldýz sayýsýný belirtilen miktarda artýrýr ve güncellenen yýldýz sayýsýný kaydeder.
    /// </summary>
    /// <param name="count">Eklemek istenen yýldýz miktarý.</param>
    public static void AddStarCount(int count)
    {
        int currentStarCount = GetStarCount();
        currentStarCount += count;
        PlayerPrefs.SetInt("StarCount", currentStarCount);
        PlayerPrefs.Save();

        // Yýldýz sayýsý deðiþtiðinde EventBus'e bildirim gönderir.
        EventBus.Publish(EventType.StarCountChanged);
    }

    /// <summary>
    /// Mevcut yýldýz sayýsýný alýr.
    /// </summary>
    /// <returns>Mevcut yýldýz sayýsý.</returns>
    public static int GetStarCount()
    {
        return PlayerPrefs.GetInt("StarCount", 0);
    }

    /// <summary>
    /// Belirtilen ID'ye göre ödül yýldýzlarýný ekler.
    /// </summary>
    /// <param name="ID">Rekabet ID'si, eðer 0 ise ödül yýldýzlarý eklenir.</param>
    public static void RewardStars(int ID)
    {
        // Ödül yýldýzlarý reklamý izlendiyse
        if (ID == 0)
        {
            AddStarCount(10);
        }
    }
}
