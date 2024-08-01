using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleTest : MonoBehaviour
{
    public GameObject objectPrefab;


    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void CreateObject()
    {
        Instantiate(objectPrefab);
    }

    public void CreateObjectWithPause()
    {
        Pause();
        Instantiate(objectPrefab);
    }
}
