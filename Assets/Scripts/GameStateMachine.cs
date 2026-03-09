using overhealer.Core;
using playground.Assets.Scripts.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace playground.Assets.Scripts.Core.StateMachines
{
    public class GameStateMachine : UpdateStateMachine
    {
        public Dictionary<Type, IExitableState> States => states;

        public GameStateMachine(UIService ui, PlayerConfig gameConfig)
        {
            states = new Dictionary<Type, IExitableState>()
            {
                [typeof(GameBootstrapState)] = new GameBootstrapState(ui, this, () => Debug.Log("!")),
                [typeof(LoadLevelState)] = new LoadLevelState(this),
                //[typeof(GameplayState)] = new GameplayState(gameConfig, ui),
            };
        }
    }
}