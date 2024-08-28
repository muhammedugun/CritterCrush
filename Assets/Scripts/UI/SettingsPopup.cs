using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// M�zik, SFX ve bildirim butonlar�n�n g�rsellerini ve durumlar�n� tan�mlar.
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
    /// M�zik butonuna t�klanma durumuna g�re m�zi�i a�ar veya kapat�r.
    /// </summary>
    public void MusicButton()
    {
        if (MusicManager.CheckMute())
            UnMuteMusic();
        else
            MuteMusic();
    }

    /// <summary>
    /// SFX butonuna t�klanma durumuna g�re ses efektlerini a�ar veya kapat�r.
    /// </summary>
    public void SFXButton()
    {
        if (CheckSoundMute())
            UnMuteSFX();
        else
            MuteSFX();
    }

    /// <summary>
    /// Bildirim butonuna t�klanma durumuna g�re Android bildirimlerini a�ar veya kapat�r.
    /// </summary>
    public void NotificationButton()
    {
#if UNITY_ANDROID
        if (AndroidNotifications.CheckSendNotifications())
        {
            AndroidNotifications.DisableNotifications();
            _NotificationButtonImage.sprite = _NotificationDisableSprite;
            Debug.Log("Bildirim kapat�ld�");
        }
        else
        {
            AndroidNotifications.EnableNotifications();
            _NotificationButtonImage.sprite = _NotificationEnableSprite;
            Debug.Log("Bildirim a��ld�");
        }
#endif
    }

    /// <summary>
    /// Sesin kapal� olup olmad���n� kontrol eder.
    /// </summary>
    /// <returns>Ses kapal�ysa true, a��ksa false d�ner.</returns>
    private bool CheckSoundMute()
    {
        return PlayerPrefs.GetFloat("SoundVolume", 1f) == 0f;
    }

    /// <summary>
    /// M�zikleri kapal� duruma getirir ve buton g�rselini g�nceller.
    /// </summary>
    private void MuteMusic()
    {
        _musicManager.MuteMusic();
        UpdateMusicButtonSprite();
    }

    /// <summary>
    /// M�zikleri a��k duruma getirir ve buton g�rselini g�nceller.
    /// </summary>
    private void UnMuteMusic()
    {
        _musicManager.UnMuteMusic();
        UpdateMusicButtonSprite();
    }

    /// <summary>
    /// Ses efektlerini kapal� duruma getirir ve buton g�rselini g�nceller.
    /// </summary>
    private void MuteSFX()
    {
        PlayerPrefs.SetFloat("SoundVolume", 0f);
        AudioListener.volume = 0f;
        UpdateSFXButtonSprite();
    }

    /// <summary>
    /// Ses efektlerini a��k duruma getirir ve buton g�rselini g�nceller.
    /// </summary>
    private void UnMuteSFX()
    {
        PlayerPrefs.SetFloat("SoundVolume", 1f);
        AudioListener.volume = 1f;
        UpdateSFXButtonSprite();
    }

    /// <summary>
    /// M�zik butonunun g�rselini g�nceller.
    /// </summary>
    private void UpdateMusicButtonSprite()
    {
        if (MusicManager.CheckMute())
            _musicButtonImage.sprite = _musicMuteSprite;
        else
            _musicButtonImage.sprite = _musicUnMuteSprite;
    }

    /// <summary>
    /// SFX butonunun g�rselini g�nceller.
    /// </summary>
    private void UpdateSFXButtonSprite()
    {
        if (CheckSoundMute())
            _SFXButtonImage.sprite = _SFXMuteSprite;
        else
            _SFXButtonImage.sprite = _SFXUnMuteSprite;
    }

    /// <summary>
    /// Bildirim butonunun g�rselini g�nceller.
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
