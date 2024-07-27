using GoogleMobileAds.Api;
using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        var adManager = FindObjectOfType<AdManager>();

        if (adManager.rewardedAd != null)
        {
            adManager.ShowRewardedAd((Reward reward) =>
            {
                _levelDataProvider.levelData.RemainingMove = rewardedMoveCount;
                var winLoseUI = FindObjectOfType<WinLoseUI>();
                winLoseUI.CloseLosePopup();
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
                    _levelDataProvider.levelData.RemainingMove = rewardedMoveCount;
                    var winLoseUI = FindObjectOfType<WinLoseUI>();
                    winLoseUI.CloseLosePopup();
                });
            });
        }

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
