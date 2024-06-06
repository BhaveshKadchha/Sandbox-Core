using System;
using System.Collections;
using System.Collections.Generic;

namespace Sandbox.Helper
{
    public class ObservableList<T> : IList<T>, IObservableList<T>
    {
        private readonly IList<T> list;
        public event Action<IList<T>> AnyValueChanged;

        public ObservableList(IList<T> initialList = null) => list = initialList ?? new List<T>();

        public T this[int index]
        {
            get => list[index];
            set
            {
                list[index] = value;
                Invoke();
            }
        }
        public void Invoke() => AnyValueChanged?.Invoke(list);
        public int Count => list.Count;
        public bool IsReadOnly => list.IsReadOnly;
        public void Add(T item)
        {
            list.Add(item);
            Invoke();
        }
        public void Clear()
        {
            list.Clear();
            Invoke();
        }
        public bool Contains(T item) => list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
        public int IndexOf(T item) => list.IndexOf(item);
        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            Invoke();
        }
        public bool Remove(T item)
        {
            var result = list.Remove(item);
            if (result)
                Invoke();
            return result;
        }
        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
            Invoke();
        }
    }

    public interface IObservableList<T>
    {
        void Add(T item);
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
        int IndexOf(T item);
        void Insert(int index, T item);
        void RemoveAt(int index);
        bool Remove(T item);
        IEnumerator<T> GetEnumerator();
    }
}