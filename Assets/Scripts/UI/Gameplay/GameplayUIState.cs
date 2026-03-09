using overhealer.Core;
using UnityEngine;

namespace Assets.Scripts.UI.States
{
    public class GameplayUIState :
        UIState
    {
        /*        public InputActionReference InventoryInputAction => inventoryInputAction;

                [SerializeField]
                private InputActionReference inventoryInputAction;*/

        public override void Enable()
        {
            base.Enable();
            /*ServiceLocator.Instance.Get<InputService>().PlayerInputActions.Player.Enable();
            inventoryInputAction.action.performed += OpenInventory;*/

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        public override void Disable()
        {
            base.Disable();
            /*ServiceLocator.Instance.Get<InputService>().PlayerInputActions.Player.Disable();
            inventoryInputAction.action.performed -= OpenInventory;*/

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /*public void OpenInventory(InputAction.CallbackContext callbackContext)
        {
            ServiceLocator.Instance.Get<UIService>().EnableState(UIStateEnum.Inventory);
        }*/
    }
}