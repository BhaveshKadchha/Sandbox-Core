using System;
using UnityEngine;
using Sandbox.Singleton;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Sandbox.UISystem
{
    public class ViewController : GenericSingleton<ViewController>
    {
        BaseScreen currentView;
        BaseScreen previousView;
        [SerializeField] BaseScreen defaultScreen;

        [Space(10)] public BaseScreen[] screens;
        [Space(10)] public EventSystem eventSystem;

        void Start()
        {
            for (int indexOfScreen = 0; indexOfScreen < screens.Length; indexOfScreen++)
                screens[indexOfScreen].canvas.enabled = false;

            if (defaultScreen != null)
            {
                currentView = Array.Find(screens, x => x.name == defaultScreen.name);
                currentView.Show();
            }
        }

        public void Show(Type screen)
        {
            if (screen.IsSubclassOf(typeof(BaseScreen)))
            {   
                previousView = currentView;
                currentView = Array.Find(screens, x => x.GetType() == screen);
                previousView.Hide();
                currentView.Show();
            }
        }


        #region BackButton
        Stack<BasePopUp> popupStack = new Stack<BasePopUp>();

        internal void AddPopUp(BasePopUp popUp) => popupStack.Push(popUp);
        internal void RemovePopUp()
        {
            if (popupStack.Count > 0)
                popupStack.Pop();
        }

        public bool TryHideLastOpenPopUp()
        {
            if (popupStack.Count > 0)
            {
                popupStack.Peek().Hide();
                return true;
            }
            return false;
        }

        public int PopupStackCount() => popupStack.Count;
        #endregion
    }
}