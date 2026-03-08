using System;

namespace playground.Assets.Scripts.Core
{
    public struct LevelPayload
    {
        public string SceneToLoad;
        public Action OnLoaded;

        public LevelPayload(string sceneName, Action onLoaded = null)
        {
            SceneToLoad = sceneName;
            OnLoaded = onLoaded;
        }
    }
}