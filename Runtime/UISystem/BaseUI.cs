using System.Collections;
using UnityEngine;

namespace Sandbox.UISystem
{
    [RequireComponent(typeof(Canvas))]
    public abstract class BaseUI : MonoBehaviour
    {
        [HideInInspector] public Canvas canvas;
        public UIShowAnimation showAnimation;
        public UIHideAnimation hideAnimation;

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        public virtual void Show()
        {
            if (IsVisible()) return;

            canvas.enabled = true;
            if (showAnimation != null)
                StartCoroutine(showAnimation.Animate());
        }

        public virtual void Hide()
        {
            if (hideAnimation != null)
                StartCoroutine(HideAnimation());
            else
                HideCanvas();
        }

        IEnumerator HideAnimation()
        {
            yield return StartCoroutine(hideAnimation.Animate());
            HideCanvas();
        }

        internal void HideCanvas()
        {
            canvas.enabled = false;
        }

        public bool IsVisible() => canvas.enabled;
    }
}