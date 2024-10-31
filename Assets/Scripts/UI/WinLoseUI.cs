using Match3;
using Ricimi;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private GameObject _winPopupPrefab, _losePopupPrefab;
    
    private PopupOpener _popupOpener;
    private List<GameObject> _deactiveObjects;
    void Start()
    {
        _popupOpener = GetComponent<PopupOpener>();
        EventBus.Subscribe(EventType.MoveCountOver, OpenLosePopupInvoke);
        EventBus.Subscribe(EventType.BoosterAndSwapsOverInLevel, OpenLosePopupInvoke);
        EventBus.Subscribe(EventType.AllGoalCompleted, OpenWinPopupInvoke);
    }

    private void OpenWinPopupInvoke()
    {
        Invoke(nameof(OpenWinPopup), 1f);
       
    }

    private void OpenWinPopup()
    {
        _popupOpener.popupPrefab = _winPopupPrefab;
        if (_popupOpener.popupPrefab)
        {
            _popupOpener.OpenPopup();
            DeactiveOtherObject();
        }
    }


    private void OpenLosePopupInvoke()
    {
        Invoke(nameof(OpenLosePopup), 1f);

    }

    private void OpenLosePopup()
    {
        _popupOpener.popupPrefab = _losePopupPrefab;
        if (_popupOpener.popupPrefab == null)
        {
            _popupOpener.OpenPopup();
            _deactiveObjects = DeactiveOtherObject();
        }
    }


    public void CloseLosePopup()
    {
        _popupOpener.ClosePopup();
        ActiveOtherObject(_deactiveObjects);

    }

    /// <summary>
    /// Popup�n g�r�lmesini engelleyen objeleri deactive eder
    /// </summary>
    public static List<GameObject> DeactiveOtherObject()
    {
        // T�m `Gem` t�r�ndeki objeleri bul
        var objects = FindObjectsByType<Gem>(FindObjectsSortMode.None);
        var result = new List<GameObject>();

        // `Gem` t�r�ndeki objelerin hepsini devre d��� b�rak ve listeye ekle
        foreach (var obj in objects)
        {
            obj.gameObject.SetActive(false);
            result.Add(obj.gameObject);
        }

        // `Grid` t�r�ndeki objeyi bul
        var gridObject = FindObjectOfType<Grid>();

        // `Grid` objesini devre d��� b�rak ve listeye ekle
        if (gridObject != null)
        {
            gridObject.gameObject.SetActive(false);
            result.Add(gridObject.gameObject);
        }

        var hintLight = GameObject.Find("HintLight(Clone)");
        if(hintLight!=null)
        {
            hintLight.SetActive(false);
            result.Add(hintLight.gameObject);
        }
            
        // Devre d��� b�rak�lan t�m objeleri i�eren listeyi d�nd�r
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
