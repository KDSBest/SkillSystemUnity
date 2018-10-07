namespace SkillTreeSystem.Conditions
{
    public class OrConditionCheck : ISkillConditionCheck
    {
        public ISkillConditionCheck A { get; set; }
        public ISkillConditionCheck B { get; set; }

        public OrConditionCheck(ISkillConditionCheck a, ISkillConditionCheck b)
        {
            A = a;
            B = b;
        }

        public CanUpdateResult CanUpdate(PlayerInfo info)
        {
            var result = A.CanUpdate(info);
            if (result == CanUpdateResult.Success)
            {
                return result;
            }

            result = B.CanUpdate(info);
            if (result == CanUpdateResult.Success)
            {
                return result;
            }

            return CanUpdateResult.CombinedIssue;
        }
    }
}