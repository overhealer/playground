using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace playground
{
    public class LoadLevelState : State, IPayloadState<LoadLevelPayload>
    {
        private GameStateMachine _gameStateMachine;

        public LoadLevelState(GameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }

        public void Enter(LoadLevelPayload payload)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(payload.LevelToLoad);
            if (payload.OnLoadComplete != null)
            {
                op.completed += (async) => payload.OnLoadComplete.Invoke();
            }
        }

        public void Exit()
        {
        }
    }

    public class LoadLevelPayload
    {
        public string LevelToLoad;
        public Action OnLoadComplete;

        public LoadLevelPayload(string levelToLoad, Action onLoadComplete)
        {
            LevelToLoad = levelToLoad;
            OnLoadComplete = onLoadComplete;
        }
    }
}
