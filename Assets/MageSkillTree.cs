using System.Collections.Generic;
using SkillTreeSystem;
using SkillTreeSystem.Conditions;

namespace Assets
{
    public class MageSkillTree : SkillTree
    {
        public MageSkillTree()
        {
            this.Initialize(new List<Skill>()
            {
                new Skill(1, "Fireball", "SkillForLevel[1](0: Level(1), 3: Level(5), 5: Skill[2](2) & Level(10))", 10),
                new Skill(2, "FireballAoe", "Skill[1](2)", 10),
                new Skill(3, "GreaterFireballAoe", "Skill[2](2) & Level(10)", 10)
            });
        }
    }
}
