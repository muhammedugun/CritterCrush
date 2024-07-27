// Copyright (C) 2015-2019 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using Match3;
using System.Linq;
using UnityEngine;

namespace Ricimi
{
    // Specialized version of the PopupOpener class that opens the PlayPopup popup
    // and sets an appropriate number of stars (that can be configured from within the
    // editor).
    public class PlayPopupOpener : PopupOpener
    {
        [HideInInspector] public int levelIndex;

        private int starsObtained;
        PlayPopup playPopup;

        protected override void Start()
        {
            base.Start();

             starsObtained = LevelStars.GetStars(levelIndex);
        }

        public override void OpenPopup()
        {
            var popup = Instantiate(popupPrefab) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;
            popup.transform.SetParent(m_canvas.transform, false);

            playPopup = popup.GetComponent<PlayPopup>();
            playPopup.playButton.scene = "Level" + (levelIndex+1).ToString();
            Debug.LogWarning("levelIndex" + levelIndex);
            playPopup.Open();
            Debug.LogWarning("starsObtained" + starsObtained);
            playPopup.SetAchievedStars(starsObtained);
            playPopup.UpdateGoals(levelIndex);
            playPopup.levelIndex = levelIndex;
            
        }

        public void ClosePopup()
        {
            playPopup.RemoveBackground();
        }

    }
}
