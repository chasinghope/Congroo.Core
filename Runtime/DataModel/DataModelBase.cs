using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Congroo.Core
{
    public class DataModelBase : MonoBehaviour
    {
        protected CancellationTokenSource CancelTokenSource;
        protected List<EventWrapper> mEventWrappers;
        public virtual void Initialize()
        {
            CancelTokenSource = new CancellationTokenSource();
            mEventWrappers = EventCenter.GetTypeEvents(this);
            EventCenter.Ins.BindEventWrappers(mEventWrappers);
        }


        public virtual void Release()
        {
            CancelTokenSource.Cancel();
            EventCenter.Ins.UnbindEventWrappers(mEventWrappers);
        }
    }
}