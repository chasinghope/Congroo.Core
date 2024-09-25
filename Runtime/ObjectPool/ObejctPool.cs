using System.Collections.Generic;

namespace Congroo.Core
{
    public class ObjectPool<T> where T : IObjectUnit, new()
    {
        Stack<T> mFreeStack = new Stack<T>();

        public ObjectPool(int nCount = 0)
        {
            for (int i = 0; i < nCount; i++)
            {
                mFreeStack.Push(new T());
            }
        }

        public T Allocate()
        {
            T obj = mFreeStack.Count > 0 ? mFreeStack.Pop() : new T();
            obj.IsUsed = true;
            obj.PoolGet();
            return obj;
        }

        public void Free(T rObjectUnit)
        {
            if (!rObjectUnit.IsUsed) return;
            rObjectUnit.IsUsed = false;
            rObjectUnit.PoolFree();
            mFreeStack.Push(rObjectUnit);
        }
    }
}
