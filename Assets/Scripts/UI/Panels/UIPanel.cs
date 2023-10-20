using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class UIPanel : MonoBehaviour, IPanel
    {
        protected GameStateMachine _stateMachine;
        protected AssetProvider _assetProvider;
        protected MainUI _mainUI;
        protected GameConfig _gameConfig;

        public virtual void InitPanel(GameStateMachine stateMachine, AssetProvider assetProvider, MainUI ui, GameConfig config)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
            _mainUI = ui;
            _gameConfig = config;
        }

        public virtual void EnablePanel()
        {
            gameObject.SetActive(true);
        }

        public virtual void EnableWithDelay(float delay)
        {
            _mainUI.StartCoroutine(EnableDelay(delay));
        }

        public virtual void DisablePanel()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnUpdate()
        {
            if (!gameObject.activeSelf)
                return;
        }

        private IEnumerator EnableDelay(float amount)
        {
            yield return new WaitForSeconds(amount);
            EnablePanel();
        }
    }
}