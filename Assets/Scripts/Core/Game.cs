using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class Game
    {
        public GameStateMachine StateMachine => _gameStateMachine;

        private GameStateMachine _gameStateMachine;
        private MainUI _mainUI;

        public Game(AssetProvider assetProvider, MainUI ui, GameConfig gameConfig)
        {
            _mainUI = ui;

            Application.targetFrameRate = 60;

            _gameStateMachine = new GameStateMachine(assetProvider, ui, gameConfig);
            _gameStateMachine.Enter<GameBootstrapState>();
        }

        public void OnUpdate()
        {
            _gameStateMachine.UpdateState();
            _mainUI.UpdatePanels();
        }

        public void OnFixedUpdate()
        {
            _gameStateMachine.FixedUpdateState();
        }
    }
}
