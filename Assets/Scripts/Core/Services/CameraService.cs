using Assets.Scripts.Game.Services;
using Cinemachine;
using playground.Assets.Scripts.Configs;
using UnityEngine;

namespace playground.Assets.Scripts.Core.Services
{
    public class CameraService :
        Service
    {
        public Camera CurrentCamera { get; private set; }

        public CinemachineVirtualCamera CurrentVirtualCamera { get; private set; }

        public CinemachineBrain CameraBrain { get; private set; }

        public void SpawnCamera()
        {
            PlayerConfig playerConfig = PlayerConfig.Instance;

            var cameraContainer = GameInstance.CreateObject(playerConfig.CameraContainerPrefab, Vector3.zero, Vector3.zero);

            CurrentCamera = cameraContainer.GetComponentInChildren<Camera>();
            CurrentVirtualCamera = cameraContainer.GetComponentInChildren<CinemachineVirtualCamera>();
            CameraBrain = cameraContainer.GetComponentInChildren<CinemachineBrain>();
        }
    }
}