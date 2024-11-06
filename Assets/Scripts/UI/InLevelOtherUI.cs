using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class InLevelOtherUI : MonoBehaviour
{
    [SerializeField] private Text _levelName;


    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (YandexGame.lang == "ru")
        {
            _levelName.text = "Уровень " + sceneName.Substring(5, sceneName.Length-5);
        }
        else if (YandexGame.lang == "tr")
        {
            _levelName.text = "Seviye " + sceneName.Substring(5, sceneName.Length-5);
        }
        else
        {
            _levelName.text = "Level " + sceneName.Substring(5, sceneName.Length-5);
        }
        
    }
}
