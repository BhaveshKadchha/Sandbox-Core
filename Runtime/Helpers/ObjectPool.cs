using System;
using System.Collections.Generic;

namespace Sandbox.Helper
{
    public class ObjectPool<T>
    {
        List<T> currentStock;

        readonly bool isDynamic;
        readonly Func<T> objectFactory;
        readonly Action<T> turnOnCallback;
        readonly Action<T> turnOffCallback;
        readonly Action<T> dumpPool;

        public ObjectPool(Func<T> objectFactory, Action<T> turnOnCallback, Action<T> turnOffCallback, Action<T> dumpPool, int initialStock = 0, bool isDynamic = true)
        {
            this.isDynamic = isDynamic;

            this.objectFactory = objectFactory;
            this.turnOffCallback = turnOffCallback;
            this.turnOnCallback = turnOnCallback;
            this.dumpPool = dumpPool;

            currentStock = new List<T>();

            for (var i = 0; i < initialStock; i++)
            {
                var o = this.objectFactory();
                this.turnOffCallback(o);
                currentStock.Add(o);
            }
        }

        public ObjectPool(Func<T> objectFactory, Action<T> turnOnCallback, Action<T> turnOffCallback, Action<T> dumpPool, List<T> initialStock, bool isDynamic = true)
        {
            this.isDynamic = isDynamic;

            this.objectFactory = objectFactory;
            this.turnOffCallback = turnOffCallback;
            this.turnOnCallback = turnOnCallback;
            this.dumpPool = dumpPool;

            currentStock = initialStock;
        }

        public T GetObject()
        {
            var result = default(T);
            if (currentStock.Count > 0)
            {
                result = currentStock[0];
                currentStock.RemoveAt(0);
            }
            else if (isDynamic)
                result = objectFactory();

            turnOnCallback(result);
            return result;
        }

        public void ReturnObject(T obj)
        {
            turnOffCallback(obj);
            currentStock.Add(obj);
        }

        public List<T> GetAllObject() => currentStock;

        public void DumpPool()
        {
            foreach (T items in currentStock)
                dumpPool(items);
        }
    }
}