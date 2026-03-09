using overhealer.Core;
using UnityEngine;

namespace playground.Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig")]
    public class PlayerConfig :
            BaseConfig<PlayerConfig>
    {
        public GameObject PlayerPrefab;
        public GameObject CameraContainerPrefab;

        [Header("Player")]
        public float PlayerMoveSpeed;
        public float PlayerJumpHeight;

        [Header("Camera")]
        public float CameraSensitivity = 2.5f;
    }
}