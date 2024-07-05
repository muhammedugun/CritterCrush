using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InLevelOtherUI : MonoBehaviour
{
    [SerializeField] private Text _levelName;


    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        _levelName.text = sceneName.Substring(0, 5) + " " + sceneName.Substring(5, sceneName.Length-5);
    }
}
