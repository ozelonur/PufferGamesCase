using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class GrenadeSkillData
    {
        [Range(1, 10)] public float bombImpactRadius;
        [Range(1, 100)] public float bombBaseDamage;
        [Range(1, 20)] public float coolDown;
    }
}