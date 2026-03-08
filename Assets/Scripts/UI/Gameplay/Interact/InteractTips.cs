using playground.Assets.Scripts.Core.Interfaces;
using UnityEngine;

namespace playground.Assets.Scripts.UI.Gameplay.Interact
{
    public class InteractTips :
            MonoBehaviour,
            IInitialisable
    {
        [SerializeField]
        private Transform tipsContainer;

        //private PlayerInteractionController interactionController;
        private Camera camera;

        public void Init()
        {
            /*var playerService = ServiceLocator.Instance.Get<PlayerService>();
            interactionController = playerService.InteractionController;
            camera = playerService.PlayerCamera.Camera;*/
        }
    }
}