using System;
using UnityEngine;

namespace HeroLogic
{
    public class HeroAnimator:MonoBehaviour
    {
        [SerializeField] public Animator animator;
        [SerializeField] private CharacterController characterController;
        
        private static readonly int MoveHash = Animator.StringToHash("IsWalk");
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int Attack1Hash = Animator.StringToHash("Attack1");
        private static readonly int Attack2Hash = Animator.StringToHash("Attack2");
        
        private void Update()
        {
            animator.SetBool(MoveHash, characterController.velocity.magnitude>0.001f);
        }

        public void Die()
        {
            animator.SetTrigger(DieHash);
        }

        public void Hit()
        {
            animator.SetTrigger(HitHash);
        }

        public void Attack1()
        {
            animator.SetTrigger(Attack1Hash);
        }
        
        public void Attack2()
        {
            animator.SetTrigger(Attack2Hash);
        }
    }
}