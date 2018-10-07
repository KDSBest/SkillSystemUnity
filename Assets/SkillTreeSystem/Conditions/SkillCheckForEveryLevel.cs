using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SkillTreeSystem.Conditions
{
    public class SkillCheckForEveryLevel : ISkillConditionCheck
    {
        public int Id { get; set; }

        public Dictionary<int, ISkillConditionCheck> ConditionChecks { get; set; }

        public SkillCheckForEveryLevel(int id, Dictionary<int, ISkillConditionCheck> conditionChecks)
        {
            Id = id;
            ConditionChecks = conditionChecks;
        }

        public CanUpdateResult CanUpdate(PlayerInfo info)
        {
            var currentLevel = info.SkillTree.GetSkillLevel(Id);

            var keys = ConditionChecks.Keys.ToList();
            keys.Sort();
            keys.Reverse();

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] <= currentLevel)
                {
                    if (ConditionChecks[keys[i]] == null)
                        return CanUpdateResult.Success;

                    return ConditionChecks[keys[i]].CanUpdate(info);
                }
            }

            return CanUpdateResult.Success;
        }
    }
}