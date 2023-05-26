using System;
using CameraLogic;
using DI;
using EnemyLogic;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private HeroLogic.Hero heroPrefab;
        [SerializeField] private Transform heroSpawnPoint;
        [SerializeField] private EnemyFactory enemyFactory;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Button switchSkillButton;
        [SerializeField] private GameObject gameOverPanel;

        private void Start()
        {
            var hero = Instantiate(heroPrefab, heroSpawnPoint.position, heroSpawnPoint.rotation);
            cameraFollow.Follow(hero.gameObject);
            healthBar.SetValue(hero.MaxHealth, hero.MaxHealth);
            hero.OnDamaged += (x) =>
            {
                healthBar.SetValue(hero.CurrentHealth, hero.MaxHealth);
            };
            switchSkillButton.onClick.AddListener(hero.SwitchSkill);
            Container.Register(hero);
            Container.Register(enemyFactory);
            hero.OnDie +=()=>gameOverPanel.SetActive(true);
        }
    }
}