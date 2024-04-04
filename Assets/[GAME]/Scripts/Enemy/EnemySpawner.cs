using System.Collections.Generic;
using _GAME_.Scripts.Core.Manager;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;

namespace _GAME_.Scripts.Enemy
{
    public class EnemySpawner : Manager<EnemySpawner>
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private EnemyBehaviorTree[] enemyPrefabs;

        [SerializeField] public Transform wayPointsParent;
        [SerializeField] private int minEnemyCountConCurrentInMap;
        [SerializeField] private float spawnCheckRadius = 1f;
        [SerializeField] private Vector2 spawnAreaMin;
        [SerializeField] private Vector2 spawnAreaMax;
        [SerializeField] private LayerMask layerMask;

        #endregion

        #region Private Variables

        private Transform _playerTransform;
        private float _spawnDistance = 10f;
        private List<EnemyBehaviorTree> _enemyBehaviorTrees = new();

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.SetPlayerTransform, GetPlayerTransform);
                Register(CustomEvents.EnemyDead, EnemyDead);
            }

            else
            {
                Unregister(CustomEvents.SetPlayerTransform, GetPlayerTransform);
                Unregister(CustomEvents.EnemyDead, EnemyDead);
            }
        }

        private void EnemyDead(object[] arguments)
        {
            EnemyBehaviorTree enemy = (EnemyBehaviorTree)arguments[0];

            if (_enemyBehaviorTrees.Contains(enemy))
            {
                _enemyBehaviorTrees.Remove(enemy);
            }

            if (_enemyBehaviorTrees.Count < minEnemyCountConCurrentInMap)
            {
                SpawnEnemy();
            }
        }

        private void GetPlayerTransform(object[] arguments)
        {
            _playerTransform = (Transform)arguments[0];
            SpawnEnemies();
        }

        #endregion

        #region Private Methods

        private void SpawnEnemies()
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            bool validSpawnFound = false;
            Vector3 spawnPosition = Vector3.zero;
            int attemptCount = 0;
            while (!validSpawnFound && attemptCount < 100)
            {
                float angle = RandomExtensions.GetRandom(0f, Mathf.PI * 2);
                float offset = RandomExtensions.GetRandom(5, 45);
                spawnPosition = new Vector3(
                    _playerTransform.position.x + (_spawnDistance + offset) * Mathf.Cos(angle),
                    _playerTransform.position.y,
                    _playerTransform.position.z + (_spawnDistance + offset) * Mathf.Sin(angle)
                );


                if (spawnPosition.x >= spawnAreaMin.x && spawnPosition.x <= spawnAreaMax.x &&
                    spawnPosition.z >= spawnAreaMin.y && spawnPosition.z <= spawnAreaMax.y)
                {
                    if (!Physics.CheckSphere(spawnPosition, spawnCheckRadius, layerMask))
                    {
                        validSpawnFound = true;
                    }
                }

                attemptCount++;
            }

            if (validSpawnFound)
            {
                EnemyBehaviorTree enemyBehaviorTree = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                    spawnPosition, Quaternion.identity);
                _enemyBehaviorTrees.Add(enemyBehaviorTree);
            }
            else
            {
                Debug.LogWarning("Valid spawn position not found after 100 attempts.");
            }
        }

        #endregion
    }
}