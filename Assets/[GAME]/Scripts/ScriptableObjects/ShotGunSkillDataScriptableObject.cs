using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Shotgun Skill Data", menuName = "Puffer Games/Shotgun Skill Data", order = 1)]
    public class ShotGunSkillDataScriptableObject : ScriptableObject
    {
        public ShotGunSkillData data;
    }
}