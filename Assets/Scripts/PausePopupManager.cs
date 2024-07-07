using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopupManager : MonoBehaviour
{
    private List<GameObject> deactivedObjects;
    [SerializeField] private GameObject _areYouSure;
    [SerializeField] private Button _resumeButton, _restartButton, _quitButton;
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
    }

    public void RestartLevel()
    {
        GameSceneManager.RestartLevel();
    }

    public void OpenAreYouSure()
    {
        _areYouSure.SetActive(true);
        _resumeButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
    }

    public void CloseAreYouSure()
    {
        _areYouSure.SetActive(false);
        _resumeButton.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
    }

    public void QuitLevel()
    {
        GameSceneManager.QuitLevel();
    }

}
