using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class InitLoader : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            if (PlayerPrefs.GetInt("isPlayingFirstTime", 1) == 1)
            {
                PlayerPrefs.SetInt("isPlayingFirstTime", 0);
                LifeManager.AddLifeCount(5);
                BoosterManager.AddBoosterCount(0, 2);
                BoosterManager.AddBoosterCount(1, 5);
                BoosterManager.AddBoosterCount(2, 5);
                BoosterManager.AddBoosterCount(3, 2);
                BoosterManager.AddBoosterCount(4, 7);
            }

            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}