using System;
using UnityEngine;
using YG;

public class BoosterManager : MonoBehaviour
{
    private void Start()
    {
        SetBoosterPrice(0, 5);
        SetBoosterPrice(1, 1);
        SetBoosterPrice(2, 2);
        SetBoosterPrice(3, 3);
        SetBoosterPrice(4, 2);
    }

    public static int GetBoosterCount(int boosterIndex)
    {
        return YandexGame.savesData.boosterCounts[boosterIndex];
    }
    public static void AddBoosterCount(int boosterIndex, int boosterCount)
    {
        int currentCount = GetBoosterCount(boosterIndex);

        YandexGame.savesData.boosterCounts[boosterIndex] = currentCount + boosterCount;
        YandexGame.SaveProgress();
        EventBus.Publish(EventType.AnyBoosterCountChanged);
    }

    public static int GetBoosterPrice(int boosterIndex)
    {
        return YandexGame.savesData.boosterPrices[boosterIndex];
    }

    private void SetBoosterPrice(int boosterIndex, int boosterPrice)
    {
        YandexGame.savesData.boosterPrices[boosterIndex] = boosterPrice;
        YandexGame.SaveProgress();
    }

    /// <summary>
    /// Herhangi bir booster var m�?
    /// </summary>
    /// <returns></returns>
    public static bool IsThereAnyBooster()
    {
        for (int i = 0; i < 5; i++)
        {
            if(GetBoosterCount(i) > 0)
                return true;
        }
        return false;

    }
}
