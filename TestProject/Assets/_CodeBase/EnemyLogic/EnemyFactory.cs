using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic
{
    public class EnemyFactory:MonoBehaviour
    {
        [SerializeField] private EnemyPreset[] enemyPresets;
        [SerializeField] private float enemyCreateDelay = 5;
        private Dictionary<Collider, Enemy> _enemyRegister = new Dictionary<Collider, Enemy>();
        private int weightSumm = 0;
        private WaitForSeconds delay;

        private void Awake()
        {
            foreach (EnemyPreset enemyPreset in enemyPresets)
            {
                weightSumm += enemyPreset.probabilityWeight;
            }
        }

        private void Start()
        {
            delay = new WaitForSeconds(enemyCreateDelay);
            StartCoroutine(EnemyCreator());
            
        }
        
        private Vector3 GetRandomPosOffScreen(float z=0) {
            bool isXAxis = Random.Range(1f, 10f) < 5f; 
            bool isLeftSide = Random.Range(1f, 10f) < 5f; 
            bool isUpSide = Random.Range(1f, 10f) < 5f;
            
            float screenHeight = Camera.main.orthographicSize * 2.0f;
            float screenWidth = screenHeight * Camera.main.aspect;
            
            float offset = 2f;
            
            float minusWidth = -screenWidth / 2f - offset;
            float plusWidth = screenWidth / 2f + offset;
            float minusHeight = -screenHeight / 2f - offset;
            float plusHeight = screenHeight / 2f + offset;
 
            if (isXAxis) {
                return new(isLeftSide ? minusWidth : plusWidth, Random.Range(minusHeight, plusHeight), z);
            } else {
                return new(Random.Range(minusWidth, plusWidth), isUpSide ? minusHeight : plusHeight, z);
            }
        }

        private Vector3 ClampPositionToField(Vector3 position)
        {
            return new Vector3(Mathf.Clamp(position.x, -50, 50), 0, Mathf.Clamp(position.z, -50, 50));
        }

        IEnumerator EnemyCreator()
        {
            while (true)
            {
                yield return delay;
                CreateEnemy();
            }
        }

        public void CreateEnemy()
        {
            var position = Camera.main.ViewportToWorldPoint(GetRandomPosOffScreen(20));
            position = ClampPositionToField(position);
            var enemy = Instantiate(GetRandomEnemyPrefab(), position, Quaternion.identity);
            _enemyRegister.Add(enemy.collider, enemy);
        }

        public Enemy GetEnemy(Collider collider)
        {
            return _enemyRegister.ContainsKey(collider) ? _enemyRegister[collider] : null;
        }

        public void RemoveEnemy(Collider collider)
        {
            if (_enemyRegister.ContainsKey(collider))
                _enemyRegister.Remove(collider);
        }

        private Enemy GetRandomEnemyPrefab()
        {
            var id = Random.Range(0, weightSumm);
            for (var i = 0; i < enemyPresets.Length; i++)
            {
                id -= enemyPresets[i].probabilityWeight;
                if (id <= 0)
                    return enemyPresets[i].prefab;
            }

            return enemyPresets[0].prefab;
        }
    }

    [Serializable]
    public struct EnemyPreset
    {
        public Enemy prefab;
        public int probabilityWeight;
    }
}