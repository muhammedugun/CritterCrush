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
        if(GameManager.Instance.Board.m_ActivatedBooster==null)
        {
            var item = GameManager.Instance.BoosterItems[boosterIndex];
            GameManager.Instance.ActivateBoosterItem(item.Item);
        }
        else
        {
            GameManager.Instance.DeactiveBoosterItem();
        }
        
    }

    
}
