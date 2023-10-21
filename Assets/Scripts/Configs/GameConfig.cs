using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    [CreateAssetMenu(menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        public float Gravity = -9.81f; 

        [Header("Player")]
        public float PlayerMoveSpeed;
        public float PlayerJumpHeight;

        [Header("Camera")]
        public float CameraSensitivity = 2.5f;
    }
}
