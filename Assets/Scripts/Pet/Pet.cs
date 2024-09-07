using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// ����
/// </summary>
public class Pet
{
    /// <summary>
    /// ����id
    /// </summary>
    public int petId = 1;

    /// <summary>
    /// ������
    /// </summary>
    public string petName;


    /// <summary>
    /// ����ȼ�
    /// </summary>
    public int Lv;

    /// <summary>
    /// �¼����辭��
    /// </summary>
    public int nextLvExp;

    /// <summary>
    /// ��������
    /// </summary>
    public Attribute attribute;

    /// <summary>
    /// ����ֵ
    /// </summary>
    public RacialSixDimensions racial;


    /// <summary>
    /// ����ֵ
    /// </summary>
    public AbilitySixDimensions ability;


    /// <summary>
    /// Ŭ��ֵ
    /// </summary>
    public EffortSixDimensions effort;


    /// <summary>
    /// ��������ֵ
    /// </summary>
    public AbilitySixDimensions extra;


    /// <summary>
    /// �Ը�
    /// </summary>
    public Personality personality;

    /// <summary>
    /// ����
    /// </summary>
    public Feature feature;

    /// <summary>
    /// ���ü�����
    /// </summary>
    public List<SkillInfo> availableSkills = null;

    /// <summary>
    /// ��ǰ������
    /// </summary>
    public List<SkillInfo> currentSkills = null;

    /// <summary>
    /// ��ӡid
    /// </summary>
    public string soulSealId = null;

    /// <summary>
    /// �����ȼ�
    /// </summary>
    public AbilityLvSixDimensions abilityLvSixDimensionsValue = null;

    /// <summary>
    /// ս��������ֵ,����Hp��Ϊս���������������
    /// </summary>
    public AbilitySixDimensions fightAbility = null;

    /// <summary>
    /// ս���е�ǰ����
    /// </summary>
    public int fightAbilityCurHp;

    /// <summary>
    /// ����Buff
    /// </summary>
    public List<BuffInfo> buffInfos = null;

    /// <summary>
    /// ����ѡ��ʹ�õļ���
    /// </summary>
    public SkillInfo GetSelectedSkill(int skillIndex)
    {
        return currentSkills[skillIndex];
    }



    /// <summary>
    /// ˢ������
    /// </summary>
    public void RefreshCapability()
    {
        PersonalityEffectsSixDimensions personalityEffects = PersonalityEffects.GetEffect(this.personality);
        // ��������ֵ
        ability.PhysicalAttack = CalculateState(racial.PhysicalAttack, effort.PhysicalAttack, 31, Lv, personalityEffects.PhysicalAttack, extra.PhysicalAttack);
        ability.SpecialAttack = CalculateState(racial.SpecialAttack, effort.SpecialAttack, 31, Lv, personalityEffects.SpecialAttack, extra.SpecialAttack);
        ability.PhysicalDefense = CalculateState(racial.PhysicalDefense, effort.PhysicalDefense, 31, Lv, personalityEffects.PhysicalDefense, extra.PhysicalDefense);
        ability.SpecialDefense = CalculateState(racial.SpecialDefense, effort.SpecialDefense, 31, Lv, personalityEffects.SpecialDefense, extra.SpecialDefense);
        ability.Speed = CalculateState(racial.Speed, effort.Speed, 31, Lv, personalityEffects.Speed, extra.Speed);

        // ��������ֵ
        ability.HP = CalculateState(racial.HP, effort.HP, 100 + 31, Lv, personalityEffects.HP, extra.HP);

    }

    /// <summary>
    /// ˢ��ս������
    /// </summary>
    public void RefreshFightAbility()
    {
        if (fightAbility == null)
        {
            fightAbility = new AbilitySixDimensions(0, 0, 0, 0, 0, 0);
        }

        // ��������ֵ
        fightAbility.PhysicalAttack = ability.PhysicalAttack;
        fightAbility.SpecialAttack = ability.SpecialAttack;
        fightAbility.PhysicalDefense = ability.PhysicalDefense;
        fightAbility.SpecialDefense = ability.SpecialDefense;
        fightAbility.Speed = ability.Speed;
        // ��������ֵ
        fightAbility.HP = ability.HP;
        fightAbilityCurHp = (int)fightAbility.HP;

    }



    /// <summary>
    /// ����ֵ����
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
        //��������ֵ=������(����ֵ*2+Ŭ��ֵ��4+31)*(����ȼ���100)+5��*�Ը�������*��װ�ٷֱȣ����ޣ���+�ⲿ�ӳ�
        //��������ֵ=����(����ֵ*2+Ŭ��ֵ��4+100+31)*(����ȼ���100)+10��*��װ�ٷֱȼӳɣ����ޣ���+�ⲿ�ӳ�
        return (int)(((((racialValue * 2) + (effortValue / 4) + additionalValue) * (level / 100) + 5) * personalityEffect) + extraValue);
    }

    /// <summary>
    /// ��ӡ��ǰ���������״̬
    /// </summary>
    public void PrintStatus()
    {
        Debug.Log("=== ����״̬ ===");
        Debug.Log($"ID: {petId}");
        Debug.Log($"����: {petName}");
        Debug.Log($"�ȼ�: {Lv}");
        Debug.Log($"�¼����辭��: {nextLvExp}");

        //Debug.Log("����ֵ:");
        //Debug.Log($"�﹥: {racial.PhysicalAttack}");
        //Debug.Log($"�ع�: {racial.SpecialAttack}");
        //Debug.Log($"���: {racial.PhysicalDefense}");
        //Debug.Log($"�ط�: {racial.SpecialDefense}");
        //Debug.Log($"�ٶ�: {racial.Speed}");
        //Debug.Log($"����: {racial.HP}");

        //Debug.Log("Ŭ��ֵ:");
        //Debug.Log($"�﹥: {effort.PhysicalAttack}");
        //Debug.Log($"�ع�: {effort.SpecialAttack}");
        //Debug.Log($"���: {effort.PhysicalDefense}");
        //Debug.Log($"�ط�: {effort.SpecialDefense}");
        //Debug.Log($"�ٶ�: {effort.Speed}");
        //Debug.Log($"����: {effort.HP}");

        Debug.Log("�Ը�:" + $"{personality}");

        //Debug.Log("�Ը�Ӱ��:");
        //var personalityEffects = PersonalityEffects.GetEffect(personality);
        //Debug.Log($"�﹥����: {personalityEffects.PhysicalAttack}");
        //Debug.Log($"�ع�����: {personalityEffects.SpecialAttack}");
        //Debug.Log($"�������: {personalityEffects.PhysicalDefense}");
        //Debug.Log($"�ط�����: {personalityEffects.SpecialDefense}");
        //Debug.Log($"�ٶ�����: {personalityEffects.Speed}");
        //Debug.Log($"��������: {personalityEffects.HP}");

        //Debug.Log("��������ֵ:");
        //Debug.Log($"�﹥: {extra.PhysicalAttack}");
        //Debug.Log($"�ع�: {extra.SpecialAttack}");
        //Debug.Log($"���: {extra.PhysicalDefense}");
        //Debug.Log($"�ط�: {extra.SpecialDefense}");
        //Debug.Log($"�ٶ�: {extra.Speed}");
        //Debug.Log($"����: {extra.HP}");

        Debug.Log("����ֵ:");
        Debug.Log($"�﹥: {ability.PhysicalAttack}");
        Debug.Log($"�ع�: {ability.SpecialAttack}");
        Debug.Log($"���: {ability.PhysicalDefense}");
        Debug.Log($"�ط�: {ability.SpecialDefense}");
        Debug.Log($"�ٶ�: {ability.Speed}");
        Debug.Log($"����: {ability.HP}");
        Debug.Log("����:" + $"{feature}");

        //Debug.Log("���ü���:");
        //foreach (var skill in availableSkills)
        //{
        //    Debug.Log($"{skill}");
        //}

        Debug.Log("��ǰ����:");
        foreach (var skill in currentSkills)
        {
            Debug.Log($"{skill.skillConfig.skillName}");
        }

        //Debug.Log($"��ӡID: {soulSealId}");

        Debug.Log("=== ����״̬ ===");
    }



    /// <summary>
    /// ����SkillInfoSO�ļ�ʵ����һ��SkillInfo
    /// </summary>
    public static SkillInfo CreateSkillInfoFromConfig(SkillConfigSO skillConfigSO)
    {
        if (skillConfigSO == null)
        {
            Debug.LogError("SkillConfigSO is null");
            return null;
        }

        // �� SkillConfigSO �л�ȡ��������
        int skillId = skillConfigSO.skillId;
        string name = skillConfigSO.skillName;
        string description = skillConfigSO.skillDescription;
        SkillType skillType = skillConfigSO.skillType;
        Attribute attribute = skillConfigSO.attribute;
        int skillPower = skillConfigSO.skillPower;
        int maxPP = skillConfigSO.maxPP;
        bool isPredestinate = skillConfigSO.isPredestinate;
        int initialCritical = skillConfigSO.initialCritical;
        int skillSpeed = skillConfigSO.skillSpeed;
        List<SkillEffect> skillEffects = new List<SkillEffect>();

        // �� SkillConfigSO �л�ȡЧ�����ã���ת��Ϊ SkillEffect ʵ��
        foreach (var effectSO in skillConfigSO.skillEffects)
        {
            var skillEffect = new SkillEffect(effectSO);
            skillEffects.Add(skillEffect);
        }

        // ���� SkillInfo ʵ��
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

        // �������ɵ�ʵ��
        return newSkillInfo;
    }



    /// <summary>
    /// ����BuffInfoSO�ļ�ʵ����һ��BuffInfo
    /// </summary>
    public static BuffInfo CreateBuffInfoFromConfig(BuffConfigSO buffConfigSO)
    {
        if (buffConfigSO == null)
        {
            Debug.LogError("BuffConfigSO is null");
            return null;
        }

        // �� BuffConfigSO �л�ȡ��������
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

        // �� BuffConfigSO �л�ȡЧ�����ã���ת��Ϊ BuffEffect ʵ��
            BuffEffect buffEffect = new BuffEffect(buffConfigSO.buffEffects);
            //foreach (var buffEffect in buffEffects1)
            //{
            //    buffEffects.Add(buffEffect);
            //}


            //BuffEffect buffEffect = new BuffEffect(effectSO);
            buffEffects.Add(buffEffect);
        


        // ���� BuffInfo ʵ��
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

        // �������ɵ�ʵ��
        return newBuffInfo;
    }


}

public enum Attribute
{
    fire,
    water,
}





