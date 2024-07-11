using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionUI : MonoBehaviour
{
    public void SetAvatar(int avatarIndex)
    {
        PlayerPrefs.SetInt("CurrentAvatarIndex", avatarIndex);
    }

    public static int GetCurrentAvatar()
    {
        return PlayerPrefs.GetInt("CurrentAvatarIndex", 0);
    }
}
