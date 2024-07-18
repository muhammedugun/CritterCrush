using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLevelBoosterUI : MonoBehaviour
{
    [SerializeField] private Transform _boosters;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            _boosters.GetChild(i).GetChild(2).GetChild(2).GetComponent<Text>().text = "x" + BoosterManager.GetBoosterCount(i).ToString();
        }
        
    }

    public void UseBooster(int boosterIndex)
    {
        if (BoosterManager.GetBoosterCount(boosterIndex) <= 0)
        {
            Debug.LogWarning("Yeterli Booster Yok");
        }
        else
        {
            Debug.LogWarning("Mevcut Booster Sayýsý: " + BoosterManager.GetBoosterCount(boosterIndex));
        }

        if(GameManager.Instance.Board.m_ActivatedBooster==null && BoosterManager.GetBoosterCount(boosterIndex)>0)
        {
            BoosterManager.AddBoosterCount(boosterIndex ,-1);
            var item = GameManager.Instance.BoosterItems[boosterIndex];
            GameManager.Instance.ActivateBoosterItem(item.Item);
        }
        else
        {
            GameManager.Instance.DeactiveBoosterItem();
        }
        
    }

    
}
