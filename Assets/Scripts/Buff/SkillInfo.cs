using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 运行时的技能数据类
/// </summary>
public class SkillInfo
{
    public SkillConfig skillConfig;
    public GameObject user;
    public GameObject target;
   

    //todo

    public void Execute(Pet user, Pet target)
    {
        if (skillConfig.skillEffects == null || skillConfig.skillEffects.Count == 0)
        {
            Debug.Log("此技能特殊效果");
        }
        else
        {
            // 逐一执行技能效果
            foreach (var skillEffect in skillConfig.skillEffects)
            {
                try
                {
                    skillEffect.Execute(user, target);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(skillEffect.effect.effectDescription+ $"技能效果执行失败: {ex.Message}");
                }
            }
        }



        //todo 伤害计算
        Damage damage = new Damage();
        damage = damage.GetAttackDamage(user, target, skillConfig);

        //前提需要 属性克制、抗性、增减伤系统
    }


}

/// <summary>
/// 技能配置类
/// </summary>
public class SkillConfig
{
    //技能id
    public int skillId;
    //技能名
    public string skillName;
    //技能描述
    public string skillDescription;
    //技能类型
    public SkillType skillType;
    //技能属性
    public Attribute attribute;
    //技能威力
    public int skillPower;
    //最大次数
    public int maxPP;
    //是否必中
    public bool isPredestinate;
    //初始暴击，按十六分之多少来表示，即1代表1/16=6.25%概率暴击
    public int initialCritical;
    //技能先制
    public int skillSpeed;
    //技能效果
    public List<SkillEffect> skillEffects;

    public SkillConfig(int skillId, string name, string description, SkillType skillType, Attribute attribute, int skillPower, int maxPP, bool isPredestinate, int initialCritical, int skillSpeed, List<SkillEffect> skillEffects)
    {
        this.skillId = skillId;
        this.skillName = name;
        this.skillDescription = description;
        this.skillType = skillType;
        this.attribute = attribute;
        this.skillPower = skillPower;
        this.maxPP = maxPP;
        this.isPredestinate = isPredestinate;
        this.initialCritical = initialCritical;
        this.skillSpeed = skillSpeed;
        this.skillEffects = skillEffects;
    }

}


[System.Serializable]
public class SkillEffect
{
    public AbstractEffect effect;

    public SkillEffect(AbstractEffect effect)
    {
        this.effect = effect;
    }

    // 执行效果，针对每个效果依次执行
    public void Execute(object user, object target)
    {

        effect.Apply(user, target);

    }
}

public enum SkillType
{
    //物理攻击
    Physical,
    //特殊攻击
    Special,
    //属性技能
    Attribute,
}