using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class MobileInputService : InputService
    {
        public override Vector3 MousePosition => Input.mousePosition;

        public MobileInputService(MainUI ui)
        {
            //_movementJoystick = ui.MovementJoystick;
        }
    }
}
