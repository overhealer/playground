
using playground.Assets.Scripts.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Game.Services
{
    public class SceneObjects : Singleton<SceneObjects>
    {
        public List<GameObject> Initialisables;
        public List<GameObject> Updatables;

#if UNITY_EDITOR
        [ContextMenu("Register Objects")]
        public void RegisterObjects()
        {
            Initialisables.Clear();
            Updatables.Clear();

            var initialisables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IInitialisable>().ToList();
            foreach (var initialisable in initialisables)
            {
                Initialisables.Add((initialisable as MonoBehaviour).gameObject);
            }

            var updatables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IUpdatable>().ToList();
            foreach (var updatable in updatables)
            {
                Updatables.Add((updatable as MonoBehaviour).gameObject);
            }

            EditorApplication.MarkSceneDirty();
        }
#endif
    }
}