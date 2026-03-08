using Assets.Scripts.Game.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace playground.Assets.Scripts.Core.Services
{
    public class SceneLoadService :
            Service
    {
        public void LoadScene(LevelPayload payload)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(payload.SceneToLoad);
            op.completed += RegisterObjects;
            if (payload.OnLoaded != null)
            {
                op.completed += (async) => payload.OnLoaded.Invoke();
            }
        }

        private void RegisterObjects(AsyncOperation op)
        {
            if (SceneObjects.Instance == null)
                return;

            foreach (var init in SceneObjects.Instance.Initialisables)
            {
                GameInstance.RegisterObject(init);
            }

            foreach (var update in SceneObjects.Instance.Updatables)
            {
                if (SceneObjects.Instance.Initialisables.Contains(update))
                    continue;

                GameInstance.RegisterObject(update);
            }
        }
    }
}