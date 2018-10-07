namespace Assets.SkillTreeSystem.Language
{
    public class SkillConditionToken
    {
        public string Value { get; set; }

        public SkillConditionTokenType Type { get; set; }

        public SkillConditionToken(SkillConditionTokenType type)
        {
            Type = type;
            Value = string.Empty;
        }

        public SkillConditionToken(SkillConditionTokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}