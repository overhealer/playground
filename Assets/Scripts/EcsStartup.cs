using Leopotam.Ecs;
using UnityEngine;

namespace Playground {
    sealed class EcsStartup : MonoBehaviour {
        [SerializeField] private AssetProvider _assetProvider;
        [SerializeField] private GameConfig _gameConfig;

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;
        private EcsSystems _lateSystems;

        private InputService _inputService;

        private void Start() {
            _inputService = new InputService();

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _fixedSystems = new EcsSystems(_world);
            _lateSystems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_lateSystems);
#endif
            _systems
                .Add(new PlayerInitSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerAnimationSystem())
                .Inject(_assetProvider)
                .Inject(_gameConfig)
                .Inject(_inputService)
                .Init();

            _fixedSystems
                .Init();

            _lateSystems
                .Init();

            _inputService.LockMouse();
        }

        private void Update() {
            _systems?.Run();
            _inputService.OnUpdate();
        }

        private void FixedUpdate() {
            _fixedSystems?.Run();
        }

        private void LateUpdate() {
            _lateSystems?.Run();
        }

        private void OnDestroy() {
            if (_systems != null) {
                _systems.Destroy();
                _systems = null;
                _fixedSystems.Destroy();
                _fixedSystems = null;
                _lateSystems.Destroy();
                _lateSystems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}