using Match3;
using Ricimi;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

/// <summary>
/// Seviye seçim menüsündeki bileşenleri tanımlar.
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
        
        YandexGame.savesData.openLevels[1] = true;
        YandexGame.SaveProgress();

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
    /// Her seviyenin yıldız say s n  g nceller.
    /// </summary>
    private void UpdateLevelStars()
    {
        int levelStarCount;

        for (int i = 0; i < _levelList.SceneCount; i++)
        {
            if (IsLevelActive(i))
            {
                levelStarCount = LevelStars.GetStars(i);
                for (int j = 0; j < levelStarCount; j++)
                {
                    _levels.transform.GetChild(i).GetChild(1).GetChild(j + 1).GetChild(2).gameObject.SetActive(true);
                    _levels.transform.GetChild(i).GetChild(1).GetChild(j + 1).GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Aktif seviyeleri g nceller.
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
    /// Her seviyenin numaras n  ayarlar.
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
    /// Bir seviyenin aktif olup olmad   n  kontrol eder.
    /// </summary>
    /// <returns>Seviyenin aktif olup olmad   n  belirtir.</returns>
    private bool IsLevelActive(int levelIndex)
    {
        return YandexGame.savesData.openLevels[levelIndex + 1];
    }

    /// <summary>
    /// Son aktif seviyeyi g nceller.
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
    /// Son aktif seviye numaras n  al r.
    /// </summary>
    /// <returns>Son aktif seviye numaras .</returns>
    private int GetLastActiveLevelNumber()
    {
        for (int i = 1; i <= _levelList.SceneCount; i++)
        {
            if (!YandexGame.savesData.openLevels[i])
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
