using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

/// <summary>
/// M�zik, SFX ve bildirim butonlar�n�n g�rsellerini ve durumlar�n� tan�mlar.
/// </summary>
public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Image _musicButtonImage, _SFXButtonImage;
    [SerializeField] private Sprite _musicMuteSprite, _musicUnMuteSprite;
    [SerializeField] private Sprite _SFXMuteSprite, _SFXUnMuteSprite;

    private MusicManager _musicManager;

    private void Start()
    {
        _musicManager = FindObjectOfType<MusicManager>();
        UpdateSFXButtonSprite();
        UpdateMusicButtonSprite();
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
    /// Sesin kapal� olup olmad���n� kontrol eder.
    /// </summary>
    /// <returns>Ses kapal�ysa true, a��ksa false d�ner.</returns>
    private bool CheckSoundMute()
    {
        return !YandexGame.savesData.soundOn;
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
        YandexGame.savesData.soundOn = false;
        YandexGame.SaveProgress();

        AudioListener.volume = 0f;
        UpdateSFXButtonSprite();
    }

    /// <summary>
    /// Ses efektlerini a��k duruma getirir ve buton g�rselini g�nceller.
    /// </summary>
    private void UnMuteSFX()
    {
        YandexGame.savesData.soundOn = true;
        YandexGame.SaveProgress();

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
        _SFXButtonImage.sprite = CheckSoundMute() ? _SFXMuteSprite : _SFXUnMuteSprite;
    }
    
}
