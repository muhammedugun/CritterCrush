using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levels;
    [SerializeField] private LevelList _levelList;

    private void Start()
    {
        UpdateLevelStars();
    }

    private void UpdateLevelStars()
    {
        int levelStarCount;

        for (int i = 0; i < _levelList.SceneCount; i++)
        {
            levelStarCount = LevelStars.GetStars(i);

            for (int j = 0; j < levelStarCount; j++)
            {
                _levels.transform.GetChild(i).GetChild(j + 1).GetChild(3).gameObject.SetActive(true);
                _levels.transform.GetChild(i).GetChild(j + 1).GetChild(2).gameObject.SetActive(false);
            }


        }
    }


}
