using UnityEngine;

namespace playground.Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Player Config")]
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