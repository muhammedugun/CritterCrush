using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public static int GetBoosterCount(int boosterIndex)
    {
        return PlayerPrefs.GetInt("Booster" + boosterIndex +"Count", 0);
    }
    public static void AddBoosterCount(int boosterIndex, int boosterCount)
    {
        int currentCount = GetBoosterCount(boosterIndex);

        PlayerPrefs.SetInt("Booster" + boosterIndex + "Count", currentCount + boosterCount);
    }

    /// <summary>
    /// Herhangi bir booster var mý?
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
