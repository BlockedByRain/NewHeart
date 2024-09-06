using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 精灵
/// </summary>
public class Pet
{
    /// <summary>
    /// 精灵id
    /// </summary>
    public int petId;

    /// <summary>
    /// 精灵名
    /// </summary>
    public string petName;


    /// <summary>
    /// 精灵等级
    /// </summary>
    public int Lv;

    /// <summary>
    /// 下级所需经验
    /// </summary>
    public int nextLvExp;

    /// <summary>
    /// 种族值
    /// </summary>
    public RacialSixDimensions racial;


    /// <summary>
    /// 能力值
    /// </summary>
    public AbilitySixDimensions ability;


    /// <summary>
    /// 努力值
    /// </summary>
    public EffortSixDimensions effort;


    /// <summary>
    /// 额外能力值
    /// </summary>
    public AbilitySixDimensions extra;


    /// <summary>
    /// 性格
    /// </summary>
    public Personality personality;

    /// <summary>
    /// 特性
    /// </summary>
    public Feature feature;

    /// <summary>
    /// 可用技能组
    /// </summary>
    public List<Skill> availableSkills;

    /// <summary>
    /// 当前技能组
    /// </summary>
    public List<Skill> currentSkills;

    /// <summary>
    /// 魂印id
    /// </summary>
    public string soulSealId;

    /// <summary>
    /// 能力等级
    /// </summary>
    public AbilityLvSixDimensions abilityLvSixDimensionsValue;

    /// <summary>
    /// 战斗中能力值
    /// </summary>
    public AbilityLvSixDimensions fightAbility;



    /// <summary>
    /// 刷新能力
    /// </summary>
    public void RefreshCapability()
    {
        PersonalityEffectsSixDimensions personalityEffects = PersonalityEffects.GetEffect(this.personality);
        // 计算能力值
        ability.PhysicalAttack = CalculateState(racial.PhysicalAttack, effort.PhysicalAttack, 31, Lv, personalityEffects.PhysicalAttack, extra.PhysicalAttack);
        ability.SpecialAttack = CalculateState(racial.SpecialAttack, effort.SpecialAttack, 31, Lv, personalityEffects.SpecialAttack, extra.SpecialAttack);
        ability.PhysicalDefense = CalculateState(racial.PhysicalDefense, effort.PhysicalDefense, 31, Lv, personalityEffects.PhysicalDefense, extra.PhysicalDefense);
        ability.SpecialDefense = CalculateState(racial.SpecialDefense, effort.SpecialDefense, 31, Lv, personalityEffects.SpecialDefense, extra.SpecialDefense);
        ability.Speed = CalculateState(racial.Speed, effort.Speed, 31, Lv, personalityEffects.Speed, extra.Speed);

        // 计算体力值
        ability.HP = CalculateState(racial.HP, effort.HP, 100 + 31, Lv, personalityEffects.HP, extra.HP);
    

}

    /// <summary>
    /// 能力值计算
    /// </summary>
    /// <param name="racialValue"></param>
    /// <param name="effortValue"></param>
    /// <param name="additionalValue"></param>
    /// <param name="level"></param>
    /// <param name="personalityEffect"></param>
    /// <param name="extraValue"></param>
    /// <returns></returns>
    private int CalculateState(float racialValue, float effortValue, float additionalValue, int level, float personalityEffect, float extraValue)
    {
        //常规能力值=【【【(种族值*2+努力值÷4+31)*(精灵等级÷100)+5】*性格修正】*套装百分比（暂无）】+外部加成
        //体力能力值=【【(种族值*2+努力值÷4+100+31)*(精灵等级÷100)+10】*套装百分比加成（暂无）】+外部加成
        return (int)(((((racialValue * 2) + (effortValue / 4) + additionalValue) * (level / 100) + 5) * personalityEffect) + extraValue);
    }

    /// <summary>
    /// 打印当前精灵的所有状态
    /// </summary>
    public void PrintStatus()
    {
        Debug.Log("=== 精灵状态 ===");
        Debug.Log($"ID: {petId}");
        Debug.Log($"名字: {petName}");
        Debug.Log($"等级: {Lv}");
        Debug.Log($"下级所需经验: {nextLvExp}");

        Debug.Log("种族值:");
        Debug.Log($"物攻: {racial.PhysicalAttack}");
        Debug.Log($"特攻: {racial.SpecialAttack}");
        Debug.Log($"物防: {racial.PhysicalDefense}");
        Debug.Log($"特防: {racial.SpecialDefense}");
        Debug.Log($"速度: {racial.Speed}");
        Debug.Log($"体力: {racial.HP}");

        Debug.Log("努力值:");
        Debug.Log($"物攻: {effort.PhysicalAttack}");
        Debug.Log($"特攻: {effort.SpecialAttack}");
        Debug.Log($"物防: {effort.PhysicalDefense}");
        Debug.Log($"特防: {effort.SpecialDefense}");
        Debug.Log($"速度: {effort.Speed}");
        Debug.Log($"体力: {effort.HP}");

        Debug.Log("性格:");
        Debug.Log($"{personality}");

        //Debug.Log("性格影响:");
        //var personalityEffects = PersonalityEffects.GetEffect(personality);
        //Debug.Log($"物攻修正: {personalityEffects.PhysicalAttack}");
        //Debug.Log($"特攻修正: {personalityEffects.SpecialAttack}");
        //Debug.Log($"物防修正: {personalityEffects.PhysicalDefense}");
        //Debug.Log($"特防修正: {personalityEffects.SpecialDefense}");
        //Debug.Log($"速度修正: {personalityEffects.Speed}");
        //Debug.Log($"体力修正: {personalityEffects.HP}");

        Debug.Log("额外能力值:");
        Debug.Log($"物攻: {extra.PhysicalAttack}");
        Debug.Log($"特攻: {extra.SpecialAttack}");
        Debug.Log($"物防: {extra.PhysicalDefense}");
        Debug.Log($"特防: {extra.SpecialDefense}");
        Debug.Log($"速度: {extra.Speed}");
        Debug.Log($"体力: {extra.HP}");

        Debug.Log("能力值:");
        Debug.Log($"物攻: {ability.PhysicalAttack}");
        Debug.Log($"特攻: {ability.SpecialAttack}");
        Debug.Log($"物防: {ability.PhysicalDefense}");
        Debug.Log($"特防: {ability.SpecialDefense}");
        Debug.Log($"速度: {ability.Speed}");
        Debug.Log($"体力: {ability.HP}");
        Debug.Log("特性:"+ $"{feature}");

        //Debug.Log("可用技能:");
        //foreach (var skill in availableSkills)
        //{
        //    Debug.Log($"{skill}");
        //}

        //Debug.Log("当前技能:");
        //foreach (var skill in currentSkills)
        //{
        //    Debug.Log($"{skill}");
        //}

        Debug.Log($"魂印ID: {soulSealId}");
        Debug.Log("=== 精灵状态 ===");
    }


}






