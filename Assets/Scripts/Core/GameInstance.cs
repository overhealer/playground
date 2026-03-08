using playground.Assets.Scripts.Configs;
using playground.Assets.Scripts.Core.Interfaces;
using playground.Assets.Scripts.Core.StateMachines;
using playground.Assets.Scripts.Core.UI.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace playground.Assets.Scripts.Core
{
    public class GameInstance
    {
        private static Action<GameObject> OnObjectCreate;
        private static Action<GameObject> OnObjectDelete;
        private static Action<IUpdatable> OnUpdatableCreate;

        private GameStateMachine gameStateMachine;

        private List<IUpdatable> updatables = new List<IUpdatable>();
        private List<ILateUpdatable> lateUpdatables = new List<ILateUpdatable>();

        public GameInstance()
        {
            OnObjectCreate = (newObject) =>
            {
                IInitialisable[] inits = newObject.GetComponentsInChildren<IInitialisable>();
                for (int i = 0; i < inits.Length; i++)
                {
                    inits[i].Init();
                }

                IUpdatable[] updatables = newObject.GetComponentsInChildren<IUpdatable>();
                for (int i = 0; i < updatables.Length; i++)
                {
                    this.updatables.Add(updatables[i]);
                }

                ILateUpdatable[] lateUpdatables = newObject.GetComponentsInChildren<ILateUpdatable>();
                for (int i = 0; i < lateUpdatables.Length; i++)
                {
                    this.lateUpdatables.Add(lateUpdatables[i]);
                }
            };

            OnObjectDelete = (objectToDelete) =>
            {
                IUpdatable[] updatables = objectToDelete.GetComponentsInChildren<IUpdatable>();
                for (int i = 0; i < updatables.Length; i++)
                {
                    this.updatables.Remove(updatables[i]);
                }

                ILateUpdatable[] lateUpdatables = objectToDelete.GetComponentsInChildren<ILateUpdatable>();
                for (int i = 0; i < lateUpdatables.Length; i++)
                {
                    this.lateUpdatables.Remove(lateUpdatables[i]);
                }
            };

            OnUpdatableCreate = (updatable) =>
            {
                this.updatables.Add(updatable);
            };
        }

        public void OnUpdate()
        {
            foreach (var updatable in updatables)
            {
                updatable.OnUpdate();
            }

            gameStateMachine.UpdateState();
        }

        public void OnLateUpdate()
        {
            foreach (var lateUpdatable in lateUpdatables)
            {
                lateUpdatable.OnLateUpdate();
            }

            gameStateMachine.LateUpdateState();
        }

        public void OnFixedUpdate()
        {
            gameStateMachine.FixedUpdateState();
        }

        public void StartGame(UIService ui)
        {
            gameStateMachine = new GameStateMachine(ui, PlayerConfig.Instance);
            gameStateMachine.Enter<GameBootstrapState>();
        }

        public static GameObject CreateObject(GameObject prefab, Vector3 pos, Vector3 rot)
        {
            GameObject newObject = UnityEngine.Object.Instantiate(prefab, pos, Quaternion.Euler(rot));

            RegisterObject(newObject);

            return newObject;
        }

        public static void RegisterObject(GameObject newObject)
        {
            OnObjectCreate?.Invoke(newObject);
        }

        public static void RegisterUpdatable(IUpdatable updatable)
        {
            OnUpdatableCreate?.Invoke(updatable);
        }

        public static void DestoyObject(GameObject objectToDelete)
        {
            UnregisterObject(objectToDelete);

            UnityEngine.Object.Destroy(objectToDelete);
        }

        public static void UnregisterObject(GameObject objectToDelete)
        {
            OnObjectDelete?.Invoke(objectToDelete);
        }
    }
}