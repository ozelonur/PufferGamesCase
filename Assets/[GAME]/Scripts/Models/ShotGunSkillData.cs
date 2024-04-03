using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class ShotGunSkillData
    {
        [Range(15, 45)] public float angle;
        [Range(0, 10)] public float range;
        [Range(1, 50)] public float baseDamage;
        [Range(1, 10)] public float pushBackLength;
        [Range(1, 20)] public float coolDown;
        [Range(1, 20)] public int pelletCount;
    }
}