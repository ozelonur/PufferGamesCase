using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Stun Skill Data", menuName = "Puffer Games/Stun Skill Data", order = 1)]
    public class StunSkillDataScriptableObject : ScriptableObject
    {
        public StunSkillData data;
    }
}