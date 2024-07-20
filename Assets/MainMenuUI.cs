using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _avatarsSprites;
    [SerializeField] private Image _avatarImage;
    private void Start()
    {
        UpdateAvatar();
    }
    public void UpdateAvatar()
    {
        _avatarImage.sprite = _avatarsSprites[AvatarSelectionUI.GetCurrentAvatar()];
    }
}
