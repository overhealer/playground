using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public class PlayerAnimationSystem : IEcsRunSystem {
        private EcsFilter<Player> _filter;
        private InputService _inputService;

        private float _moveAnimBlend;
        private const float _moveBlendSpeed = 5f;
        private int _moveAnimHash = Animator.StringToHash("Speed");

        public void Run() {
            ref Player player = ref _filter.Get1(0);

            _moveAnimBlend = Mathf.Lerp(_moveAnimBlend, _inputService.MoveAxis.magnitude, _moveBlendSpeed * Time.deltaTime);
            player.PlayerAnimator.SetFloat(_moveAnimHash, _moveAnimBlend);
        }
    }
}
