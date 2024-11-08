using System.Collections.Generic;
using System.Threading.Tasks;
using Ricimi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopupManager : MonoBehaviour
{
    private List<GameObject> deactivedObjects;

    void Start()
    {
        PauseLevel();
    }

    public void PauseLevel()
    {
        GameSceneManager.PauseLevel(ref deactivedObjects);
    }

    public void ResumeLevel()
    {
        GameSceneManager.ResumeLevel(ref deactivedObjects);
        GetComponent<Popup>()?.Close();
    }

    public void RestartLevel()
    {
        GameSceneManager.RestartLevel();
    }


    public void QuitLevel()
    {
        GameSceneManager.QuitLevel();
    }

}
