namespace Sandbox.UISystem
{
    public interface IHideUIAnimation
    {
        void OnHideAnimationRunning(float perc);
    }

    public sealed class UIHideAnimation : UIAnimation
    {
        IHideUIAnimation hideUIAnimation;
        UIHideAnimation(float time, IHideUIAnimation hide)
        {
            animationTime = time;
            hideUIAnimation = hide;
        }

        public override void OnAnimationRunning(float perc) => hideUIAnimation.OnHideAnimationRunning(perc);

        public class Builder
        {
            onAnimationEnd callback;
            public Builder WithCallback(onAnimationEnd callback)
            {
                this.callback = callback;
                return this;
            }

            public UIHideAnimation Build(float time, IHideUIAnimation hide)
            {
                UIHideAnimation anim = new UIHideAnimation(time, hide);
                if (callback != null)
                    anim.OnAnimationEnd = callback;
                return anim;
            }
        }
    }
}