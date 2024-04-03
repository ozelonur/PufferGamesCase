using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class PlayerBaseAttackData
    {
        [Range(5, 15)] public float radius;
        [Range(20, 40)] public int magazineCapacity;
        [Range(1, 20)] public float rotationSpeed;
        [Range(0, 1)] public float rotationThreshold;
        [Range(1, 10)] public float baseDamage;
        [Range(100, 2000)] public int maxHealth;
    }
}