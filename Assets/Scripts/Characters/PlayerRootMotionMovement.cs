using overhealer.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playground.Assets.Scripts.Characters
{
    public class PlayerRootMotionMovementController :
            MonoBehaviour, IInitialisable, IUpdatable
    {
        [Header("Inputs"), SerializeField]
        private InputActionReference moveInputAction;

        [SerializeField]
        private InputActionReference jumpInputAction;

        [SerializeField]
        private InputActionReference sprintInputAction;

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private CharacterController characterController;
        [SerializeField]
        private PlayerCameraController playerCameraController;
        [SerializeField]
        private Transform playerModelTransform;
        [SerializeField]
        private Transform headTransform;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private float jumpSpeed;

        [SerializeField]
        private float jumpButtonGracePeriod;

        [SerializeField]
        private Transform cameraTransform;

        private bool isSprinting;
        private float ySpeed;
        private float originalStepOffset;
        private float? lastGroundedTime;
        private float? jumpButtonPressedTime;

        private int movementXHash = Animator.StringToHash("MoveX");
        private int movementYHash = Animator.StringToHash("MoveY");
        private float currentMovementX;
        private float currentMovementY;
        private float animationSmoothTime = 5f;
        private Vector3 animationVelocity;

        public void Init()
        {
            cameraTransform = ServiceLocator.Instance.Get<CameraService>().CurrentCamera.transform;
            SetupInput();
            originalStepOffset = characterController.stepOffset;
        }

        public void OnUpdate()
        {
            Vector2 input = moveInputAction.action.ReadValue<Vector2>();

            Vector3 movementDirection = new Vector3(input.x, 0f, input.y);

            isSprinting = sprintInputAction.action.ReadValue<float>() > 0;

            float targetInputX = isSprinting ? input.x : input.x / 2;
            float targetInputY = isSprinting ? input.y : input.y / 2;

            currentMovementX = Mathf.Lerp(currentMovementX, targetInputX, animationSmoothTime * Time.deltaTime);
            currentMovementY = Mathf.Lerp(currentMovementY, targetInputY, animationSmoothTime * Time.deltaTime);

            float inputX = currentMovementX;
            float inputY = currentMovementY;

            animator.SetFloat(movementXHash, inputX, 0.05f, Time.deltaTime);
            animator.SetFloat(movementYHash, inputY, 0.05f, Time.deltaTime);

            movementDirection = playerCameraController.LookRotation * movementDirection;
            movementDirection.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
            }

            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
            {
                characterController.stepOffset = originalStepOffset;
                ySpeed = -0.5f;

                if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
                {
                    ySpeed = jumpSpeed;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            }
            else
            {
                characterController.stepOffset = 0;
            }

            if (movementDirection != Vector3.zero)
            {
                animator.SetBool("IsMoving", true);

                /*Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);*/
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            //transform.rotation = playerCameraController.LookRotation;
        }

        public void SetupInput()
        {
            jumpInputAction.action.performed += Jump;
        }

        private void OnDestroy()
        {
            jumpInputAction.action.performed -= Jump;
        }

        public void Jump(InputAction.CallbackContext callbackContext)
        {
            if (characterController.isGrounded == false)
                return;

            jumpButtonPressedTime = Time.time;
        }

        private void OnAnimatorMove()
        {
            animationVelocity = animator.deltaPosition;
            animationVelocity.y = ySpeed * Time.deltaTime;

            characterController.Move(/*playerCameraController.LookRotation * */animationVelocity);
        }
    }
}