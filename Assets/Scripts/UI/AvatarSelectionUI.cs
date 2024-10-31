using UnityEngine;
using YG;

/// <summary>
/// ProfilePopupUI ve MainMenuUI bile�enlerini bulur.
/// </summary>
public class AvatarSelectionUI : MonoBehaviour
{
    private ProfilePopupUI _profilePopupUI;
    private AvatarUpdater _avatarUpdater;

    private void Start()
    {
        _profilePopupUI = FindObjectOfType<ProfilePopupUI>();
        _avatarUpdater = FindObjectOfType<AvatarUpdater>();
    }

    /// <summary>
    /// Se�ilen avatar indeksini PlayerPrefs'te saklar ve ilgili UI bile�enlerini g�nceller.
    /// </summary>
    /// <param name="avatarIndex">Se�ilen avatar�n indeks numaras�.</param>
    public void SetAvatar(int avatarIndex)
    {
        YandexGame.savesData.currentAvatarIndex = avatarIndex;
        YandexGame.SaveProgress();
        _profilePopupUI.UpdateAvatar();
        _avatarUpdater.UpdateAvatar();
    }

    /// <summary>
    /// Mevcut avatar indeksini al�r.
    /// </summary>
    /// <returns>Mevcut avatar�n indeks numaras�.</returns>
    public static int GetCurrentAvatar()
    {
        return YandexGame.savesData.currentAvatarIndex;
    }
}
