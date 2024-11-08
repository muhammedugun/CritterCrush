using System.Collections;
using UnityEngine;

namespace Ricimi
{
    // This class is responsible for creating and opening a popup of the given prefab and add
    // it to the UI canvas of the current scene.
    public class PopupOpener : MonoBehaviour
    {
        public GameObject popupPrefab;
        
        private GameObject popup;
        
        protected Canvas m_canvas;

        protected virtual void Start()
        {
            m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        public virtual void OpenPopup()
        {
            popup = Instantiate(popupPrefab, m_canvas.transform, false) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;
            popup.GetComponent<Popup>().Open();
        }


        public void ClosePopup()
        {
            popup.GetComponent<Popup>().RemoveBackground();
        }
    }
}