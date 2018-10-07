using System.Runtime.InteropServices;

namespace SkillTreeSystem.Conditions
{
    public class SkillConditionCheck : ISkillConditionCheck
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public SkillConditionCheck(int id, int level)
        {
            Id = id;
            Level = level;
        }

        public CanUpdateResult CanUpdate(PlayerInfo info)
        {
            var currentLevel = info.SkillTree.GetSkillLevel(Id);

            if (currentLevel >= Level)
            {
                return CanUpdateResult.Success;
            }

            return CanUpdateResult.SkillNotHighEnough;
        }
    }
}