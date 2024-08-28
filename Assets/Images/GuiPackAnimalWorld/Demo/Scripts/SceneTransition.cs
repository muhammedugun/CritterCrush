// Copyright (C) 2015-2019 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ricimi
{
    // This class is responsible for loading the next scene in a transition (the core of
    // this work is performed in the Transition class, though).
    public class SceneTransition : MonoBehaviour
    {
        public string scene = "<Insert scene name>";
        public float duration = 1.0f;
        public Color color = Color.black;

        private PopupOpener _popupOpener;

        public void PerformTransition()
        {
            if(IsLevelWithNumber(scene))
            {
                if(LifeManager.GetLifeCount() > 0)
                {
                    Debug.LogWarning("Hayat " + LifeManager.GetLifeCount());
                    Transition.LoadLevel(scene, duration, color);
                }
                else
                {
                    if(TryGetComponent<PopupOpener>(out _popupOpener))
                    {
                        _popupOpener.OpenPopup();
                    }
                    Debug.LogWarning("Hayat yok");
                }
            }
            else
            {
                Transition.LoadLevel(scene, duration, color);
            }
            
        }

        /// <summary>
        /// Gidilecek olan sahnenin bir Level sahnesi olup olmadýðýný kontrol eder.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLevelWithNumber(string input)
        {
            return Regex.IsMatch(input, @"^Level\d+$");
        }
    }
}
