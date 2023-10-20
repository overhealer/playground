using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class GameBootstrap : MonoBehaviour
    {
        public bool UseDebug;
        public Game GameInstance => _gameInstance;

        [SerializeField] private AssetProvider _assetProvider;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private MainUI _mainUI;

        private Game _gameInstance;

        private void Awake()
        {
            GameInit();
        }

        public void GameInit()
        {
            print("Start Game Init...");

            DontDestroyOnLoad(gameObject);
            MainUI ui = Instantiate(_mainUI, transform);

            _gameInstance = new Game(_assetProvider, ui, _gameConfig);

            Debug.Log("Game Init complete!");
        }

        private void Update()
        {
            _gameInstance?.OnUpdate();
        }

        private void FixedUpdate()
        {
            _gameInstance?.OnFixedUpdate();
        }
    }
}
