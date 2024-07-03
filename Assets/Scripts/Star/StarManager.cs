using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{

    [SerializeField] private Text _starCountText;

    private void Start()
    {
        if(_starCountText!=null)
        {
            _starCountText.text = GetStarCount().ToString();
        }
        else
        {
            Debug.LogWarning("StarCountText atanmamýþ!");
        }
    }

    public static void AddStarCount(int count)
    {
        int currentStarCount = GetStarCount();
        currentStarCount += count;
        PlayerPrefs.SetInt("StarCount", currentStarCount);
    }

    public static int GetStarCount()
    {
        return PlayerPrefs.GetInt("StarCount", 0);
    }
}
