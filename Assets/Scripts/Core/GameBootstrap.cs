using playground.Assets.Scripts.Core.UI.UI;
using UnityEngine;

namespace playground.Assets.Scripts.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField]
        private UIService uiService;

        private GameInstance gameInstance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            gameInstance = new GameInstance();
            gameInstance.StartGame(uiService);
        }

        private void Update()
        {
            if (gameInstance != null)
            {
                gameInstance.OnUpdate();
            }
        }

        private void LateUpdate()
        {
            if (gameInstance != null)
            {
                gameInstance.OnLateUpdate();
            }
        }
    }
}