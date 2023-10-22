using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        private PlayerCharacter _playerCharacter;

        public virtual void Init(AssetProvider assetProvider, GameConfig gameConfig)
        {
            _playerCharacter = Instantiate(assetProvider.PlayerPrefab, _playerSpawnPoint.position, Quaternion.identity, transform);
            _playerCharacter.Init(gameConfig);
        }

        public virtual void UpdateLevel()
        {
            _playerCharacter.UpdatePlayer();
        }
    }
}
