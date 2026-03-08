using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace playground.Assets.Scripts.Core.StateMachines.GameStates
{
    public class LoadLevelState :
            State,
            IPayloadState<LevelPayload>
    {
        private GameStateMachine gameStateMachine;

        public LoadLevelState(GameStateMachine stateMachine)
        {
            gameStateMachine = stateMachine;
        }

        public void Enter(LevelPayload payload)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(payload.SceneToLoad);
            if (payload.OnLoaded != null)
            {
                op.completed += (async) => payload.OnLoaded.Invoke();
            }
        }

        public void Exit()
        {
        }
    }
}