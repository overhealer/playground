using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config")]
    public class GameConfig : ScriptableObject {
        [Header("Global")]
        public float Gravity;

        [Header("Player")]
        public float PlayerWalkSpeed;
        public float PlayerRunSpeed;
        public float PlayerJumpHeight;
        public float PlayerCameraSensitivity;
    }
}