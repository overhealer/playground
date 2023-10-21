using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class InputService : IService
    {
        public virtual Vector3 MousePosition => Input.mousePosition;
        public virtual float MoveAxisX => Input.GetAxis(HORIZONTAL_AXIS);
        public virtual float MoveAxisY => Input.GetAxis(VERTICAL_AXIS);
        public virtual float LookAxisX => Input.GetAxis(HORIZONTAL_LOOK_AXIS);
        public virtual float LookAxisY => Input.GetAxis(VERTICAL_LOOK_AXIS);
        public virtual bool JumpKeyPressed => Input.GetKey(KeyCode.Space);


        protected const string HORIZONTAL_AXIS = "Horizontal";
        protected const string VERTICAL_AXIS = "Vertical";
        protected const string HORIZONTAL_LOOK_AXIS = "Mouse X";
        protected const string VERTICAL_LOOK_AXIS = "Mouse Y";



        public InputService()
        {

        }
    }
}
