using Match3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopupUI : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite _grayStar;
    void Start()
    {
        _scoreText.text = LevelData.Instance.CurrentScore.ToString();
        
        string input = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(input.Substring("Level".Length));
            
        switch (LevelStars.GetStars(levelNumber-1))
        {
            case 0:
                _stars[0].sprite = _grayStar;
                _stars[1].sprite = _grayStar;
                _stars[2].sprite = _grayStar;
                break;
            case 1:
                _stars[1].sprite = _grayStar;
                _stars[2].sprite = _grayStar;
                break;
            case 2:
                _stars[2].sprite = _grayStar;
                break;
        }

        
    }

    public void RestartLevel()
    {
        GameSceneManager.RestartLevel();
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    public void LoadNextLevel()
    {
        GameSceneManager.LoadNextLevel();
    }

}
