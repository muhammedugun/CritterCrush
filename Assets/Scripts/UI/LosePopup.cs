using Match3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reklam izleyerek kazan�lacak serbest hamle say�s�n� ve metinleri tan�mlar.
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
    /// Reklam izleyerek serbest hamle kazanma i�lemini ba�lat�r.
    /// </summary>
    public void FreeMoveButton()
    {
        /*
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
        }*/
    }

    /// <summary>
    /// �d�l olarak serbest hamle ekler ve kaybetme pop-up'�n� kapat�r.
    /// </summary>
    private void RewardMoves()
    {
        _levelDataProvider.levelData.RemainingMove = rewardedMoveCount;
        var winLoseUI = FindObjectOfType<WinLoseUI>();
        winLoseUI.CloseLosePopup();
    }

    /// <summary>
    /// Seviye ba�tan ba�lat�l�r.
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
