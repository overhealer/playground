using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    [CreateAssetMenu(menuName = "Configs/Asset Provider", fileName = "Asset Provider")]
    public class AssetProvider : ScriptableObject {
        [Header("Main")]
        public GameObject PlayerPrefab;
        public GameObject CameraPrefab;
    }
}