using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public class InputService : IService {
        public bool IsJump { get { return Input.GetKeyDown(KeyCode.Space); } }
        public bool IsSprint { get { return Input.GetKeyDown(KeyCode.LeftShift); } }

        public Vector3 MoveAxis { get { return _moveAxis; } }
        public Vector2 MouseAxis { get { return _mouseAxis; } }

        private Vector3 _moveAxis;
        private Vector2 _mouseAxis;

        private const string _moveAxisX = "Horizontal", _moveAxisY = "Vertical";
        private const string _mouseAxisX = "Mouse X", _mouseAxisY = "Mouse Y";

        public void OnUpdate() {
            _moveAxis = new Vector3(Input.GetAxis(_moveAxisX), 0, Input.GetAxis(_moveAxisY));
            _mouseAxis = new Vector2(Input.GetAxis(_mouseAxisX), Input.GetAxis(_mouseAxisY));
        }

        public void LockMouse() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void UnlockMouse() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}