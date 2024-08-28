using Match3;
using Ricimi;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Seviye seçim menüsündeki bileþenleri tanýmlar.
/// </summary>
public class LevelSelectionMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject _levels;
    [SerializeField] private LevelList _levelList;
    [SerializeField] private GameObject _activeLevelPrefab, _lastActiveLevelPrefab;
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private float[] _levelsFocusPosY;

    private void Start()
    {
        LifeManager.LifeLoadControl();

        PlayerPrefs.SetInt("isLevel" + 1 + "Active", 1);

        UpdateActiveLevels();
        UpdateLastActiveLevel();

        StartCoroutine(ExecuteNextFrame());

        SetCameraPosition();
    }

    private IEnumerator ExecuteNextFrame()
    {
        yield return null;

        UpdateLevelStars();
        SetLevelNumber();
    }

    /// <summary>
    /// Her seviyenin yýldýz sayýsýný günceller.
    /// </summary>
    private void UpdateLevelStars()
    {
        int levelStarCount;

        for (int i = 0; i < _levelList.SceneCount; i++)
        {
            if (IsLevelActive(i))
            {
                Debug.LogWarning("Level " + i + "Active");
                levelStarCount = LevelStars.GetStars(i);
                Debug.LogWarning("levelStarCount " + levelStarCount);
                for (int j = 0; j < levelStarCount; j++)
                {
                    _levels.transform.GetChild(i).GetChild(1).GetChild(j + 1).GetChild(3).gameObject.SetActive(true);
                    _levels.transform.GetChild(i).GetChild(1).GetChild(j + 1).GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Aktif seviyeleri günceller.
    /// </summary>
    private void UpdateActiveLevels()
    {
        for (int i = 0; i < _levelList.SceneCount; i++)
        {
            bool isLevelActive = IsLevelActive(i);

            if (isLevelActive)
            {
                _levels.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

                var activeLevelPrefab = Instantiate(_activeLevelPrefab) as GameObject;
                activeLevelPrefab.GetComponentInChildren<PlayPopupOpener>().levelIndex = i;
                activeLevelPrefab.transform.SetParent(_levels.transform.GetChild(i).transform, false);
                activeLevelPrefab.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Her seviyenin numarasýný ayarlar.
    /// </summary>
    private void SetLevelNumber()
    {
        for (int i = 0; i < _levelList.SceneCount; i++)
        {
            if (IsLevelActive(i))
            {
                _levels.transform.GetChild(i).GetChild(1).GetComponentInChildren<Text>().text = (i + 1).ToString();
            }
        }
    }

    /// <summary>
    /// Bir seviyenin aktif olup olmadýðýný kontrol eder.
    /// </summary>
    /// <param name="levelIndex">Seviye indeksi.</param>
    /// <returns>Seviyenin aktif olup olmadýðýný belirtir.</returns>
    private bool IsLevelActive(int levelIndex)
    {
        int boolValue = PlayerPrefs.GetInt("isLevel" + (levelIndex + 1) + "Active", 0);

        if (boolValue == 1)
            return true;
        else if (boolValue == 0)
            return false;
        else
        {
            Debug.LogWarning("Geçersiz Deðer!");
            return false;
        }
    }

    /// <summary>
    /// Son aktif seviyeyi günceller.
    /// </summary>
    private void UpdateLastActiveLevel()
    {
        for (int i = 0; i < _levelList.SceneCount; i++)
        {
            if (!IsLevelActive(i))
            {
                _levels.transform.GetChild(i - 1).GetChild(0).gameObject.SetActive(false);

                int childCount = _levels.transform.GetChild(i - 1).childCount;

                if (childCount > 1)
                {
                    Destroy(_levels.transform.GetChild(i - 1).GetChild(1).gameObject);
                }

                var lastActiveLevel = Instantiate(_lastActiveLevelPrefab) as GameObject;
                lastActiveLevel.GetComponentInChildren<PlayPopupOpener>().levelIndex = i - 1;
                lastActiveLevel.transform.SetParent(_levels.transform.GetChild(i - 1).transform, false);
                lastActiveLevel.GetComponentInChildren<Text>().text = i.ToString();
                lastActiveLevel.SetActive(true);
                break;
            }
        }
    }

    /// <summary>
    /// Son aktif seviye numarasýný alýr.
    /// </summary>
    /// <returns>Son aktif seviye numarasý.</returns>
    private int GetLastActiveLevelNumber()
    {
        for (int i = 1; i <= _levelList.SceneCount; i++)
        {
            if (PlayerPrefs.GetInt("isLevel" + i + "Active", 0) == 0)
                return i - 1;
        }
        return _levelList.SceneCount;
    }

    /// <summary>
    /// Kamera pozisyonunu ayarlar.
    /// </summary>
    private void SetCameraPosition()
    {
        int lastActiveLevelNumber = GetLastActiveLevelNumber();
        float lastActiveLevelHeight = _levelsFocusPosY[lastActiveLevelNumber - 1];
        _contentTransform.localPosition = new Vector3(_contentTransform.position.x, lastActiveLevelHeight, _contentTransform.position.z);
    }
}
