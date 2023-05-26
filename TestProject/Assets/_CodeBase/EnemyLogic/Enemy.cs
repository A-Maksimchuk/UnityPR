using System;
using System.Collections;
using System.Threading.Tasks;
using DI;
using GameLogic.Skills;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy:MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] public new Collider collider;
        [SerializeField] private float maxHealth;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackDistance = 2;
        [SerializeField] private float power = 10;
        [SerializeField] private float attackCooldown = 3;
        [SerializeField] private Animator animator;
        private HeroLogic.Hero playerHero;
        private float _currentHealth;
        private float _currentCooldown;
        private Transform _transform;
        private bool isDie = false;

        private void Awake()
        {
            _currentHealth = maxHealth;
            _transform = transform;
        }

        private void Start()
        {
            playerHero = Container.Get<HeroLogic.Hero>();
        }

        private void OnDestroy()
        {
            Container.Get<EnemyFactory>().RemoveEnemy(collider);
        }

        private void Update()
        {
            if(isDie)
                return;
            var target = playerHero.transform.position;
            var currentPosition = _transform.position;
            if (Vector3.Distance(target, currentPosition)>attackDistance)
            {
                _transform.position = Vector3.MoveTowards(currentPosition, target,
                    moveSpeed * Time.deltaTime);
            }
            else
            {
                if (_currentCooldown <= 0)
                {
                    Atack();
                }
            }

            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
            }
            _transform.forward = (target - currentPosition).normalized;
        }

        public void Hit(SkillParameters skillParameters)
        {
            _currentHealth -= skillParameters.power;
            if (_currentHealth <= 0)
                Die();
            else
            {
                animator.SetTrigger("Block");
            }
        }

        void Atack()
        {
            _currentCooldown = attackCooldown;
            animator.SetTrigger("Attack");
            playerHero.Attack(power);
        }

        private void Die()
        {
            isDie = true;
            collider.enabled = false;
            animator.SetTrigger("Die");
            StartCoroutine(WaitAndDestroy());
        }

        IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}