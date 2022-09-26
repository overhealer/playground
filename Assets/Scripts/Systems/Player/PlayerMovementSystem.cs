using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public class PlayerMovementSystem : IEcsRunSystem {
        private EcsFilter<Player> _filter;
        private GameConfig _gameConfig;
        private InputService _inputService;

        public void Run() {
            ref Player player = ref _filter.Get1(0);
            player.PlayerCharacterController.Move(_inputService.MoveAxis * _gameConfig.PlayerMovementSpeed * Time.deltaTime);
        }
    }
}
