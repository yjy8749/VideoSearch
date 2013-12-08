using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace VideoSearch
{
    class CycleList<T>
    {
        public List<T> list = new List<T>();
        private int size = 0;
        private int now = 0;
        public CycleList(int size)
        {
            this.size = size;
        }
        public void add(T t)
        {
            if (list.Count == size)
            {
                list.RemoveAt(0);
            }
            now = list.Count;
            list.Add(t);
        }
        public T getNext()
        {
            if (list.Count == 0) return default(T);
            now = now+1 < list.Count?now+1:now;
            return list[now];
        }
        public T getLast()
        {
            if (list.Count == 0) return default(T);
            now = now - 1 < 0 ? now : now - 1;
            return list[now];
        }
        public T getNow()
        {
            return list[now];
        }
        public T get(int index)
        {
            return list[index];
        }
        public int getCount()
        {
            return list.Count;
        }
    }
}
