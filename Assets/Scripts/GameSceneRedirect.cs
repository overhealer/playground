using overhealer.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace playground.Assets.Scripts.Core
{
    public class GameSceneRedirect :
        MonoBehaviour
    {
        private void Awake()
        {
            if (ServiceLocator.Instance.Services.Count == 0)
            {
                SceneManager.LoadScene("Bootstrap");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}