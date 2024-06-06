using UnityEngine.Events;
using UnityEngine;
using System;

namespace Sandbox.Helper
{
    [Serializable]
    public class Observable<T>
    {
        [SerializeField] T value;
        [SerializeField] UnityEvent<T> onValueChanged;

        public T Value
        {
            get => value;
            set => Set(value);
        }

        public static implicit operator T(Observable<T> observer) => observer.value;

        public Observable(T value, UnityAction<T> callback = null)
        {
            this.value = value;
            onValueChanged = new UnityEvent<T>();
            if (callback != null) onValueChanged.AddListener(callback);
        }

        public void Set(T value)
        {
            if (Equals(this.value, value)) return;
            this.value = value;
            Invoke();
        }

        public void Invoke()
        {
            onValueChanged?.Invoke(value);
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (onValueChanged == null) onValueChanged = new UnityEvent<T>();
            onValueChanged.AddListener(callback);
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (onValueChanged == null) return;
            onValueChanged.RemoveListener(callback);
        }

        public void RemoveAllListener()
        {
            if (onValueChanged == null) return;
            onValueChanged.RemoveAllListeners();
        }

        public void Dispose()
        {
            RemoveAllListener();
            onValueChanged = null;
            value = default;
        }
    }
}