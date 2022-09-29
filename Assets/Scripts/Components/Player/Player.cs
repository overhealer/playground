using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public struct Player {
        public Transform PlayerTranform;
        public CharacterController PlayerCharacterController;
        public Animator PlayerAnimator;
        public Camera PlayerCamera;
        public CinemachineVirtualCamera PlayerVirtualCamera;

        public float CurrentMovementSpeed;
    }
}