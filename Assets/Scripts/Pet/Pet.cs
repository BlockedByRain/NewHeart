using System;
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
    public int petId = 1;

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
    /// 精灵属性
    /// </summary>
    public int attribute { get; private set; }

    public void SetAttribute(int attributeId)
    {
        // 验证属性有效性
        var attr = AttributeSystem.GetAttribute(attributeId);
        attribute = attributeId;
    }

    public float GetDamageMultiplier(Pet defender)
    {
        return AttributeSystem.GetMultiplier(this.attribute, defender.attribute);
    }

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
    public List<SkillInfo> availableSkills = null;

    /// <summary>
    /// 当前技能组
    /// </summary>
    public List<SkillInfo> currentSkills = null;

    /// <summary>
    /// 魂印id
    /// </summary>
    public string soulSealId = null;

    /// <summary>
    /// 能力等级
    /// </summary>
    public AbilityLvSixDimensions abilityLvSixDimensionsValue = null;

    /// <summary>
    /// 战斗中能力值,其中Hp作为战斗中最大体力上限
    /// </summary>
    public AbilitySixDimensions fightAbility = null;

    /// <summary>
    /// 战斗中当前体力
    /// </summary>
    public int fightAbilityCurHp;

    /// <summary>
    /// 精灵Buff
    /// </summary>
    public List<BuffInfo> buffInfos = null;

    /// <summary>
    /// 返回选择使用的技能
    /// </summary>
    public SkillInfo GetSelectedSkill(int skillIndex)
    {
        return currentSkills[skillIndex];
    }



    /// <summary>
    /// 刷新能力
    /// </summary>
    public void RefreshCapability()
    {
        // 获取性格修正系数
        var personalityEffect = PersonalityEffects.GetEffect(this.personality);

        // 计算常规能力值
        ability.PhysicalAttack = AbilityCalculator.CalculateNormal(
            racial.PhysicalAttack,
            effort.PhysicalAttack,
            Lv,
            personalityEffect.physicalAttackMultiplier,
            extra.PhysicalAttack
        );

        ability.SpecialAttack = AbilityCalculator.CalculateNormal(
            racial.SpecialAttack,
            effort.SpecialAttack,
            Lv,
            personalityEffect.specialAttackMultiplier,
            extra.SpecialAttack
        );

        ability.PhysicalDefense = AbilityCalculator.CalculateNormal(
            racial.PhysicalDefense,
            effort.PhysicalDefense,
            Lv,
            personalityEffect.physicalDefenseMultiplier,
            extra.PhysicalDefense
        );

        ability.SpecialDefense = AbilityCalculator.CalculateNormal(
            racial.SpecialDefense,
            effort.SpecialDefense,
            Lv,
            personalityEffect.specialDefenseMultiplier,
            extra.SpecialDefense
        );

        ability.Speed = AbilityCalculator.CalculateNormal(
            racial.Speed,
            effort.Speed,
            Lv,
            personalityEffect.speedMultiplier,
            extra.Speed
        );

        // 计算体力值
        ability.HP = AbilityCalculator.CalculateHP(
            racial.HP,
            effort.HP,
            Lv,
            personalityEffect.hpMultiplier,
            extra.HP
        );

        Debug.Log($"物理攻击计算中间值：种族={racial.PhysicalAttack}, 努力={effort.PhysicalAttack}, 等级={Lv}, 基础值={racial.PhysicalAttack * 2 + effort.PhysicalAttack / 4 + 31}");

    }

    /// <summary>
    /// 刷新战斗能力
    /// </summary>
    public void RefreshFightAbility()
    {
        if (fightAbility == null)
        {
            fightAbility = new AbilitySixDimensions(0, 0, 0, 0, 0, 0);
        }

        // 计算能力值
        fightAbility.PhysicalAttack = ability.PhysicalAttack;
        fightAbility.SpecialAttack = ability.SpecialAttack;
        fightAbility.PhysicalDefense = ability.PhysicalDefense;
        fightAbility.SpecialDefense = ability.SpecialDefense;
        fightAbility.Speed = ability.Speed;
        // 计算体力值
        fightAbility.HP = ability.HP;
        fightAbilityCurHp = (int)fightAbility.HP;

    }



    /// <summary>
    /// 能力值计算
    /// </summary>
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

        //Debug.Log("种族值:");
        //Debug.Log($"物攻: {racial.PhysicalAttack}");
        //Debug.Log($"特攻: {racial.SpecialAttack}");
        //Debug.Log($"物防: {racial.PhysicalDefense}");
        //Debug.Log($"特防: {racial.SpecialDefense}");
        //Debug.Log($"速度: {racial.Speed}");
        //Debug.Log($"体力: {racial.HP}");

        //Debug.Log("努力值:");
        //Debug.Log($"物攻: {effort.PhysicalAttack}");
        //Debug.Log($"特攻: {effort.SpecialAttack}");
        //Debug.Log($"物防: {effort.PhysicalDefense}");
        //Debug.Log($"特防: {effort.SpecialDefense}");
        //Debug.Log($"速度: {effort.Speed}");
        //Debug.Log($"体力: {effort.HP}");

        Debug.Log("性格:" + $"{personality}");

        //Debug.Log("性格影响:");
        //var personalityEffects = PersonalityEffects.GetEffect(personality);
        //Debug.Log($"物攻修正: {personalityEffects.PhysicalAttack}");
        //Debug.Log($"特攻修正: {personalityEffects.SpecialAttack}");
        //Debug.Log($"物防修正: {personalityEffects.PhysicalDefense}");
        //Debug.Log($"特防修正: {personalityEffects.SpecialDefense}");
        //Debug.Log($"速度修正: {personalityEffects.Speed}");
        //Debug.Log($"体力修正: {personalityEffects.HP}");

        //Debug.Log("额外能力值:");
        //Debug.Log($"物攻: {extra.PhysicalAttack}");
        //Debug.Log($"特攻: {extra.SpecialAttack}");
        //Debug.Log($"物防: {extra.PhysicalDefense}");
        //Debug.Log($"特防: {extra.SpecialDefense}");
        //Debug.Log($"速度: {extra.Speed}");
        //Debug.Log($"体力: {extra.HP}");

        Debug.Log("能力值:");
        Debug.Log($"物攻: {ability.PhysicalAttack}");
        Debug.Log($"特攻: {ability.SpecialAttack}");
        Debug.Log($"物防: {ability.PhysicalDefense}");
        Debug.Log($"特防: {ability.SpecialDefense}");
        Debug.Log($"速度: {ability.Speed}");
        Debug.Log($"体力: {ability.HP}");
        Debug.Log("特性:" + $"{feature}");

        //Debug.Log("可用技能:");
        //foreach (var skill in availableSkills)
        //{
        //    Debug.Log($"{skill}");
        //}

        Debug.Log("当前技能:");
        foreach (var skill in currentSkills)
        {
            Debug.Log($"{skill.skillConfig.skillName}");
        }

        //Debug.Log($"魂印ID: {soulSealId}");

        Debug.Log("=== 精灵状态 ===");
    }



    /// <summary>
    /// 根据SkillInfoSO文件实例化一个SkillInfo
    /// </summary>
    public static SkillInfo CreateSkillInfoFromConfig(SkillConfigSO skillConfigSO)
    {
        if (skillConfigSO == null)
        {
            Debug.LogError("SkillConfigSO is null");
            return null;
        }

        // 从 SkillConfigSO 中获取技能属性
        int skillId = skillConfigSO.skillId;
        string name = skillConfigSO.skillName;
        string description = skillConfigSO.skillDescription;
        SkillType skillType = skillConfigSO.skillType;
        int attribute = skillConfigSO.attribute;
        int skillPower = skillConfigSO.skillPower;
        int maxPP = skillConfigSO.maxPP;
        bool isPredestinate = skillConfigSO.isPredestinate;
        int initialCritical = skillConfigSO.initialCritical;
        int skillSpeed = skillConfigSO.skillSpeed;
        List<SkillEffect> skillEffects = new List<SkillEffect>();

        // 从 SkillConfigSO 中获取效果配置，并转换为 SkillEffect 实例
        foreach (var effectSO in skillConfigSO.skillEffects)
        {
            var skillEffect = new SkillEffect(effectSO);
            skillEffects.Add(skillEffect);
        }

        // 创建 SkillInfo 实例
        SkillConfig newSkillConfig = new SkillConfig(
            skillId,
            name,
            description,
            skillType,
            attribute,
            skillPower,
            maxPP,
            isPredestinate,
            initialCritical,
            skillSpeed,
            skillEffects
        );
        SkillInfo newSkillInfo = new SkillInfo();
        newSkillInfo.skillConfig = newSkillConfig;

        // 返回生成的实例
        return newSkillInfo;
    }



    /// <summary>
    /// 根据BuffInfoSO文件实例化一个BuffInfo
    /// </summary>
    public static BuffInfo CreateBuffInfoFromConfig(BuffConfigSO buffConfigSO)
    {
        if (buffConfigSO == null)
        {
            Debug.LogError("BuffConfigSO is null");
            return null;
        }

        // 从 BuffConfigSO 中获取技能属性
        int buffId = buffConfigSO.buffId;
        string buffName = buffConfigSO.buffName;
        string buffDescribe = buffConfigSO.buffDescribe;
        BuffType buffType = buffConfigSO.buffType;
        string[] buffTags = buffConfigSO.buffTags;
        int effectiveTime = buffConfigSO.effectiveTime;
        bool isStackable = buffConfigSO.isStackable;
        int maxStack = buffConfigSO.maxStack;
        AddTimeChangeEnum addTimeChange = buffConfigSO.addTimeChange;
        TimeOverStackChangeEnum timeOverStackChange = buffConfigSO.timeOverStackChange;
        List<BuffEffect> buffEffects = new List<BuffEffect>();

        // 从 BuffConfigSO 中获取效果配置，并转换为 BuffEffect 实例
            BuffEffect buffEffect = new BuffEffect(buffConfigSO.buffEffects);
            //foreach (var buffEffect in buffEffects1)
            //{
            //    buffEffects.Add(buffEffect);
            //}


            //BuffEffect buffEffect = new BuffEffect(effectSO);
            buffEffects.Add(buffEffect);
        


        // 创建 BuffInfo 实例
        BuffConfig newBuffConfig = new BuffConfig(
            buffId,
            buffName,
            buffDescribe,
            buffType,
            buffTags,
            effectiveTime,
            isStackable,
            maxStack,
            addTimeChange,
            timeOverStackChange,
            buffEffects
            );

        BuffInfo newBuffInfo= new BuffInfo(newBuffConfig ,null ,null);

        // 返回生成的实例
        return newBuffInfo;
    }


}

public enum Attribute
{
    fire,
    water,
}





