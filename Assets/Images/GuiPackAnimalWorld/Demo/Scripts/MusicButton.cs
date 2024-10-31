// Copyright (C) 2015-2019 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using YG;

namespace Ricimi
{
    // This class represents the music button that is used in several places in the demo.
    // It handles the logic to enable and disable the demo's music and store the player
    // selection to PlayerPrefs.
    public class MusicButton : MonoBehaviour
    {
        private SpriteSwapper m_spriteSwapper;
        private bool m_on;

        private void Start()
        {
            m_spriteSwapper = GetComponent<SpriteSwapper>();
            m_on = YandexGame.savesData.musicOn == true;
            YandexGame.SaveProgress();
            if (!m_on)
                m_spriteSwapper.SwapSprite();
        }

        public void Toggle()
        {
            m_on = !m_on;
            var backgroundAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            backgroundAudioSource.volume = m_on ? 1 : 0;
            YandexGame.savesData.musicOn = m_on ? true : false;
            YandexGame.SaveProgress();
        }

        public void ToggleSprite()
        {
            m_on = !m_on;
            m_spriteSwapper.SwapSprite();
        }
    }
}
