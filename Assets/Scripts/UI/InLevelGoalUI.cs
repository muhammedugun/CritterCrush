using System;
using DG.Tweening;
using Match3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Oyun i�i hedeflerin UI'da y�netilmesini sa�layan s�n�f.
/// </summary>
public class InLevelGoalUI : MonoBehaviour
{
    public GameObject _goals;
    private LevelData _leveldata;
    [SerializeField] private bool _isWinPopUp;
    private GameObject _otherGoals;

    public GameObject gemUIObject;
    public float duration;
    public Canvas canvas; // Eklemeniz gereken Canvas referans�

    private void Start()
    {
        if (_isWinPopUp)
        {
            _otherGoals = GameObject.Find("Manager").GetComponent<InLevelGoalUI>()._goals;
        }

        _leveldata = FindObjectOfType<LevelDataProvider>().levelData;

        
        EventBus<Gem, int>.Subscribe(EventType.GoalCountChanged, OnMatch);

        UpdateGoalSprite();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.GoalCountChanged, UpdateGoalCount);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GoalCountChanged, UpdateGoalCount);
    }

    /// <summary>
    /// Hedef sprite'lar�n� g�nceller.
    /// </summary>
    public void UpdateGoalSprite()
    {
        // Mevcut hedefleri gizle
        for (int i = 0; i < _goals.transform.childCount; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(false);
        }

        int length = _leveldata.Goals.Length;

        // Hedefleri g�ncelle
        for (int i = 0; i < length; i++)
        {
            _goals.transform.GetChild(i).gameObject.SetActive(true);
            var image = _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>();
            image.sprite = _leveldata.Goals[i].Gem.UISprite;

            _goals.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "/" + _leveldata.Goals[i].TargetCount.ToString();
            if (_isWinPopUp)
            {
                _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = _otherGoals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text;
                _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite = _otherGoals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite;
            }
        }
    }

    /// <summary>
    /// Hedef say�s�n� g�nceller.
    /// </summary>
    public void UpdateGoalCount()
    {
        int length = _leveldata.Goals.Length;

        for (int i = 0; i < length; i++)
        {
            int currentCount = _leveldata.Goals[i].TargetCount - _leveldata.levelGoalCounts[i];
            _goals.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = currentCount.ToString();
        }
    }

    /// <summary>
    /// E�le�me oldu�unda �a�r�lan fonksiyon.
    /// </summary>
    public void OnMatch(Gem gem, int matchCount)
    {
        Vector3 matchWorldPosition = gem.transform.position;

        Vector2 matchCanvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Camera.main.WorldToScreenPoint(matchWorldPosition), Camera.main, out matchCanvasPosition);

        GameObject newItem = Instantiate(gemUIObject, canvas.transform);
        newItem.GetComponent<Image>().sprite = gem.UISprite;

        RectTransform newItemRectTransform = newItem.GetComponent<RectTransform>();
        newItemRectTransform.anchoredPosition = matchCanvasPosition;

        Vector2 goalCanvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Camera.main.WorldToScreenPoint(GetWorldPosition(gem.UISprite)), Camera.main, out goalCanvasPosition);

        float tweenDelay = Random.Range(0f, 0.5f);
        newItemRectTransform.DOAnchorPos(goalCanvasPosition, duration).SetDelay(tweenDelay);
        newItemRectTransform.DOScale(Vector3.one * 0.5f, duration).SetDelay(tweenDelay).OnComplete(() =>
        {
            Destroy(newItem);
        });
    }

    /// <summary>
    /// Sprite i�in d�nya pozisyonunu al�r.
    /// </summary>
    Vector3 GetWorldPosition(Sprite sprite)
    {
        int length = _leveldata.Goals.Length;

        RectTransform rectTransform = new RectTransform();

        for (int i = 0; i < length; i++)
        {
            var image = _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>();
            if (image.sprite == sprite)
            {
                rectTransform = _goals.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<RectTransform>();
                break;
            }
        }

        // RectTransform'un pivot pozisyonunu d�nya koordinatlar�na �evir
        Vector3 worldPos = rectTransform.TransformPoint(rectTransform.rect.center);
        return worldPos;
    }
}
