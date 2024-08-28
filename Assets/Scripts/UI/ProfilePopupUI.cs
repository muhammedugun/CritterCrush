using Match3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Profil popup'�n� y�neten UI s�n�f�.
/// </summary>
public class ProfilePopupUI : MonoBehaviour
{
    [SerializeField] private GameObject _Boosters;
    [SerializeField] private GameObject _inventoryBoosterPrefab;
    [SerializeField] private Sprite[] _boosterSprites;
    [SerializeField] private Sprite[] _avatarsSprites;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Text _lastLevelText, _scoreText, _levelPercentileText;
    [SerializeField] private LevelList _levelList;
    [SerializeField] private InputField _nameInputField;

    private void Start()
    {
        LifeManager.LifeLoadControl();

        UpdateAvatar();
        int boosterCount;
        int boosterAddCount = 0;
        for (int i = 0; i < 5; i++)
        {
            boosterCount = BoosterManager.GetBoosterCount(i);

            if (boosterCount > 0)
            {
                var inventoryBooster = Instantiate(_inventoryBoosterPrefab) as GameObject;
                inventoryBooster.transform.SetParent(_Boosters.transform.GetChild(0).GetChild(boosterAddCount).transform, false);
                inventoryBooster.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = boosterCount.ToString();
                inventoryBooster.transform.GetChild(2).GetComponent<Image>().sprite = _boosterSprites[i];
                boosterAddCount++;
            }
        }
        UpdateLastLevel();
        UpdateScore();
        UpdateLevelPercentile();
        UpdateName();
    }

    /// <summary>
    /// Avatar'� g�nceller.
    /// </summary>
    public void UpdateAvatar()
    {
        _avatarImage.sprite = _avatarsSprites[AvatarSelectionUI.GetCurrentAvatar()];
    }

    /// <summary>
    /// Son aktif seviyeyi al�r.
    /// </summary>
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
    /// Son seviyeyi UI'da g�nceller.
    /// </summary>
    void UpdateLastLevel()
    {
        int lastActiveLevelNumber = GetLastActiveLevelNumber();
        _lastLevelText.text = lastActiveLevelNumber.ToString();
    }

    /// <summary>
    /// Skoru UI'da g�nceller.
    /// </summary>
    void UpdateScore()
    {
        _scoreText.text = ScoreManager.GetTotalScore(_levelList.SceneCount).ToString();
    }

    /// <summary>
    /// Seviye y�zdesini UI'da g�nceller.
    /// </summary>
    private void UpdateLevelPercentile()
    {
        int lastActiveLevelNumber = GetLastActiveLevelNumber();
        int levelCount = _levelList.SceneCount;
        _levelPercentileText.text = (lastActiveLevelNumber * 100 / levelCount).ToString() + "%";
    }

    /// <summary>
    /// Kullan�c�n�n ad�n� kaydeder.
    /// </summary>
    public void SaveName()
    {
        if (!string.IsNullOrWhiteSpace(_nameInputField.text))
            PlayerPrefs.SetString("Name", _nameInputField.text);
    }

    /// <summary>
    /// Kullan�c�n�n ad�n� UI'da g�nceller.
    /// </summary>
    private void UpdateName()
    {
        _nameInputField.text = PlayerPrefs.GetString("Name", _nameInputField.text);
    }
}
