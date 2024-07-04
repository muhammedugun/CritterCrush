using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private Image _scoreBar;
    [SerializeField] private LevelList _levelList;
    [SerializeField] private Sprite _goldStarSprite;
    [SerializeField] Transform _stars;

    private void Start()
    {
        EventBus<int>.Subscribe(EventType.ScoreChanged, UpdateBar);
    }
    

    public void UpdateBar(int score)
    {

        int sceneIndex = LevelList.GetSceneIndex();

        _scoreBar.fillAmount = (score / (float)_levelList.TargetScore[0]);
        
        
        if (score >= LevelData.Instance.TargetScore * 0.33)
        {
            _stars.GetChild(0).GetComponent<Image>().sprite = _goldStarSprite;
            _stars.GetChild(0).GetComponent<Animator>().SetTrigger("Play");
        }
        if (score >= LevelData.Instance.TargetScore * 0.66)
        {
            _stars.GetChild(1).GetComponent<Image>().sprite = _goldStarSprite;
            _stars.GetChild(1).GetComponent<Animator>().SetTrigger("Play");
        }
        if (score >= LevelData.Instance.TargetScore)
        {
            _stars.GetChild(2).GetComponent<Image>().sprite = _goldStarSprite;
            _stars.GetChild(2).GetComponent<Animator>().SetTrigger("Play");
        }
    }
}
