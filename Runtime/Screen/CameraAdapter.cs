using UnityEngine;
using UnityEngine.Serialization;

namespace Congroo.Core
{
    public class CameraAdapter : MonoBehaviour
    {
        public float VaildWidth;
        public float VaildHeight;
        private Camera mCamera;

        public float aspectRatio;
        public float w;
        public float h;
        public float curWidth = 0f;

        public bool IsOnce = false;
        public bool IsRefreshed = false;
        void Start()
        {
            mCamera = GetComponent<Camera>();
            Refresh();
        }

        private void Update()
        {
            if (IsOnce)
            {
                if (!IsRefreshed)
                {
                    Refresh();
                    IsRefreshed = true;
                }
            }
            else
            {
                Refresh();
            }
        
        }
        
        private void Refresh()
        {
            w = Screen.width;
            h = Screen.height;
            aspectRatio = w * 1f / h ;
            float wWant = VaildWidth / aspectRatio / 2f;
            mCamera.orthographicSize = Mathf.Max(wWant, VaildHeight / 2f);
        }
    }
}