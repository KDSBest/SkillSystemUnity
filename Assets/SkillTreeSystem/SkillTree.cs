using System.Collections.Generic;
using System.Diagnostics;
using SkillTreeSystem.Conditions;

namespace SkillTreeSystem
{
    public class SkillTree
    {
        /// <summary>
        /// Gets or sets the skills.
        ///     Key: Skill Id
        ///     Value: Skill
        /// </summary>
        public Dictionary<int, Skill> Skills { get; set; }

        /// <summary>
        /// The skill levels:
        ///       Key: Skill Id
        ///       Value: Skill Level 
        /// </summary>
        private Dictionary<int, int> skillLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillTree"/> class.
        /// </summary>
        public SkillTree()
        {
            Skills = new Dictionary<int, Skill>();
            skillLevels = new Dictionary<int, int>();
        }

        /// <summary>
        /// Initializes the specified skills and the Skills cache by id
        /// </summary>
        /// <param name="skills">The skills.</param>
        public void Initialize(List<Skill> skills)
        {
            foreach (var skill in skills)
            {
                Skills.Add(skill.Id, skill);
            }
        }

        /// <summary>
        /// Gets the skill level.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int GetSkillLevel(int id)
        {
            if (skillLevels.ContainsKey(id))
                return skillLevels[id];

            return 0;
        }

        /// <summary>
        /// Determines whether Skill can be updated
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="playinfo">The playinfo.</param>
        /// <returns></returns>
        public CanUpdateResult CanUpdateSkill(int id, PlayerInfo playinfo)
        {
            if (GetSkillLevel(id) >= Skills[id].MaxLevel)
                return CanUpdateResult.AlreadyMaxLevel;

            return Skills[id].CanUpdate(playinfo);
        }

        /// <summary>
        /// Updates the skill.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="playinfo">The playinfo.</param>
        /// <returns></returns>
        public bool UpdateSkill(int id, PlayerInfo playinfo)
        {
            var canUpdateResult = CanUpdateSkill(id, playinfo);
            if (canUpdateResult != CanUpdateResult.Success)
            {
                UnityEngine.Debug.Log("Can't update skill because of " + canUpdateResult);
                return false;
            }

            if (!skillLevels.ContainsKey(id))
            {
                skillLevels.Add(id, 1);
                return true;
            }

            skillLevels[id]++;
            return true;
        }
    }
}