using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounterUI : MonoBehaviour
{
    [SerializeField] private Text _moveCountText;
    [SerializeField] private LevelList _levelList;

    void Start()
    {
        EventBus<int>.Subscribe(EventType.Moved, UpdateMoveCount);
        _moveCountText.text = _levelList.MaxMove[LevelList.GetSceneIndex()].ToString();
    }

    
    private void UpdateMoveCount(int remainingMoveCount)
    {
        _moveCountText.text = remainingMoveCount.ToString();
    }
}
