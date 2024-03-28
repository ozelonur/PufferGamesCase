using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GAME_.Scripts.Enemy
{
    public class EnemySpawner : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private EnemyController enemyPrefab;

        #endregion

        #region Private Variables

        private Transform _playerTransform;
        private float _spawnDistance = 10f;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.SetPlayerTransform, GetPlayerTransform);
            }

            else
            {
                Unregister(CustomEvents.SetPlayerTransform, GetPlayerTransform);
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
            for (int i = 0; i < 20; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            float angle = Random.Range(0f, Mathf.PI * 2);

            float offset = Random.Range(5, 40);
            Vector3 spawnPosition = new(
                _playerTransform.position.x + (_spawnDistance + offset) * Mathf.Cos(angle),
                _playerTransform.position.y,
                _playerTransform.position.z + (_spawnDistance + offset) * Mathf.Sin(angle)
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        #endregion
    }
}