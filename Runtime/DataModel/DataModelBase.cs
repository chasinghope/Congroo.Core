using System;
using System.Collections.Generic;
using System.Threading;

namespace Congroo.Core
{
    public class DataModelBase
    {
        protected List<EventWrapper> mEventWrappers;
        protected CancellationTokenSource mCancelTokenSource;

        public virtual void Initialize()
        {
            mCancelTokenSource = new CancellationTokenSource();
            EventCenter.Ins.BindEventWrappers(mEventWrappers);
        }


        public virtual void Release()
        {
            mCancelTokenSource.Cancel();
            EventCenter.Ins.UnbindEventWrappers(mEventWrappers);
        }

    }
}
