using Match3;
using UnityEngine;
using UnityEngine.UI;

public class InLevelGoalUI : MonoBehaviour
{
    public GameObject _goals;
    private LevelData _leveldata;
    [SerializeField] private bool _isWinPopUp;
    private GameObject _otherGoals;

    private void Start()
    {
        if (_isWinPopUp)
        {
            _otherGoals = GameObject.Find("Manager").GetComponent<InLevelGoalUI>()._goals;
        }

        _leveldata = FindObjectOfType<LevelDataProvider>().levelData;

        EventBus.Subscribe(EventType.GoalCountChanged, UpdateGoalCount);

        UpdateGoalSprite();
        
    }

    public void UpdateGoalSprite()
    {

        for (int i = 0; i < _goals.transform.childCount; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(false);
        }


        int length = _leveldata.Goals.Length;

        for (int i = 0; i < length; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(true);
            var image = _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>();
            image.sprite = _leveldata.Goals[i].Gem.UISprite;

            _goals.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "/" + _leveldata.levelGoalCounts[i].ToString();
            if (_isWinPopUp)
            {
                _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = _otherGoals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text;
                _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite = _otherGoals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite;
            }
        }
    }

        public void UpdateGoalCount()
        {
            int length = _leveldata.Goals.Length;

            for (int i = 0; i < length; i++)
            {

                int currentCount = _leveldata.Goals[i].TargetCount - _leveldata.levelGoalCounts[i];
                _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = currentCount.ToString();


            }
        }

    
}
