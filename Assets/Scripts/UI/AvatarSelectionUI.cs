using UnityEngine;

/// <summary>
/// ProfilePopupUI ve MainMenuUI bileþenlerini bulur.
/// </summary>
public class AvatarSelectionUI : MonoBehaviour
{
    private ProfilePopupUI _profilePopupUI;
    private MainMenuUI _mainMenuUI;

    private void Start()
    {
        _profilePopupUI = FindObjectOfType<ProfilePopupUI>();
        _mainMenuUI = FindObjectOfType<MainMenuUI>();
    }

    /// <summary>
    /// Seçilen avatar indeksini PlayerPrefs'te saklar ve ilgili UI bileþenlerini günceller.
    /// </summary>
    /// <param name="avatarIndex">Seçilen avatarýn indeks numarasý.</param>
    public void SetAvatar(int avatarIndex)
    {
        PlayerPrefs.SetInt("CurrentAvatarIndex", avatarIndex);
        _profilePopupUI.UpdateAvatar();
        _mainMenuUI.UpdateAvatar();
    }

    /// <summary>
    /// Mevcut avatar indeksini alýr.
    /// </summary>
    /// <returns>Mevcut avatarýn indeks numarasý.</returns>
    public static int GetCurrentAvatar()
    {
        return PlayerPrefs.GetInt("CurrentAvatarIndex", 0);
    }
}
