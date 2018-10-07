namespace SkillTreeSystem.Conditions
{
    public interface ISkillConditionCheck
    {
        CanUpdateResult CanUpdate(PlayerInfo info);
    }
}