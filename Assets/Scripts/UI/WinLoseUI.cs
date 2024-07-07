using Match3;
using Ricimi;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private GameObject _winPopupPrefab, _losePopupPrefab;
    private PopupOpener _popupOpener;
    void Start()
    {
        _popupOpener = GetComponent<PopupOpener>();
        EventBus.Subscribe(EventType.MoveCountOver, OpenLosePopup);
        EventBus.Subscribe(EventType.AllGoalCompleted, OpenWinPopup);
    }

    private void OpenWinPopup()
    {
        _popupOpener.popupPrefab = _winPopupPrefab;
        _popupOpener.OpenPopup();
        DeactiveOtherObject();
    }

    private void OpenLosePopup()
    {
        _popupOpener.popupPrefab = _losePopupPrefab;
        _popupOpener.OpenPopup();
        DeactiveOtherObject();
    }

    /// <summary>
    /// Popupýn görülmesini engelleyen objeleri deactive eder
    /// </summary>
    public static List<GameObject> DeactiveOtherObject()
    {
        // Tüm `Gem` türündeki objeleri bul
        var objects = FindObjectsByType<Gem>(FindObjectsSortMode.None);
        var result = new List<GameObject>();

        // `Gem` türündeki objelerin hepsini devre dýþý býrak ve listeye ekle
        foreach (var obj in objects)
        {
            obj.gameObject.SetActive(false);
            result.Add(obj.gameObject);
        }

        // `Grid` türündeki objeyi bul
        var gridObject = FindObjectOfType<Grid>();

        // `Grid` objesini devre dýþý býrak ve listeye ekle
        if (gridObject != null)
        {
            gridObject.gameObject.SetActive(false);
            result.Add(gridObject.gameObject);
        }

        var hintLight = GameObject.Find("HintLight(Clone)");
        hintLight.SetActive(false);
        result.Add(hintLight.gameObject);

        // Devre dýþý býrakýlan tüm objeleri içeren listeyi döndür
        return result;
    }

    public static void ActiveOtherObject(List<GameObject> objects)
    {
        // Verilen objelerin her birini aktive et
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
