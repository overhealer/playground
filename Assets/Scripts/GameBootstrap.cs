using overhealer.Core;
using UnityEngine;

namespace playground.Assets.Scripts
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField]
        private UIService uiService;

        private GameInstance gameInstance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            //gameInstance = new GameInstance();
            gameInstance.StartStateMachine();
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