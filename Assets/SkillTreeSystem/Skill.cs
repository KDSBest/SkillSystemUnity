using System.Collections;
using Assets.SkillTreeSystem.Language;
using SkillTreeSystem.Conditions;
using UnityEngine;
using UnityEngine.Networking;

namespace SkillTreeSystem
{
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ISkillConditionCheck Condition { get; set; }

        public int MaxLevel { get; set; }

        public Skill(int id, string name, ISkillConditionCheck condition, int maxLevel)
        {
            Id = id;
            Name = name;
            Condition = condition;
            MaxLevel = maxLevel;
        }

        public Skill(int id, string name, string condition, int maxLevel)
        {
            Id = id;
            Name = name;
            Condition = SkillConditionParser.Parse(condition);
            MaxLevel = maxLevel;
        }

        public CanUpdateResult CanUpdate(PlayerInfo playinfo)
        {
            if (Condition == null)
                return CanUpdateResult.Success;

            return Condition.CanUpdate(playinfo);
        }
    }
}