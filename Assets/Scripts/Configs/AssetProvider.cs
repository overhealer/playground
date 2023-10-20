using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace playground
{
    [CreateAssetMenu(menuName = "Configs/Asset Provider")]
    public class AssetProvider : ScriptableObject
    {
        public GameObject CameraPrefab;
    }
}
