using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounterUI : MonoBehaviour
{
    /// <summary>
    /// Y�ld�z say�s�n� g�steren UI metin bile�eni.
    /// </summary>
    [SerializeField] private Text _starCountText;

    private void Start()
    {
        UpdateStarCountUI();
    }

    private void OnEnable()
    {
        // Y�ld�z say�s� de�i�ti�inde UI'yi g�ncellemek i�in EventBus'e abone olunur.
        EventBus.Subscribe(EventType.StarCountChanged, UpdateStarCountUI);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.StarCountChanged, UpdateStarCountUI);
    }

    /// <summary>
    /// Y�ld�z say�s�n� g�nceller ve UI'yi g�nceller.
    /// </summary>
    private void UpdateStarCountUI()
    {
        if (_starCountText != null)
        {
            _starCountText.text = StarManager.GetStarCount().ToString();
        }
        else
        {
            Debug.LogWarning("StarCountText atanmam��!");
        }
    }
}
