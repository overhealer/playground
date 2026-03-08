using playground.Assets.Scripts.Core.Services;
using playground.Assets.Scripts.Core.UI.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playground.Assets.Scripts.UI.States
{
    public class UIState :
            MonoBehaviour
    {
        public UIStateEnum State => state;

        [SerializeField]
        private UIStateEnum state;
        [SerializeField]
        private UIStateEnum stateOnClose;

        [SerializeField]
        private InputActionReference closeInputAction;

        public virtual void Enable()
        {
            if (closeInputAction)
                closeInputAction.action.performed += Close;
        }

        public virtual void Disable()
        {
            if (closeInputAction)
                closeInputAction.action.performed -= Close;
        }

        public void Close(InputAction.CallbackContext callbackContext)
        {
            ServiceLocator.Instance.Get<UIService>().EnableState(stateOnClose);
        }
    }
}