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
    }

    public static int GetStarCount()
    {
        return PlayerPrefs.GetInt("StarCount", 0);
    }

}
