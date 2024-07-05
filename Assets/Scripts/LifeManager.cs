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
    /// Can� 1 azalt�r
    /// </summary>
    public static void DecreaseHealthOne()
    {
        AddHeartCount(-1);
    }

    public static void AddHeartCount(int count)
    {
        int currentHeartCount = GetHeartCount();
        currentHeartCount += count;
        PlayerPrefs.SetInt("LifeCount", currentHeartCount);
    }

    public static int GetHeartCount()
    {
        return PlayerPrefs.GetInt("LifeCount", 0);
    }
}
