using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounterUI : MonoBehaviour
{
    [SerializeField] private Transform _lives;
    [SerializeField] private Sprite _lifeSprite, _greyLifeSprite;

    private void Start()
    {
        UpdateLifeCounterUI();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.LifeCountChanged, UpdateLifeCounterUI);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.LifeCountChanged, UpdateLifeCounterUI);
    }

    private void UpdateLifeCounterUI()
    {
        if (_lives != null)
        {
            if (_greyLifeSprite != null)
            {
                for (int i = 1; i <= 5; i++)
                {
                    _lives.GetChild(i).GetComponent<Image>().sprite = _greyLifeSprite;
                }
            }

            int lifeCount = LifeManager.GetLifeCount();

            for (int i = 1; i < lifeCount + 1; i++)
            {

                _lives.GetChild(i).GetComponent<Image>().sprite = _lifeSprite;
            }

        }
        else
        {
            Debug.LogWarning("Lives atanmam��!");
        }
    }
}
