using Assets.Scripts.Game.Services;
using playground.Assets.Scripts.Configs;
using UnityEngine;

namespace playground.Assets.Scripts.Core.Services
{
    public class PlayerService :
        Service
    {
        public GameObject Player { get; private set; }

        public void SpawnPlayer()
        {
            PlayerConfig playerConfig = PlayerConfig.Instance;

            var player = GameInstance.CreateObject(playerConfig.PlayerPrefab, Vector3.zero, Vector3.zero);
            Player = player;
        }
    }
}