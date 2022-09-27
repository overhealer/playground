using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public class PlayerMovementSystem : IEcsRunSystem {
        private EcsFilter<Player> _filter;
        private GameConfig _gameConfig;
        private InputService _inputService;

        private float _xCamRotation;
        private const float _minCamRotationX = -80f, _maxCamRotationX = 80f;

        public void Run() {
            ref Player player = ref _filter.Get1(0);
            //position
            player.PlayerCharacterController.Move(
                (_inputService.MoveAxis.x * player.PlayerTranform.right + 
                _inputService.MoveAxis.z * player.PlayerTranform.forward) *
                _gameConfig.PlayerMovementSpeed * Time.deltaTime);
            //camera rotation
            _xCamRotation -= _inputService.MouseAxis.y * _gameConfig.PlayerCameraSensitivity;
            _xCamRotation = Mathf.Clamp(_xCamRotation, _minCamRotationX, _maxCamRotationX);
            player.PlayerVirtualCamera.transform.localRotation = Quaternion.Euler(_xCamRotation, 0f, 0f);
            player.PlayerTranform.transform.Rotate(Vector3.up * _inputService.MouseAxis.x * _gameConfig.PlayerCameraSensitivity);
        }
    }
}
