using System.Collections.Generic;
using Match3;
using Ricimi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

/// <summary>
/// Reklam izleyerek kazan�lacak serbest hamle say�s�n� ve metinleri tan�mlar.
/// </summary>
public class LosePopup : MonoBehaviour
{
    [SerializeField] private Text _freeMoveCountText;
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        _scoreText.text = LevelData.Instance.CurrentScore.ToString();
    }

    public void RestartLevel()
    {
        GameSceneManager.RestartLevel();
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        LifeManager.DecreaseHealthOne();
        SceneManager.LoadScene("LevelSelection");
    }
}
