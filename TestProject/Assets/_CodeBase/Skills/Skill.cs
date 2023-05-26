using System;
using DI;
using EnemyLogic;
using UnityEngine;

namespace GameLogic.Skills
{
    public class Skill : MonoBehaviour
    {
        private float _destroyTime = int.MaxValue;
        private SkillParameters _skillParameters;

        public void SetParameters(SkillParameters skillParameters, Transform spawner)
        {
            _skillParameters = skillParameters;
            _skillParameters.direction = spawner.TransformDirection(_skillParameters.direction);
            _destroyTime = _skillParameters.destroyTime;
        }

        private void Update()
        {
            transform.position += _skillParameters.direction * _skillParameters.speed * Time.deltaTime;
            _destroyTime -= Time.deltaTime;
            if(_destroyTime<=0)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Environment"))
                Destroy(gameObject);
            if (other.CompareTag("Enemy"))
            {
                Container.Get<EnemyFactory>().GetEnemy(other)?.Hit(_skillParameters);
                Destroy(gameObject);
            }
        }
    }
}