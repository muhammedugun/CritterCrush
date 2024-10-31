using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Match3
{
    public class InitLoader : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            if (YandexGame.savesData.isPlayingFirstTime)
            {
                YandexGame.savesData.isPlayingFirstTime = false;
                YandexGame.SaveProgress();
                
                LifeManager.AddLifeCount(5);
                BoosterManager.AddBoosterCount(0, 2);
                BoosterManager.AddBoosterCount(1, 5);
                BoosterManager.AddBoosterCount(2, 5);
                BoosterManager.AddBoosterCount(3, 2);
                BoosterManager.AddBoosterCount(4, 7);
            }
        }
    }
}