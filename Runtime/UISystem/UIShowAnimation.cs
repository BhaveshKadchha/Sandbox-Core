namespace Sandbox.UISystem
{
    public interface IShowUIAnimation
    {
        void OnShowAnimationRunning(float perc);
    }

    public sealed class UIShowAnimation : UIAnimation
    {
        IShowUIAnimation showUIAnimation;
        UIShowAnimation(float time, IShowUIAnimation show)
        {
            animationTime = time;
            showUIAnimation = show;
        }

        public override void OnAnimationRunning(float perc) => showUIAnimation.OnShowAnimationRunning(perc);

        public class Builder
        {
            onAnimationEnd callback;
            public Builder WithCallback(onAnimationEnd callback)
            {
                this.callback = callback;
                return this;
            }

            public UIShowAnimation Build(float time, IShowUIAnimation show)
            {
                UIShowAnimation anim = new UIShowAnimation(time, show);
                if (callback != null)
                    anim.OnAnimationEnd = callback;
                return anim;
            }
        }
    }
}