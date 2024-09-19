using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Congroo.Core
{
    public partial class DataModelMgr : Singleton<DataModelMgr>
    {
        Transform transform;
        private List<Type> mDataModelList = new List<Type>();
        private Dictionary<Type, DataModelBase> mDataModelDict = new Dictionary<Type, DataModelBase>();
        
        private DataModelMgr(){}
        
        protected override void InstanceCreated()
        {
            base.InstanceCreated();
            GameObject go = new GameObject("[DataModelMgr]");
            UnityEngine.Object.DontDestroyOnLoad(go);
            transform = go.transform;
        }


        public T AddDataModel<T>() where T : DataModelBase  
        {
            Type type = typeof(T);
            if(!mDataModelList.Contains(type))
            {
                GameObject go = new GameObject(type.Name);
                go.transform.SetParent(transform);
                T value = go.AddComponent<T>();
                value.Initialize();
                mDataModelList.Add(type);
                mDataModelDict.Add(type, value);
                return value;
            }
            return null;
        }

        public void RemoveDataModel<T>() where T : DataModelBase
        {
            Type type = typeof(T);
            if(mDataModelDict.TryGetValue(type, out DataModelBase value))
            {
                value.Release();
                GameObject.Destroy(value.gameObject);
                mDataModelDict.Remove(type);
                mDataModelList.Remove(type);
            }
        }
    }
}