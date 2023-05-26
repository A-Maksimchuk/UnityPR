using System;
using DI;
using GameLogic.Skills;
using Hero;
using Services;
using UnityEngine;

namespace HeroLogic
{
    public class Hero:MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private HeroMove heroMove;
        [SerializeField] private HeroAnimator heroAnimator;
        [SerializeField] private SkillData[] skills;
        [SerializeField] private Transform skillSpawnPoint;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => _currentHealth;
        public Action<Hero> OnDamaged;
        public Action OnDie;
        
        private int _currentSkill = 0;
        private float _currentHealth;
        private float _skillCoolDown = 0;
        private IInputService _inputService;
        private bool _isDie = false;

        private void Start()
        {
            _currentHealth = maxHealth;
            _inputService = Container.Get<IInputService>();
            _inputService.OnFire += Fire;
        }

        private void Fire(Vector2 point)
        {
            if(_isDie)
                return;
            heroMove.LookAt(point);
            if(_skillCoolDown>0)
                return;
            _skillCoolDown = skills[_currentSkill].coolDown;
            heroAnimator.Attack1();
            skills[_currentSkill].HandleSkill(skillSpawnPoint);
        }

        public void Attack(float power)
        {
            if(_isDie)
                return;
            _currentHealth -= power;
            OnDamaged?.Invoke(this);
            if(_currentHealth>0)
                heroAnimator.Hit();
            else
            {
                heroAnimator.Die();
                OnDie.Invoke();
                heroMove.enabled = false;
                _isDie = true;
            }
        }

        public void SwitchSkill()
        {
            _currentSkill++;
            if (_currentSkill > skills.Length - 1)
                _currentSkill = 0;
        }

        private void Update()
        {
            if (_skillCoolDown > 0)
                _skillCoolDown -= Time.deltaTime;
        }
    }
}