using UnityEngine;

/// <summary>
/// ProfilePopupUI ve MainMenuUI bile�enlerini bulur.
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
    /// Se�ilen avatar indeksini PlayerPrefs'te saklar ve ilgili UI bile�enlerini g�nceller.
    /// </summary>
    /// <param name="avatarIndex">Se�ilen avatar�n indeks numaras�.</param>
    public void SetAvatar(int avatarIndex)
    {
        PlayerPrefs.SetInt("CurrentAvatarIndex", avatarIndex);
        _profilePopupUI.UpdateAvatar();
        _mainMenuUI.UpdateAvatar();
    }

    /// <summary>
    /// Mevcut avatar indeksini al�r.
    /// </summary>
    /// <returns>Mevcut avatar�n indeks numaras�.</returns>
    public static int GetCurrentAvatar()
    {
        return PlayerPrefs.GetInt("CurrentAvatarIndex", 0);
    }
}
