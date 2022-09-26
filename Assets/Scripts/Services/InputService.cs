using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public class InputService : IService {
        public Vector3 MoveAxis { get { return _moveAxis; } }

        private Vector3 _moveAxis;

        private const string _moveAxisXName = "Horizontal", _moveAxisYName = "Vertical";

        public void OnUpdate() {
            _moveAxis = new Vector3(Input.GetAxis(_moveAxisXName), 0, Input.GetAxis(_moveAxisYName));
        }
    }
}