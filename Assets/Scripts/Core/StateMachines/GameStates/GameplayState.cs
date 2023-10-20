using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class GameplayState : State, IUpdateState, IFixedUpdateState
    {
        private AssetProvider _assetProvider;
        private GameConfig _gameConfig;
        private MainUI _mainUI;
        private Level _currentLevel;
        private int _currentLevelNumber;

        public GameplayState(AssetProvider assetProvider, GameConfig gameConfig, MainUI ui)
        {
            _assetProvider = assetProvider;
            _gameConfig = gameConfig;
            _mainUI = ui;
        }

        public void Enter()
        {
            _mainUI.InitGameCanvas();

            Level level = null;
            _currentLevel = level;
            _currentLevel.Init();
        }

        public void Exit()
        {

        }

        public void FixedUpdate()
        {
            
        }

        void IUpdateState.Update()
        {
            _currentLevel.UpdateLevel();
        }
    }
}
