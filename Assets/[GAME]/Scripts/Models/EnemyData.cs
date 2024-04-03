using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class EnemyData
    {
        [Range(0, 100)] public float maxHealth;
        [Range(0, 10)] public int damagePower;
        [Range(0, 10)] public float visionRadius;
        [Range(0, 10)] public float patrolPointCircleRadius;
        [Range(1, 10)] public int patrolPointMinCount;
        [Range(1, 10)] public int patrolPointMaxCount;
        [Range(1, 10)] public float minWaitTime;
        [Range(1, 10)] public float maxWaitTime;
    }
}