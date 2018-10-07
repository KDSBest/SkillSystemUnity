namespace SkillTreeSystem.Conditions
{
    public class PlayerLevelConditionCheck : ISkillConditionCheck
    {
        public int Level { get; set; }

        public PlayerLevelConditionCheck(int level)
        {
            Level = level;
        }

        public CanUpdateResult CanUpdate(PlayerInfo info)
        {
            if (info.Level >= Level)
            {
                return CanUpdateResult.Success;
            }

            return CanUpdateResult.PlayerLevelNotHighEnough;
        }
    }
}