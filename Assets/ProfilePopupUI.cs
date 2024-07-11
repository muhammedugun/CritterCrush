using Match3;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopupUI : MonoBehaviour
{
    
    [SerializeField] private GameObject _Boosters;
    [SerializeField] private GameObject _inventoryBoosterPrefab;
    [SerializeField] private Sprite[] _boosterSprites;
    [SerializeField] private Sprite[] _avatarsSprites;
    [SerializeField] private Image _avatarImage;


    private void Start()
    {
        UpdateAvatar();
        int boosterCount;
        int boosterAddCount=0;
        for (int i = 0; i < 5; i++)
        {
            boosterCount = BoosterManager.GetBoosterCount(i);

            if (boosterCount > 0)
            {
                var inventoryBooster= Instantiate(_inventoryBoosterPrefab) as GameObject;
                inventoryBooster.transform.SetParent(_Boosters.transform.GetChild(0).GetChild(boosterAddCount).transform, false);
                inventoryBooster.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = boosterCount.ToString();
                inventoryBooster.transform.GetChild(2).GetComponent<Image>().sprite = _boosterSprites[i];
                boosterAddCount++;
            }

        }


    }

    private void UpdateAvatar()
    {
        _avatarImage.sprite = _avatarsSprites[AvatarSelectionUI.GetCurrentAvatar()];
    }


}
