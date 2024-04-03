using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dash Skill Data", menuName = "Puffer Games/Dash Skill Data", order = 1)]
    public class DashSkillDataScriptableObject : ScriptableObject
    {
        public DashSkillData data;
    }
}