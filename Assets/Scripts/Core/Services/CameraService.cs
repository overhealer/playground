using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class CameraService : IService
    {
        public Camera CurrentCamera { get; set; }
        public CinemachineVirtualCamera CurrentVirtualCamera { get; set; }

        public CinemachineBrain CameraBrain { get; set; }
    }
}
