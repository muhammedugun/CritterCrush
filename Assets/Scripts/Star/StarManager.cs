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
    /// Y�ld�z say�s�n� belirtilen miktarda art�r�r ve g�ncellenen y�ld�z say�s�n� kaydeder.
    /// </summary>
    /// <param name="count">Eklemek istenen y�ld�z miktar�.</param>
    public static void AddStarCount(int count)
    {
        int currentStarCount = GetStarCount();
        currentStarCount += count;
        PlayerPrefs.SetInt("StarCount", currentStarCount);
        PlayerPrefs.Save();

        // Y�ld�z say�s� de�i�ti�inde EventBus'e bildirim g�nderir.
        EventBus.Publish(EventType.StarCountChanged);
    }

    /// <summary>
    /// Mevcut y�ld�z say�s�n� al�r.
    /// </summary>
    /// <returns>Mevcut y�ld�z say�s�.</returns>
    public static int GetStarCount()
    {
        return PlayerPrefs.GetInt("StarCount", 0);
    }

    /// <summary>
    /// Belirtilen ID'ye g�re �d�l y�ld�zlar�n� ekler.
    /// </summary>
    /// <param name="ID">Rekabet ID'si, e�er 0 ise �d�l y�ld�zlar� eklenir.</param>
    public static void RewardStars(int ID)
    {
        // �d�l y�ld�zlar� reklam� izlendiyse
        if (ID == 0)
        {
            AddStarCount(10);
        }
    }
}
