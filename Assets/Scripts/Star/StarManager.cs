using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    public static void AddStarCount(int count)
    {
        int currentStarCount = GetStarCount();
        currentStarCount += count;
        PlayerPrefs.SetInt("StarCount", currentStarCount);
        PlayerPrefs.Save();

        EventBus.Publish(EventType.StarCountChanged);
    }

    public static int GetStarCount()
    {
        return PlayerPrefs.GetInt("StarCount", 0);
    }

    public static void RewardStars(int ID)
    {
        // Reward Stars reklam� izlendiyse
        if(ID==0)
        {
            AddStarCount(10);
        }
               
    }

}
