using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class DashSkillData
    {
        [Range(1, 10)] public float dashDistance;
        [Range(1, 20)] public float coolDown;
    }
}