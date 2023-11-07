using UnityEngine;

namespace CharacterSystem
{
    public class AnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Death = Animator.StringToHash("Death");

        public void SetIdle(bool value)
        {
            animator.SetBool(Idle, value);
        }

        public void SetRun(bool value)
        {
            animator.SetBool(Run, value);
        }

        public void SetDeath()
        {
            animator.SetTrigger(Death);
        }

        public void Flip(bool value)
        {
            spriteRenderer.flipX = value;
        }
    }
}