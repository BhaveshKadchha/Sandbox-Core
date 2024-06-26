﻿using UnityEngine;

namespace Sandbox.Singleton
{
    public class RegulatorSingleton<T> : MonoBehaviour where T : Component
    {
        static T instance;
        public static bool HasInstance => instance != null;
        public float initialziationTime;

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
                        go.hideFlags = HideFlags.HideAndDontSave;
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
            initialziationTime = Time.time;
            DontDestroyOnLoad(gameObject);

            T[] oldInstance = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (T old in oldInstance)
                if (old.GetComponent<RegulatorSingleton<T>>().initialziationTime < initialziationTime)
                    Destroy(old.gameObject);

            if (instance == null)
                instance = this as T;
        }
    }
}