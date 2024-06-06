using UnityEngine;

namespace Sandbox.AudioSystem
{
    [AddComponentMenu("")]
    public sealed class SFXSystem : AudioSystem
    {
        SFXSystem() { }

        public class Builder
        {
            float volume = 1;
            bool state = true;
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

            public Builder WithParent(Transform transform)
            {
                parent = transform;
                return this;
            }

            public SFXSystem Build()
            {
                GameObject go = new GameObject("SFXSystem");
                if (parent != null) go.transform.SetParent(parent);
                SFXSystem system = go.AddComponent<SFXSystem>();
                system.Init("SFX_State", "SFX_Volume", volume, state);
                return system;
            }
        }

        public override void Init(string stateKey, string volumeKey, float defaultVolume, bool defaultState)
        {
            base.Init(stateKey, volumeKey, defaultVolume, defaultState);
        }

        public override void ChangeState(bool state)
        {
            base.ChangeState(state);
            if (!state)
            {
                StopAllCoroutines();
                audioSource.Stop();
            }
        }

        public override void ChangeVolume(float volume)
        {
            base.ChangeVolume(volume);
            audioSource.volume = volume;
        }

        public void PlaySFXClip(AudioClip audioClip)
        {
            if (currentState) audioSource.PlayOneShot(audioClip);
        }

        public void PlaySFXClip(AudioClip audioClip,float volume)
        {
            if (currentState) audioSource.PlayOneShot(audioClip, volume);
        }
    }
}