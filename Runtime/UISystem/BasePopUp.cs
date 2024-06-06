namespace Sandbox.UISystem
{
    public abstract class BasePopUp : BaseUI
    {
        public override void Show()
        {
            ViewController.Instance.AddPopUp(this);
            base.Show();
        }

        public override void Hide()
        {
            ViewController.Instance.RemovePopUp();
            base.Hide();
        }
    }
}