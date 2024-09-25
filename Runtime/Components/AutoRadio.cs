using System;
using UnityEngine;

namespace Congroo.Core
{
    public class AutoRadio : MonoBehaviour
    {
        public Vector2 Radio;
        private void Start()
        {
            int targetWidth = Mathf.CeilToInt(Screen.currentResolution.height * Radio.x / Radio.y);
            Screen.SetResolution(targetWidth, Screen.currentResolution.height, false);
            Debug.Log("[AutoRadio] Work...");
        }
    }
}