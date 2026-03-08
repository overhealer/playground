using playground.Assets.Scripts.Configs;
using playground.Assets.Scripts.Core;
using playground.Assets.Scripts.Core.Attributes;
using playground.Assets.Scripts.Core.Interfaces;
using playground.Assets.Scripts.Core.Services;
using playground.Assets.Scripts.Core.StateMachines;
using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;
using playground.Assets.Scripts.Core.StateMachines.GameStates;
using playground.Assets.Scripts.Core.UI.UI;
using System;
using System.Reflection;
using UnityEngine;

namespace playground
{
    public class GameBootstrapState : State, IState
    {
        private GameStateMachine gameStateMachine;
        private PlayerConfig gameConfig;
        private SaveService saveService;
        private UIService uiService;

        public GameBootstrapState(UIService ui, GameStateMachine GameStateMachine, PlayerConfig gameConfig)
        {
            gameStateMachine = GameStateMachine;
            uiService = ui;
            this.gameConfig = gameConfig;
        }

        public void Enter()
        {
            CreateServices(uiService);
            Debug.Log("Loading game scene...");
            LevelPayload payload = new LevelPayload("Game", () => gameStateMachine.Enter<GameplayState>());
            gameStateMachine.Enter<LoadLevelState, LevelPayload>(payload);
        }

        public void Exit()
        {

        }

        public void CreateServices(UIService uiService)
        {
            Debug.Log("Create services...");

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetCustomAttribute<ServiceAttribute>(true) != null)
                {
                    //Debug.Log(type);
                    ServiceLocator.Instance.Add(type, (IService)Activator.CreateInstance(type));
                }
            }

            foreach (var service in ServiceLocator.Instance.Services.Values)
            {
                if (service is IInitialisable)
                {
                    (service as IInitialisable).Init();
                }

                if (service is IUpdatable)
                {
                    GameInstance.RegisterUpdatable(service as IUpdatable);
                }
            }

            uiService.InitUI();
        }
    }
}