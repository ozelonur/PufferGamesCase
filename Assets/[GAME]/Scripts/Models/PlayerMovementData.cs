using System;
using UnityEngine;

namespace _GAME_.Scripts.Models
{
    [Serializable]
    public class PlayerMovementData
    {
        [Range(0, 20)] public float speed;
        [Range(0, 1000)] public float turnSpeed;
        [Range(0, 50)] public float mouseSensitivity;
    }
}