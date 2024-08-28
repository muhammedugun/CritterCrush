using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Müzik, SFX ve bildirim butonlarýnýn görsellerini ve durumlarýný tanýmlar.
/// </summary>
public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Image _musicButtonImage, _SFXButtonImage, _NotificationButtonImage;
    [SerializeField] private Sprite _musicMuteSprite, _musicUnMuteSprite;
    [SerializeField] private Sprite _SFXMuteSprite, _SFXUnMuteSprite;
    [SerializeField] private Sprite _NotificationEnableSprite, _NotificationDisableSprite;

    private MusicManager _musicManager;

    private void Start()
    {
        _musicManager = FindObjectOfType<MusicManager>();
        UpdateSFXButtonSprite();
        UpdateMusicButtonSprite();
        UpdateNotificationButtonSprite();
    }

    /// <summary>
    /// Müzik butonuna týklanma durumuna göre müziði açar veya kapatýr.
    /// </summary>
    public void MusicButton()
    {
        if (MusicManager.CheckMute())
            UnMuteMusic();
        else
            MuteMusic();
    }

    /// <summary>
    /// SFX butonuna týklanma durumuna göre ses efektlerini açar veya kapatýr.
    /// </summary>
    public void SFXButton()
    {
        if (CheckSoundMute())
            UnMuteSFX();
        else
            MuteSFX();
    }

    /// <summary>
    /// Bildirim butonuna týklanma durumuna göre Android bildirimlerini açar veya kapatýr.
    /// </summary>
    public void NotificationButton()
    {
#if UNITY_ANDROID
        if (AndroidNotifications.CheckSendNotifications())
        {
            AndroidNotifications.DisableNotifications();
            _NotificationButtonImage.sprite = _NotificationDisableSprite;
            Debug.Log("Bildirim kapatýldý");
        }
        else
        {
            AndroidNotifications.EnableNotifications();
            _NotificationButtonImage.sprite = _NotificationEnableSprite;
            Debug.Log("Bildirim açýldý");
        }
#endif
    }

    /// <summary>
    /// Sesin kapalý olup olmadýðýný kontrol eder.
    /// </summary>
    /// <returns>Ses kapalýysa true, açýksa false döner.</returns>
    private bool CheckSoundMute()
    {
        return PlayerPrefs.GetFloat("SoundVolume", 1f) == 0f;
    }

    /// <summary>
    /// Müzikleri kapalý duruma getirir ve buton görselini günceller.
    /// </summary>
    private void MuteMusic()
    {
        _musicManager.MuteMusic();
        UpdateMusicButtonSprite();
    }

    /// <summary>
    /// Müzikleri açýk duruma getirir ve buton görselini günceller.
    /// </summary>
    private void UnMuteMusic()
    {
        _musicManager.UnMuteMusic();
        UpdateMusicButtonSprite();
    }

    /// <summary>
    /// Ses efektlerini kapalý duruma getirir ve buton görselini günceller.
    /// </summary>
    private void MuteSFX()
    {
        PlayerPrefs.SetFloat("SoundVolume", 0f);
        AudioListener.volume = 0f;
        UpdateSFXButtonSprite();
    }

    /// <summary>
    /// Ses efektlerini açýk duruma getirir ve buton görselini günceller.
    /// </summary>
    private void UnMuteSFX()
    {
        PlayerPrefs.SetFloat("SoundVolume", 1f);
        AudioListener.volume = 1f;
        UpdateSFXButtonSprite();
    }

    /// <summary>
    /// Müzik butonunun görselini günceller.
    /// </summary>
    private void UpdateMusicButtonSprite()
    {
        if (MusicManager.CheckMute())
            _musicButtonImage.sprite = _musicMuteSprite;
        else
            _musicButtonImage.sprite = _musicUnMuteSprite;
    }

    /// <summary>
    /// SFX butonunun görselini günceller.
    /// </summary>
    private void UpdateSFXButtonSprite()
    {
        if (CheckSoundMute())
            _SFXButtonImage.sprite = _SFXMuteSprite;
        else
            _SFXButtonImage.sprite = _SFXUnMuteSprite;
    }

    /// <summary>
    /// Bildirim butonunun görselini günceller.
    /// </summary>
    private void UpdateNotificationButtonSprite()
    {
#if UNITY_ANDROID
        if (AndroidNotifications.CheckSendNotifications())
            _NotificationButtonImage.sprite = _NotificationEnableSprite;
        else
            _NotificationButtonImage.sprite = _NotificationDisableSprite;
#endif
    }
}
