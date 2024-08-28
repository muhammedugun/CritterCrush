using GoogleMobileAds.Api;
using Match3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reklam izleyerek kazanýlacak serbest hamle sayýsýný ve metinleri tanýmlar.
/// </summary>
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

    /// <summary>
    /// Reklam izleyerek serbest hamle kazanma iþlemini baþlatýr.
    /// </summary>
    public void FreeMoveButton()
    {
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
    }

    /// <summary>
    /// Ödül olarak serbest hamle ekler ve kaybetme pop-up'ýný kapatýr.
    /// </summary>
    private void RewardMoves()
    {
        _levelDataProvider.levelData.RemainingMove = rewardedMoveCount;
        var winLoseUI = FindObjectOfType<WinLoseUI>();
        winLoseUI.CloseLosePopup();
    }

    /// <summary>
    /// Seviye baþtan baþlatýlýr.
    /// </summary>
    public void RestartLevel()
    {
        LifeManager.AddLifeCount(-1);
        GameSceneManager.RestartLevel();
    }

    /// <summary>
    /// Seviye terk edilir.
    /// </summary>
    public void QuitLevel()
    {
        LifeManager.AddLifeCount(-1);
        GameSceneManager.QuitLevel();
    }
}
