using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public static int GetBoosterCount(int boosterIndex)
    {
        return PlayerPrefs.GetInt("Booster" + boosterIndex +"Count", 0);
    }
}
