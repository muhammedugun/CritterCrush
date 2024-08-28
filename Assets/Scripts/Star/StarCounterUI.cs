using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounterUI : MonoBehaviour
{
    /// <summary>
    /// Yýldýz sayýsýný gösteren UI metin bileþeni.
    /// </summary>
    [SerializeField] private Text _starCountText;

    private void Start()
    {
        UpdateStarCountUI();

        // Yýldýz sayýsý deðiþtiðinde UI'yi güncellemek için EventBus'e abone olunur.
        EventBus.Subscribe(EventType.StarCountChanged, UpdateStarCountUI);
    }

    /// <summary>
    /// Yýldýz sayýsýný günceller ve UI'yi günceller.
    /// </summary>
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
