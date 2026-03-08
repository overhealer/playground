using playground.Assets.Scripts.Core.Services;
using playground.Assets.Scripts.UI.States;
using System.Collections.Generic;
using UnityEngine;

namespace playground.Assets.Scripts.Core.UI.UI
{
    public class UIService :
            MonoBehaviour,
            IService
    {
        public UIStateEnum State
        {
            get
            {
                if (currentState != null)
                    return currentState.State;
                return UIStateEnum.None;
            }
        }

        [SerializeField]
        private List<UIState> StatePrefabs;

        [SerializeField]
        private Canvas mainCanvas;

        private UIState currentState;

        public void InitUI()
        {
            ServiceLocator.Instance.Add(typeof(UIService), this);
        }

        public void EnableState(UIStateEnum state)
        {
            if (currentState != null)
            {
                currentState.Disable();
                GameInstance.DestoyObject(currentState.gameObject);
            }

            var newState = GameInstance.CreateObject(GetStatePrefab(state).gameObject, Vector3.zero, Vector3.zero);
            newState.transform.SetParent(mainCanvas.transform, false);
            currentState = newState.GetComponent<UIState>();

            currentState.Enable();
        }

        private UIState GetStatePrefab(UIStateEnum state)
        {
            for (int i = 0; i < StatePrefabs.Count; i++)
            {
                if (StatePrefabs[i].State == state)
                    return StatePrefabs[i];
            }

            return null;
        }
    }
}