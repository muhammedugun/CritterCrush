// Copyright (C) 2015-2019 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ricimi
{
    // This class handles updating the music UI widgets depending on the player's selection.
    public class MusicManager : MonoBehaviour
    {
        private Slider m_musicSlider;
        private GameObject m_musicButton;

        private void Start()
        {
            m_musicSlider = GetComponent<Slider>();
            m_musicSlider.value = YandexGame.savesData.musicOn ? 1 : 0;
            YandexGame.SaveProgress();
            m_musicButton = GameObject.Find("MusicButton/Button");
        }

        public void SwitchMusic()
        {
            var backgroundAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            backgroundAudioSource.volume = m_musicSlider.value;
            if ((int)m_musicSlider.value == 1)
                YandexGame.savesData.musicOn = true;
            else
                YandexGame.savesData.musicOn = false;
            
            YandexGame.SaveProgress();
            
            if (m_musicButton != null)
                m_musicButton.GetComponent<MusicButton>().ToggleSprite();
        }
    }
}
