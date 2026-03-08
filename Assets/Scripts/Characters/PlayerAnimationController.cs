using UnityEngine;

namespace playground.Assets.Scripts.Characters
{
    public class PlayerAnimationController :
        MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private int moveXAnimationHash = Animator.StringToHash("MoveX");
        private int moveYAnimationHash = Animator.StringToHash("MoveY");

        public void SetMoveAxisValue(float x, float y)
        {
            animator.SetFloat(moveXAnimationHash, x);
            animator.SetFloat(moveYAnimationHash, y);
        }
    }
}