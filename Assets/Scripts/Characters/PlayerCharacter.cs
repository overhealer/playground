using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _cameraFollowTarget;
        [SerializeField] private Transform _playerModel;

        private InputService _inputService;
        private GameConfig _gameConfig;

        private Vector3 _currentDirection;
        private Vector3 _currentVelocity;
        private float _currentSpeed;

        private int _moveXAnimationHash = Animator.StringToHash("MoveX");
        private int _moveYAnimationHash = Animator.StringToHash("MoveY");

        public void Init(GameConfig gameConfig)
        {
            _inputService = ServiceContainer.Instance.Get<InputService>();
            _gameConfig = gameConfig;
            ServiceContainer.Instance.Get<CameraService>().CurrentVirtualCamera.Follow = _cameraFollowTarget;
        }

        public void UpdatePlayer()
        {
            UpdateMovement();
            CameraRotation();
            if (_inputService.JumpKeyPressed)
                Jump();
        }

        private void UpdateMovement()
        {
            if(_inputService.MoveAxisY != 0 || _inputService.MoveAxisX != 0)
            {
                _playerModel.rotation = Quaternion.Euler(0, _cameraFollowTarget.transform.rotation.eulerAngles.y, 0);
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, _gameConfig.PlayerMoveSpeed, Time.deltaTime * 8f);
            }
            else
            {
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0, Time.deltaTime * 8f);
            }

            _currentDirection = new Vector3(_inputService.MoveAxisX, 0, _inputService.MoveAxisY);
            if(_currentDirection.magnitude > 1)
                _currentDirection.Normalize();
            _currentVelocity = _playerModel.rotation * new Vector3(
                _currentDirection.x * _currentSpeed,
                _currentVelocity.y,
                _currentDirection.z * _currentSpeed);

            _currentVelocity.y = _characterController.isGrounded ? _currentVelocity.y : _currentVelocity.y + _gameConfig.Gravity;

            _characterController.Move((_currentVelocity) * Time.deltaTime);
            _animator.SetFloat(_moveXAnimationHash, _inputService.MoveAxisX);
            _animator.SetFloat(_moveYAnimationHash, _inputService.MoveAxisY);
        }

        private void CameraRotation()
        {
            _cameraFollowTarget.rotation *= Quaternion.AngleAxis(_inputService.LookAxisX * _gameConfig.CameraSensitivity, Vector3.up);
            _cameraFollowTarget.rotation *= Quaternion.AngleAxis(_inputService.LookAxisY * _gameConfig.CameraSensitivity, Vector3.right);

            var angles = _cameraFollowTarget.localEulerAngles;
            angles.z = 0f;
            float angleX = _cameraFollowTarget.localEulerAngles.x;
            if(angleX > 180f && angleX < 340f)
            {
                angles.x = 340f;
            }
            else if (angleX < 180f && angleX > 40f)
            {
                angles.x = 40f;
            }
            _cameraFollowTarget.localEulerAngles = angles;
        }

        private void Jump()
        {
            if (!_characterController.isGrounded)
                return;

            _currentVelocity.y += _gameConfig.PlayerJumpHeight;
        }
    }
}
