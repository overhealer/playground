using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        private PlayerCharacter _playerCharacter;

        public void Init(AssetProvider assetProvider, GameConfig gameConfig)
        {
            _playerCharacter = Instantiate(assetProvider.PlayerPrefab, transform);
            _playerCharacter.Init(gameConfig);
        }

        public void UpdateLevel()
        {
            _playerCharacter.UpdatePlayer();
        }
    }
}
