using UnityEngine;

public class AvatarSelectionUI : MonoBehaviour
{
    private ProfilePopupUI _profilePopupUI;
    private MainMenuUI _mainMenuUI;
    private void Start()
    {
        _profilePopupUI = FindObjectOfType<ProfilePopupUI>();
        _mainMenuUI = FindObjectOfType<MainMenuUI>();
    }
    public void SetAvatar(int avatarIndex)
    {
        PlayerPrefs.SetInt("CurrentAvatarIndex", avatarIndex);
        _profilePopupUI.UpdateAvatar();
        _mainMenuUI.UpdateAvatar();
    }

    public static int GetCurrentAvatar()
    {
        return PlayerPrefs.GetInt("CurrentAvatarIndex", 0);
    }
}
