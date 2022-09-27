using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config")]
    public class GameConfig : ScriptableObject {
        [Header("Global")]
        public float Gravity;

        [Header("Player")]
        public float PlayerMovementSpeed;
        public float PlayerJumpPower;
        public float PlayerCameraSensitivity;
    }
}