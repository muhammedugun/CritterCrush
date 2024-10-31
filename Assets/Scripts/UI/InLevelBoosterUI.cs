using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLevelBoosterUI : MonoBehaviour
{
    [SerializeField] private Transform _boosters;
    private Board _board;
    void Start()
    {
        _board = FindObjectOfType<Board>();

        for (int i = 0; i < 5; i++)
        {
            UpdateBoosterCount(i);
        }

        EventBus<int>.Subscribe(EventType.BoosterUsed, UpdateBoosterCount);
        
    }

    public void UseBooster(int boosterIndex)
    {
        if (BoosterManager.GetBoosterCount(boosterIndex) <= 0)
        {
            Debug.LogWarning("Yeterli Booster Yok");
        }

        if(GameManager.Instance.Board.m_ActivatedBooster==null && BoosterManager.GetBoosterCount(boosterIndex)>0)
        {
            var item = GameManager.Instance.BoosterItems[boosterIndex];
            _board.boosterIndex = boosterIndex;
            GameManager.Instance.ActivateBoosterItem(item.Item);
            
        }
        else
        {
            _board.boosterIndex = -1;
            GameManager.Instance.DeactiveBoosterItem();
        }

    }

    public void UpdateBoosterCount(int index)
    {
        _boosters.GetChild(index).GetChild(2).GetChild(1).GetComponent<Text>().text = "x" + BoosterManager.GetBoosterCount(index).ToString();
    }

    
}
