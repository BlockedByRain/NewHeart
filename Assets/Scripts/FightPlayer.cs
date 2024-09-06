using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer
{
    //套装

    //称号

    //目镜

    //背包
    public List<Pet> petBag = new List<Pet>(6);

    //当前出战的精灵索引
    public int activePetIndex = 0;
    //玩家是否已选择操作
    public bool actionChosen = false;

    public bool HasChosenAction()
    {
        return actionChosen;
    }

    public void DefaultAction()
    {
        // 这里可以设定为默认攻击或防御
        actionChosen = true;
        Debug.Log("时间用尽，自动执行默认操作！");
    }

    public void ExecuteAction(Pet user, Pet target)
    {
        Pet activePet = GetActivePet();
        SkillInfo chosenSkill = ChooseSkill(); 
        
        // 假设玩家选择了技能
        int playerChoose =0;

        activePet.GetSelectedSkill(playerChoose).Execute(user,target);
        actionChosen = false;
    }

    public Pet GetActivePet()
    {
        return petBag[activePetIndex];
    }

    /// <summary>
    /// 是否所有精灵都阵亡
    /// </summary>
    /// <returns></returns>
    public bool AreAllPetsDefeated()
    {
        foreach (Pet pet in petBag)
        {
            if (pet.fightAbility.HP > 0)
                return false;
        }
        return true;
    }

    public SkillInfo ChooseSkill()
    {
        // 模拟技能选择逻辑，假设选择当前技能列表中的第一个技能
        return GetActivePet().currentSkills[0];
    }
}
