using System.Collections.Generic;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;

namespace _GAME_.Scripts.Helpers
{
    public static class SkillCooldownHelper
    {
        private static Dictionary<SkillType, float> lastSkillUseTime = new();

        public static void RecordSkillUse(SkillType skillType)
        {
            if (lastSkillUseTime.ContainsKey(skillType))
            {
                lastSkillUseTime[skillType] = Time.time;
            }
            else
            {
                lastSkillUseTime.Add(skillType, Time.time);
            }
        }

        public static bool IsSkillOnCooldown(SkillType skillType, float cooldownDuration)
        {
            if (lastSkillUseTime.TryGetValue(skillType, out float lastUseTime))
            {
                return Time.time - lastUseTime < cooldownDuration;
            }
            return false;
        }
    }
}