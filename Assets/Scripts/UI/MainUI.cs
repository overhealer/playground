using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace playground
{
    public class MainUI : MonoBehaviour
    {
        public Canvas ActiveCanvas => _activeCanvas;
        [SerializeField] private Canvas _gameCanvasPrefab, _debugCanvasPrefab;
        private Dictionary<Type, UIPanel> _panels = new Dictionary<Type, UIPanel>();
        private GameStateMachine _gameStateMachine;
        private Canvas _inputCanvas, _gameCanvas, _menuCanvas;
        private AssetProvider _assetProvider;
        private GameConfig _gameConfig;
        private Canvas _activeCanvas;


        public void InitUI(GameStateMachine stateMachine, AssetProvider assetProvider, GameConfig config)
        {
            _gameStateMachine = stateMachine;
            _assetProvider = assetProvider;
            _gameConfig = config;
        }

        public void InitGameCanvas()
        {
            if(_menuCanvas)
                Destroy(_menuCanvas.gameObject);

            _gameCanvas = Instantiate(_gameCanvasPrefab);
            _gameCanvas.worldCamera = ServiceContainer.Instance.Get<CameraService>().CurrentCamera;
            _gameCanvas.planeDistance = 5f;
            _activeCanvas = _gameCanvas;
            InitPanels(_gameCanvas);
        }

        public void InitPanels(Canvas canvas)
        {
            UIPanel[] panels = canvas.GetComponentsInChildren<UIPanel>(true);
            _panels.Clear();
            for (int i = 0; i < panels.Length; i++)
            {
                _panels.Add(panels[i].GetType(), panels[i]);
                panels[i].InitPanel(_gameStateMachine, _assetProvider, this, _gameConfig);
            }
        }

        public TPanel GetPanel<TPanel>() where TPanel : class, IPanel
        {
            return _panels[typeof(TPanel)] as TPanel;
        }

        public void EnablePanel<TPanel>(bool disableOther = false)
        {
            if(disableOther)
                DisableAllPanels();
            _panels[typeof(TPanel)].EnablePanel();
        }

        public void DisablePanel<TPanel>()
        {
            _panels[typeof(TPanel)].DisablePanel();
        }

        public void DisableAllPanels()
        {
            foreach (var panel in _panels)
            {
                if(panel.Value.gameObject.activeSelf)
                    panel.Value.DisablePanel();
            }
        }

        public void UpdatePanels()
        {
            foreach (var panel in _panels)
            {
                panel.Value.OnUpdate();
            }
        }
    }


}
