using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace playground
{
    [CreateAssetMenu(menuName = "Configs/Asset Provider")]
    public class AssetProvider : ScriptableObject
    {
        public PlayerCharacter PlayerPrefab;
        public GameObject CameraPrefab;

        public LevelDatabase LevelDatabase;
    }
}
