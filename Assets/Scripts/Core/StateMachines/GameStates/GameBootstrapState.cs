using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class GameBootstrapState : State, IState
    {
        private GameStateMachine _gameStateMachine;
        private AssetProvider _assetProvider;
        private MainUI _mainUI;
        private GameConfig _gameConfig;
        private SaveService _saveService;

        public GameBootstrapState(AssetProvider assetProvider, MainUI ui, GameStateMachine i_GameStateMachine, GameConfig i_gameConfig)
        {
            _gameStateMachine = i_GameStateMachine;
            _assetProvider = assetProvider;
            _mainUI = ui;
            _gameConfig = i_gameConfig;
        }

        public void Enter()
        {
            SetupServices();
            _mainUI.InitUI(_gameStateMachine, _assetProvider, _gameConfig);
            Debug.Log("Loading game scene...");
            LoadLevelPayload payloadNextLevel = new LoadLevelPayload("Game", () => _gameStateMachine.Enter<GameplayState>());
            _gameStateMachine.Enter<LoadLevelState, LoadLevelPayload>(payloadNextLevel);
        }

        public void Exit()
        {

        }

        public void SetupServices()
        {
            Debug.Log("Init game services");
            InputService inputService = new MobileInputService(_mainUI);
            CameraService cameraService = new CameraService();
            _saveService = new SaveService();
            TimeService timeService = new TimeService();

            GameObject cameraContainer = GameObject.Instantiate(_assetProvider.CameraPrefab);
            cameraService.CurrentCamera = cameraContainer.GetComponentInChildren<Camera>();
            cameraService.CurrentVirtualCamera = cameraContainer.GetComponentInChildren<CinemachineVirtualCamera>();
            cameraService.CameraBrain = cameraContainer.GetComponentInChildren<CinemachineBrain>();
            GameObject.DontDestroyOnLoad(cameraContainer);

            ServiceContainer.Instance.Set(inputService);
            ServiceContainer.Instance.Set(cameraService);
            ServiceContainer.Instance.Set(_saveService);
            ServiceContainer.Instance.Set(timeService);
            Debug.Log("Game services init complete!");
        }
    }
}