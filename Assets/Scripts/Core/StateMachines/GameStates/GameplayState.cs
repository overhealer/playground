using playground.Assets.Scripts.Configs;
using playground.Assets.Scripts.Core.Services;
using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.UpdatableInterfaces;
using playground.Assets.Scripts.Core.UI.UI;
using playground.Assets.Scripts.UI.States;

namespace playground
{
    public class GameplayState :
        State,
        IUpdateState,
        IFixedUpdateState
    {
        private PlayerConfig gameConfig;
        private UIService uiService;
        private int currentLevelNumber;

        public GameplayState(PlayerConfig gameConfig, UIService ui)
        {
            this.gameConfig = gameConfig;
            uiService = ui;
        }

        public void Enter()
        {
            ServiceLocator.Instance.Get<CameraService>().SpawnCamera();
            ServiceLocator.Instance.Get<PlayerService>().SpawnPlayer();

            ServiceLocator.Instance.Get<UIService>().EnableState(UIStateEnum.Gameplay);
        }

        public void Exit()
        {

        }

        public void FixedUpdate()
        {

        }

        void IUpdateState.Update()
        {

        }

        private void OnLoaded()
        {

        }
    }
}
