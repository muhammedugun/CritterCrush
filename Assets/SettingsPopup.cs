using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Image _musicButtonImage, _SFXButtonImage;
    [SerializeField] private Sprite _musicMuteSprite, _musicUnMuteSprite;
    [SerializeField] private Sprite _SFXMuteSprite, _SFXUnMuteSprite;

    private MusicManager _musicManager;
    

    void Start()
    {
        _musicManager = FindObjectOfType<MusicManager>();
        AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        UpdateSFXButtonSprite();
        UpdateMusicButtonSprite();
    }

    public void MusicButton()
    {
        if (MusicManager.CheckMute())
            UnMuteMusic();
        else
            MuteMusic();
    }
    public void SFXButton()
    {
        if (CheckSoundMute())
            UnMuteSFX();
        else
            MuteSFX();
    }

    private bool CheckSoundMute()
    {
        if (PlayerPrefs.GetFloat("SoundVolume", 1f) == 0f)
            return true;
        else
            return false;
    }

    private void MuteMusic()
    {
        _musicManager.MuteMusic();
        UpdateMusicButtonSprite();
    }

    private void UnMuteMusic()
    {
        _musicManager.UnMuteMusic();
        UpdateMusicButtonSprite();
    }

    private void MuteSFX()
    {
        PlayerPrefs.SetFloat("SoundVolume", 0f);
        AudioListener.volume = 0f;
        UpdateSFXButtonSprite();
    }

    private void UnMuteSFX()
    {
        PlayerPrefs.SetFloat("SoundVolume", 1f);
        AudioListener.volume = 1f;
        UpdateSFXButtonSprite();
    }

    private void UpdateMusicButtonSprite()
    {
        if (MusicManager.CheckMute())
            _musicButtonImage.sprite = _musicMuteSprite;
        else
            _musicButtonImage.sprite = _musicUnMuteSprite;
    }


    private void UpdateSFXButtonSprite()
    {
        if (CheckSoundMute())
            _SFXButtonImage.sprite = _SFXMuteSprite;
        else
            _SFXButtonImage.sprite = _SFXUnMuteSprite;
    }

    
}
