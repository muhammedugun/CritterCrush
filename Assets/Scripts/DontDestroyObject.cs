using System;
using UnityEngine;

namespace Match3
{
    public class DontDestroyObject : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}