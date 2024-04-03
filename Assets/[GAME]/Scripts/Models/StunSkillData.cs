using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class StunSkillData
    {
        [Range(1, 20)] public float impactAreaRadius;
        [Range(0, 5)] public float stunTime;
        [Range(1, 20)] public float coolDown;
    }
}