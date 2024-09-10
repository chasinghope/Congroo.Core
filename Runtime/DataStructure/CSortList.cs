using System.Collections.Generic;
using System;
using System.Collections;

namespace Congroo.Core
{
    public class CSortList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private List<T> list = new List<T>();

        public int Count => list.Count;


        public void Add(T item)
        {
            int index = list.BinarySearch(item);
            if (index < 0)
                index = ~index;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        //IEnumerator<T> IEnumerable<T>.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}