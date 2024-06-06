using System.Collections;
using UnityEngine;

namespace Sandbox.UISystem
{
    public abstract class UIAnimation
    {
        public float animationTime;
        public delegate void onAnimationEnd();
        public onAnimationEnd OnAnimationEnd;

        public IEnumerator Animate()
        {
            float elapsed = 0;
            float perc;
            while (elapsed < animationTime)
            {
                perc = elapsed / animationTime;
                OnAnimationRunning(perc);
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
            OnAnimationEnd?.Invoke();
            yield return null;
        }

        public abstract void OnAnimationRunning(float perc);
    }
}