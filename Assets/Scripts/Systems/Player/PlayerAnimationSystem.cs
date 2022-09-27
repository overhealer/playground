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
        private int _fallAnimHash = Animator.StringToHash("FreeFall");
        private int _jumpAnimHash = Animator.StringToHash("Jump");
        private int _groundedAnimHash = Animator.StringToHash("Grounded");


        public void Run() {
            ref Player player = ref _filter.Get1(0);

            //jump
            player.PlayerAnimator.SetBool(_groundedAnimHash, player.PlayerCharacterController.isGrounded);
            if (player.PlayerCharacterController.isGrounded) {
                player.PlayerAnimator.SetBool(_fallAnimHash, false);
                player.PlayerAnimator.SetBool(_jumpAnimHash, _inputService.IsJump);
            }
            else {
                player.PlayerAnimator.SetBool(_fallAnimHash, true);
            }

            //move 
            _moveAnimBlend = Mathf.Lerp(_moveAnimBlend, _inputService.MoveAxis.magnitude, _moveBlendSpeed * Time.deltaTime);
            player.PlayerAnimator.SetFloat(_moveAnimHash, _moveAnimBlend);
        }
    }
}
