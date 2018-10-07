namespace SkillTreeSystem
{
    public class PlayerInfo
    {
        public int Level { get; set; }

        public SkillTree SkillTree { get; set; }

        public PlayerInfo(int level, SkillTree skillTree)
        {
            Level = level;
            SkillTree = skillTree;
        }
    }
}