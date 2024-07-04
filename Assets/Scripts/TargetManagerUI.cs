using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject _goals;
    private LevelList _levelList;

    private void Start()
    {
        _levelList = FindObjectOfType<LevelDataProvider>().levelList;
        UpdateGoalSprite();
        EventBus<int>.Subscribe(EventType.GoalCountChanged, UpdateGoalCount);
    }

    public void UpdateGoalSprite()
    {
        int levelIndex = LevelList.GetSceneIndex();

        for (int i = 0; i < _goals.transform.childCount; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(false);
        }


        int length = _levelList.Goals[levelIndex].Goals.Length;

        for (int i = 0; i < length; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(true);
            var image = _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>();
            image.sprite = _levelList.Goals[levelIndex].Goals[i].Gem.UISprite;

            _goals.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "/"+_levelList.Goals[levelIndex].Goals[i].Count.ToString();
        }
    }

    public void UpdateGoalCount(int count)
    {
        int levelIndex = LevelList.GetSceneIndex();
        int length = _levelList.Goals[levelIndex].Goals.Length;

        for (int i = 0; i < length; i++)
        {
            _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = count.ToString();


        }
    }


}
