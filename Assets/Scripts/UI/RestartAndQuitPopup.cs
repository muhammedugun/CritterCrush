using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartAndQuitPopup : MonoBehaviour
{
    private PausePopupManager _pausePopupManager;
    
    void Start()
    {
        _pausePopupManager = FindObjectOfType<PausePopupManager>();
    }


    public void ResumeLevel()
    {
        _pausePopupManager.ResumeLevel();
        _pausePopupManager.gameObject.GetComponent<Popup>().Close();
    }

    public void RestartLevel()
    {
        _pausePopupManager.RestartLevel();
    }


    public void QuitLevel()
    {
        _pausePopupManager.QuitLevel();
    }
}
