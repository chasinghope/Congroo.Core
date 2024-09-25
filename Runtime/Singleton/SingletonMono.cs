using UnityEngine;


namespace Congroo.Core
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        protected static T mInstance;
        public static T Ins => mInstance;

        protected virtual void Awake()
        {
            if (mInstance == null)
                mInstance = gameObject.GetComponent<T>();
        }

        protected virtual void OnDestroy()
        {
            mInstance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            Release();
        }


        public void Release()
        {
            if (mInstance != null)
            {
                Destroy(gameObject);
                mInstance = null;
            }
        }


    }
}
