using playground.Assets.Scripts.Configs;
using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine;
using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;
using playground.Assets.Scripts.Core.StateMachines.GameStates;
using playground.Assets.Scripts.Core.UI.UI;
using System;
using System.Collections.Generic;

namespace playground.Assets.Scripts.Core.StateMachines
{
    public class GameStateMachine : UpdateStateMachine
    {
        public Dictionary<Type, IExitableState> States => states;

        public GameStateMachine(UIService ui, PlayerConfig gameConfig)
        {
            states = new Dictionary<Type, IExitableState>()
            {
                [typeof(GameBootstrapState)] = new GameBootstrapState(ui, this, gameConfig),
                [typeof(LoadLevelState)] = new LoadLevelState(this),
                [typeof(GameplayState)] = new GameplayState(gameConfig, ui),
            };
        }
    }
}