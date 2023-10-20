using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class GameStateMachine : UpdateStateMachine
    {
        public Dictionary<Type, IExitableState> States => _states;

        public GameStateMachine(AssetProvider assetProvider, MainUI ui, GameConfig gameConfig)
        {
            _states = new Dictionary<System.Type, IExitableState>()
            {
                [typeof(GameBootstrapState)] = new GameBootstrapState(assetProvider, ui, this, gameConfig),
                [typeof(LoadLevelState)] = new LoadLevelState(this),
                [typeof(GameplayState)] = new GameplayState(assetProvider, gameConfig, ui),
            };
        }
    }
}