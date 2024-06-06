using UnityEngine;

namespace Sandbox.AudioSystem
{
    [AddComponentMenu("")]
    public sealed class MusicSystem : AudioSystem
    {
        AudioClip defaultMusicClip;

        MusicSystem() { }

        public class Builder
        {
            float volume = 1;
            bool state = true;
            AudioClip clip;
            Transform parent;

            public Builder WithDefaultState(bool state)
            {
                this.state = state;
                return this;
            }

            public Builder WithDefaultVolume(float volume)
            {
                this.volume = volume;
                return this;
            }

            public Builder WithClip(AudioClip clip)
            {
                this.clip = clip;
                return this;
            }

            public Builder WithParent(Transform transform)
            {
                parent = transform;
                return this;
            }

            public MusicSystem Build()
            {
                GameObject go = new GameObject("MusicSystem");
                if (parent != null) go.transform.SetParent(parent);
                MusicSystem system = go.AddComponent<MusicSystem>();
                system.defaultMusicClip = clip;
                system.Init("Music_State", "Music_Volume", volume, state);
                return system;
            }
        }

        public override void Init(string stateKey, string volumeKey, float defaultVolume, bool defaultState)
        {
            base.Init(stateKey, volumeKey, defaultVolume, defaultState);
            audioSource.loop = true;

            if (defaultMusicClip != null)
            {
                audioSource.clip = defaultMusicClip;
                if (currentState) audioSource.Play();
            }
        }

        public override void ChangeState(bool state)
        {
            base.ChangeState(state);
            if (state) audioSource.Play();
            else audioSource.Stop();
        }

        public override void ChangeVolume(float volume)
        {
            base.ChangeVolume(volume);
            audioSource.volume = volume;
        }

        public void RestartMusic()
        {
            audioSource.Stop();
            if (currentState) audioSource.Play();
        }

        public void ChangeClip(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            if (currentState) audioSource.Play();
        }
    }
}