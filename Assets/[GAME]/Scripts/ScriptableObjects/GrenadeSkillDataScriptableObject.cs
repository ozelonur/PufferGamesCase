using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Grenade Skill Data", menuName = "Puffer Games/Grenade Skill Data", order = 1)]
    public class GrenadeSkillDataScriptableObject : ScriptableObject
    {
        public GrenadeSkillData data;
    }
}