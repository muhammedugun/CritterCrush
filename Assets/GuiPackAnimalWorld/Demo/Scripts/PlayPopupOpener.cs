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
        

        private int _levelNumber;

        private int starsObtained;

        private int _levelIndex;

        protected override void Start()
        {
            base.Start();

            string input = transform.parent.name;

            string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

            if (int.TryParse(digitsOnly, out int result))
            {
                _levelNumber = result;
            }
            else
            {
                Debug.LogWarning("Level number tanýmlama baþarýsýz oldu.");
            }

            

             _levelIndex = _levelNumber -1;
             starsObtained = LevelStars.GetStars(_levelIndex);


        }

        public override void OpenPopup()
        {
            var popup = Instantiate(popupPrefab) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;
            popup.transform.SetParent(m_canvas.transform, false);

            var playPopup = popup.GetComponent<PlayPopup>();
            playPopup.playButton.scene = "Level" + _levelNumber;
            playPopup.Open();
            playPopup.SetAchievedStars(starsObtained);
            playPopup.UpdateGoals(_levelIndex);
            playPopup.levelIndex = _levelIndex;
            
        }
    }
}
