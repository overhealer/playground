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

            InitLevel(0);
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

        public void InitLevel(int id)
        {
            if(_currentLevel != null)
            {
                GameObject.Destroy(_currentLevel.gameObject);
            }
            _currentLevel = GameObject.Instantiate(_assetProvider.LevelDatabase.Levels[id]);
            _currentLevel.Init(_assetProvider, _gameConfig);
        }
    }
}
