using Match3;
using UnityEngine;
using UnityEngine.UI;

public class WinPopupUI : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    
    void Start()
    {
        _scoreText.text = LevelData.Instance.CurrentScore.ToString();
    }

    public void RestartLevel()
    {
        GameSceneManager.RestartLevel();
    }

    public void QuitLevel()
    {
        GameSceneManager.QuitLevel();
    }

    public void LoadNextLevel()
    {
        GameSceneManager.LoadNextLevel();
    }

}
