#if UNITY_ANDROID
using GoogleMobileAds.Api;
#endif
using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LosePopup : MonoBehaviour
{
    [SerializeField] private Text _freeMoveText, _freeMoveCountText;

    private LevelDataProvider _levelDataProvider;
    private int rewardedMoveCount;
    

    private void Start()
    {
        _levelDataProvider = FindObjectOfType<LevelDataProvider>();
        int maxMove = _levelDataProvider.levelList.MaxMove[LevelList.GetSceneIndex()];
        rewardedMoveCount = (int)(maxMove * 0.25);

        _freeMoveText.text = "Watch ad to continue with " + rewardedMoveCount + " moves";
        _freeMoveCountText.text = "+" + rewardedMoveCount;
    }

    public void FreeMoveButton()
    {
#if UNITY_ANDROID
        var adManager = FindObjectOfType<AdManager>();

        if (adManager.rewardedAd != null)
        {
            adManager.ShowRewardedAd((Reward reward) =>
            {
                RewardMoves();
            });
        }
        else
        {
            adManager.LoadRewardedAd((RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded ad failed to load: " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded successfully!");

                adManager.ShowRewardedAd((Reward reward) =>
                {
                    RewardMoves();
                });
            });
        }
#endif

#if UNITY_WEBGL
        YandexGame.Instance._RewardedShow(2);
#endif

    }

    private void RewardMoves()
    {
        _levelDataProvider.levelData.RemainingMove = rewardedMoveCount;
        var winLoseUI = FindObjectOfType<WinLoseUI>();
        winLoseUI.CloseLosePopup();
    }

    public static void RewardMoves(int ID)
    {
#if UNITY_WEBGL
        if (ID == 2)
        {
            var losePopup = FindObjectOfType<LosePopup>();
            losePopup.RewardMoves();
        }
#endif
    }

    public void RestartLevel()
    {
        LifeManager.AddLifeCount(-1);
        GameSceneManager.RestartLevel();
    }

    public void QuitLevel()
    {
        LifeManager.AddLifeCount(-1);
        GameSceneManager.QuitLevel();
    }

}
