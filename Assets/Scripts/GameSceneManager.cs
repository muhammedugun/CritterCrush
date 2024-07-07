using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static void PauseLevel(ref List<GameObject> deactivedObjects)
    {
        deactivedObjects = WinLoseUI.DeactiveOtherObject();
        Time.timeScale = 0f;
    }
    public static void ResumeLevel(ref List<GameObject> deactivedObjects)
    {
        Time.timeScale = 1f;
        WinLoseUI.ActiveOtherObject(deactivedObjects);
    }

    public static void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void QuitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    public static void LoadNextLevel()
    {
        Time.timeScale = 1f;
        string sceneName = SceneManager.GetActiveScene().name;
        int nextLevelNumber = int.Parse(sceneName.Substring(5, sceneName.Length - 5)) + 1;
        SceneManager.LoadScene("Level"+nextLevelNumber);
    }
}
