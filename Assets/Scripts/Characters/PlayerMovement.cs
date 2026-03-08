using playground.Assets.Scripts.Configs;
using playground.Assets.Scripts.Core.Interfaces;
using playground.Assets.Scripts.Core.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playground.Assets.Scripts.Characters
{
    public class PlayerMovement :
        MonoBehaviour,
        IUpdatable,
        IInitialisable
    {
        [Header("Inputs"), SerializeField]
        private InputActionReference moveInputAction;

        [SerializeField]
        private InputActionReference jumpInputAction;

        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private PlayerAnimationController playerAnimationController;

        [SerializeField]
        private Transform playerModel;

        [SerializeField]
        private Transform cameraFollowTarget;

        private InputService inputService;
        private PlayerConfig playerConfig;

        private Vector3 currentDirection;
        private Vector3 currentVelocity;
        private float currentSpeed;

        public void Init()
        {
            inputService = ServiceLocator.Instance.Get<InputService>();
            playerConfig = PlayerConfig.Instance;

            jumpInputAction.action.performed += Jump_performed;
        }

        private void OnDestroy()
        {
            jumpInputAction.action.performed -= Jump_performed;
        }

        public void OnUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var moveInput = moveInputAction.action.ReadValue<Vector2>();

            if (moveInput.x != 0 || moveInput.y != 0)
            {
                playerModel.rotation = Quaternion.Euler(0, cameraFollowTarget.rotation.eulerAngles.y, 0);
                currentSpeed = Mathf.MoveTowards(currentSpeed, playerConfig.PlayerMoveSpeed, Time.deltaTime * 8f);
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, Time.deltaTime * 8f);
            }

            currentDirection = new Vector3(moveInput.x, 0, moveInput.y);
            if (currentDirection.magnitude > 1)
                currentDirection.Normalize();
            currentVelocity = playerModel.rotation * new Vector3(
                currentDirection.x * currentSpeed,
                currentVelocity.y,
                currentDirection.z * currentSpeed);

            currentVelocity.y = characterController.isGrounded ? currentVelocity.y : currentVelocity.y + LevelsConfig.Instance.Gravity;

            characterController.Move(currentVelocity * Time.deltaTime);

            playerAnimationController?.SetMoveAxisValue(moveInput.x, moveInput.y);
        }

        private void Jump_performed(InputAction.CallbackContext context)
        {
            Jump();
        }

        private void Jump()
        {
            if (!characterController.isGrounded)
                return;

            currentVelocity.y += playerConfig.PlayerJumpHeight;
        }
    }
}