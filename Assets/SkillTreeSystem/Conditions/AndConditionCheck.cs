namespace SkillTreeSystem.Conditions
{
    public class AndConditionCheck : ISkillConditionCheck
    {
        public ISkillConditionCheck A { get; set; }
        public ISkillConditionCheck B { get; set; }

        public AndConditionCheck(ISkillConditionCheck a, ISkillConditionCheck b)
        {
            A = a;
            B = b;
        }

        public CanUpdateResult CanUpdate(PlayerInfo info)
        {
            var result = A.CanUpdate(info);

            if (result != CanUpdateResult.Success)
            {
                return CanUpdateResult.CombinedIssue;
            }

            result = B.CanUpdate(info);

            if (result != CanUpdateResult.Success)
            {
                return CanUpdateResult.CombinedIssue;
            }

            return CanUpdateResult.Success;
        }
    }
}