using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public class PlayerInitSystem : IEcsInitSystem {
        private AssetProvider _assetProvider;
        private EcsWorld _ecsWorld;

        public void Init() {
            EcsEntity playerEntity = _ecsWorld.NewEntity();

            ref Player player = ref playerEntity.Get<Player>();

            GameObject playerGO = Object.Instantiate(_assetProvider.PlayerPrefab);
            player.PlayerTranform = playerGO.transform;
            player.PlayerCharacterController = playerGO.GetComponent<CharacterController>();
            player.PlayerAnimator = playerGO.GetComponentInChildren<Animator>();
        }
    }
}