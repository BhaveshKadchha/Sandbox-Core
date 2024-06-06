using System;
using UnityEngine;
using Sandbox.Helper;

namespace Sandbox.AudioSystem
{
    public class AudioStateEventArgs : EventArgs
    {
        public bool IsOn;
        public float Volume;
    }

    public abstract class AudioSystem : MonoBehaviour
    {
        public EventHandler<AudioStateEventArgs> OnStateChanged;
        public AudioStateEventArgs eventArg;
        public bool currentState, defaultState;
        public float currentVolume, defaultVolume;
        internal AudioSource audioSource;
        internal string volumeKey, stateKey;

        public virtual void Init(string stateKey, string volumeKey, float defaultVolume, bool defaultState)
        {
            this.stateKey = stateKey;
            this.defaultVolume = defaultVolume;
            this.defaultState = defaultState;
            this.volumeKey = volumeKey;

            audioSource = AudioSourceFactory();
            Load();
            audioSource.volume = currentVolume;
        }

        public void ChangeState() => ChangeState(!currentState);
        public virtual void ChangeState(bool state)
        {
            currentState = state;
            eventArg.IsOn = state;
            OnStateChanged(this, eventArg);
        }
        public virtual void ChangeVolume(float volume)
        {
            currentVolume = volume;
            eventArg.Volume = volume;
            OnStateChanged(this, eventArg);
        }

        public void Save()
        {
            EncryptedPlayerPrefs.SetBool(stateKey, currentState);
            EncryptedPlayerPrefs.SetFloat(volumeKey, currentVolume);
        }
        public void Load()
        {
            currentState = EncryptedPlayerPrefs.GetBool(stateKey, defaultState);
            currentVolume = EncryptedPlayerPrefs.GetFloat(volumeKey, defaultVolume);
            eventArg = new AudioStateEventArgs { IsOn = currentState, Volume = currentVolume };
        }

        public AudioSource AudioSourceFactory()
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            source.volume = currentVolume;
            return source;
        }
    }
}