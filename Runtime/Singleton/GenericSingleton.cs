﻿using UnityEngine;

namespace Sandbox.Singleton
{
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        static T instance;
        public static bool HasInstance => instance != null;

        public static T TryGetInstance() => HasInstance ? instance : null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance != null)
                    {
                        var go = new GameObject(typeof(T).Name + " AutoGenerated");
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
            private set { }
        }

        public virtual void Awake()
        {
            if (!Application.isPlaying) return;
            instance = this as T;
        }
    }
}