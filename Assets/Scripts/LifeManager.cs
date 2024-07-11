using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private void Start()
    {
        EventBus.Subscribe(EventType.MoveCountOver, DecreaseHealthOne);
    }

    /// <summary>
    /// Caný 1 azaltýr
    /// </summary>
    public static void DecreaseHealthOne()
    {
        AddLifeCount(-1);
    }

    public static void AddLifeCount(int count)
    {
        int currentHeartCount = GetLifeCount();
        currentHeartCount += count;
        currentHeartCount = Mathf.Clamp(currentHeartCount, 0, 5);
        PlayerPrefs.SetInt("LifeCount", currentHeartCount);
    }

    public static int GetLifeCount()
    {
        return PlayerPrefs.GetInt("LifeCount", 0);
    }
}
