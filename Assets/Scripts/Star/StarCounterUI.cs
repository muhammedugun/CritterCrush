using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounterUI : MonoBehaviour
{
    [SerializeField] private Text _starCountText;

    private void Start()
    {
        UpdateStarCountUI();

        EventBus.Subscribe(EventType.StarCountChanged, UpdateStarCountUI);
    }

    private void UpdateStarCountUI()
    {
        if (_starCountText != null)
        {
            _starCountText.text = StarManager.GetStarCount().ToString();
        }
        else
        {
            Debug.LogWarning("StarCountText atanmamýþ!");
        }
    }

}
