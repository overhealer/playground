using overhealer.Core;
using playground.Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playground.Assets.Scripts.Characters
{
    public class PlayerCameraController :
        MonoBehaviour,
        IUpdatable,
        IInitialisable
    {
        public Quaternion LookRotation => Quaternion.Euler(0f, cameraFollowTarget.eulerAngles.y, 0f);

        [SerializeField]
        private InputActionReference lookInputAction;

        [SerializeField]
        private Transform cameraFollowTarget;

        private float sensitivity;

        public void Init()
        {
            //ServiceLocator.Instance.Get<CameraService>().CurrentVirtualCamera.Follow = cameraFollowTarget;

            sensitivity = PlayerConfig.Instance.CameraSensitivity;
        }

        public void OnUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            var lookInput = lookInputAction.action.ReadValue<Vector2>();

            cameraFollowTarget.rotation *= Quaternion.AngleAxis(lookInput.x * sensitivity * Time.deltaTime, Vector3.up);
            cameraFollowTarget.rotation *= Quaternion.AngleAxis(lookInput.y * sensitivity * Time.deltaTime, Vector3.right);

            var angles = cameraFollowTarget.localEulerAngles;
            angles.z = 0f;
            float angleX = cameraFollowTarget.localEulerAngles.x;
            if (angleX > 180f && angleX < 340f)
            {
                angles.x = 340f;
            }
            else if (angleX < 180f && angleX > 40f)
            {
                angles.x = 40f;
            }
            cameraFollowTarget.localEulerAngles = angles;
        }
    }
}