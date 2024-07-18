using Match3;
using UnityEngine;
using UnityEngine.UI;

public class InLevelGoalUI : MonoBehaviour
{
    public GameObject _goals;
    private LevelList _levelList;
    [SerializeField] private bool _isWinPopUp;
    private GameObject _otherGoals;

    private void Start()
    {
        if (_isWinPopUp)
        {
            _otherGoals = GameObject.Find("Manager").GetComponent<InLevelGoalUI>()._goals;
        }
        _levelList = FindObjectOfType<LevelDataProvider>().levelList;
        UpdateGoalSprite();
        EventBus<int>.Subscribe(EventType.GoalCountChanged, UpdateGoalCount);
        
        
    }

    public void UpdateGoalSprite()
    {
        int levelIndex = LevelList.GetSceneIndex();

        for (int i = 0; i < _goals.transform.childCount; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(false);
        }


        int length = _levelList.Goals[levelIndex].Goals.Length;

        for (int i = 0; i < length; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(true);
            var image = _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>();
            image.sprite = _levelList.Goals[levelIndex].Goals[i].Gem.UISprite;

            _goals.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "/"+_levelList.Goals[levelIndex].Goals[i].Count.ToString();
            if(_isWinPopUp)
            {
                _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = _otherGoals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text;
                _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite = _otherGoals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite;
            }
        }
    }

    public void UpdateGoalCount(int count)
    {
        int levelIndex = LevelList.GetSceneIndex();
        int length = _levelList.Goals[levelIndex].Goals.Length;

        for (int i = 0; i < length; i++)
        {
            int currentCount = _levelList.Goals[levelIndex].Goals[i].Count - count;
            _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = currentCount.ToString();


        }
    }


}
