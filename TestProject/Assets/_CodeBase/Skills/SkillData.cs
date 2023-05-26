using System;
using UnityEngine;

namespace GameLogic.Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Game/SkillData", order = 0)]
    public class SkillData : ScriptableObject
    {
        [SerializeField] private Skill skillPrefab;
        [SerializeField] private SkillParameters[] skillEntities;
        [SerializeField] public float coolDown;

        public void HandleSkill(Transform spawner)
        {
            foreach (SkillParameters skillEntity in skillEntities)
            {
                var skill = Instantiate(skillPrefab, spawner.position+skillEntity.offset, Quaternion.identity);
                skill.SetParameters(skillEntity, spawner);
            }
        }
    }

    [Serializable]
    public struct SkillParameters
    {
        public Vector3 direction;
        public Vector3 offset;
        public float speed;
        public float power;
        public float destroyTime;
    }
}