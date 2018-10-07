using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using SkillTreeSystem;
using SkillTreeSystem.Conditions;
using UnityEngine;
using UnityEngine.UI;

public class MageSkillTreeUi : MonoBehaviour
{
    private PlayerInfo playerInfo = new PlayerInfo(1, new MageSkillTree());

    public Text PlayerLevelText;
    public Text SkillText;

    public List<Button> Buttons;

    public void Start()
    {
        var skills = playerInfo.SkillTree.Skills.Values.ToList();
        for (int i = 0; i < playerInfo.SkillTree.Skills.Count; i++)
        {
            var skill = skills[i];
            Buttons[i].onClick.AddListener(() => { playerInfo.SkillTree.UpdateSkill(skill.Id, playerInfo); });
        }
    }

    public void FixedUpdate()
    {
        PlayerLevelText.text = "PlayerLevel: " + playerInfo.Level;
        SkillText.text = "Skills: ";
        var skills = playerInfo.SkillTree.Skills.Values.ToList();
        for (int i = 0; i < playerInfo.SkillTree.Skills.Count; i++)
        {
            var skill = skills[i];
            SkillText.text += "\r\n" + skill.Name + " (" + skill.Id + "): " +
                              playerInfo.SkillTree.GetSkillLevel(skill.Id);

            Buttons[i].interactable = playerInfo.SkillTree.CanUpdateSkill(skill.Id, playerInfo) == CanUpdateResult.Success;
        }
    }

    public void PlayerLevelUpCheat()
    {
        playerInfo.Level++;
    }
}
