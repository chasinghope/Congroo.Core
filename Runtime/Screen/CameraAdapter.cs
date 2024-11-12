using UnityEngine;

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

        public float vaildAspectRatio;
        
        void Start()
        {
            mCamera = GetComponent<Camera>();
            vaildAspectRatio = VaildWidth / VaildHeight;
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
            float hWant = VaildHeight / 2f;
            if (aspectRatio <= vaildAspectRatio)
            {
                mCamera.orthographicSize = Mathf.Max(wWant, hWant);
            }
            else
            {
                mCamera.orthographicSize = Mathf.Min(wWant, hWant);
            }
            
           
        }
    }
}