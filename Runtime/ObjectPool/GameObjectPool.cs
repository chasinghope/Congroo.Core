using System;
using System.Collections.Generic;
using UnityEngine;

namespace Congroo.Core
{
    public class GameObjectPool
    {
        Stack<GameObject> mFreeStack = new Stack<GameObject>();

        private Action<GameObject> mAllocateAction;
        private Action<GameObject> mFreeAction;

        private Transform mParent;
        private GameObject mPrefab;

        public GameObjectPool(GameObject rPrefab, Transform rParent, int rCount = 0)
        {
            if (rPrefab == null)
            {
                throw new ArgumentException($"rPrefab is null");
            }

            mPrefab = rPrefab;
            mParent = rParent;
            if (mParent == null)
            {
                mParent = new GameObject($"pool ->  {mPrefab.name}").transform;
            }
            for (int i = 0; i < rCount; i++)
            {
                GameObject gameObj = CreateGameObject();
                gameObj.SetActive(false);
                gameObj.transform.SetParent(mParent);
                mFreeStack.Push(gameObj);
            }
        }

        public GameObject Allocate()
        {
            GameObject obj = null;
            while (obj == null)
            {
                obj = mFreeStack.Count > 0 ? mFreeStack.Pop() : CreateGameObject();
            }
            obj.transform.SetParent(null);
            obj.SetActive(true);
            mAllocateAction?.Invoke(obj);
            return obj;
        }


        public T Allocate<T>() where T : MonoBehaviour
        {
            return Allocate().GetComponent<T>();
        }


        public void Free(GameObject rObjectUnit)
        {
            rObjectUnit.SetActive(false);
            rObjectUnit.transform.SetParent(mParent);
            mFreeStack.Push(rObjectUnit);
        }
        

        private GameObject CreateGameObject()
        {
            GameObject gameObj = GameObject.Instantiate(mPrefab);
            return gameObj;
        }
    }


    public class GameObjectPool<T> where T : MonoBehaviour, IObjectUnit
    {
        Stack<T> mFreeStack = new Stack<T>();
        
        private Transform mParent;
        private T mPrefab;
        
        public GameObjectPool(GameObject rPrefab, Transform rParent, int nCount = 0)
        {
            if (rPrefab == null)
            {
                throw new ArgumentException($"rPrefab is null");
            }
            
            mPrefab = rPrefab.GetComponent<T>();
            mParent = rParent;
            if (mParent == null)
            {
                mParent = new GameObject($"pool ->  {mPrefab.name}").transform;
            }
            for (int i = 0; i < nCount; i++)
            {
                T gameObj = CreateGameObject();
                gameObj.gameObject.SetActive(false);
                gameObj.IsUsed = false;
                gameObj.transform.SetParent(mParent);
                mFreeStack.Push(gameObj);
            }
        }


        public T Allocate()
        {
            T obj = null;
            while (obj == null)
            {
                obj = mFreeStack.Count > 0 ? mFreeStack.Pop() : CreateGameObject();
            }

            
            obj.IsUsed = true;
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            obj.PoolGet();
            
            return obj;
        }

        public void Free(T rObjectUnit)
        {
            if (!rObjectUnit.IsUsed)
                return;
            
            rObjectUnit.gameObject.SetActive(false);
            rObjectUnit.IsUsed = false;
            rObjectUnit.transform.SetParent(mParent);
            mFreeStack.Push(rObjectUnit);
        }
        
        private T CreateGameObject()
        {
            GameObject gameObj = GameObject.Instantiate(mPrefab.gameObject);
            return gameObj.GetComponent<T>();
        }


    }
}
