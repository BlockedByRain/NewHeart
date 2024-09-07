using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    //伤害数值
    public int damageValue;
    //伤害类型
    public DamageType damageType;
    //伤害属性
    public Attribute attribute;


    /// <summary>
    /// 计算攻击伤害
    /// </summary>
    public Damage GetAttackDamage(Pet creator, Pet target, SkillConfig skillConfig)
    {
        //伤害值=【【等级系数*技能威力*（进攻方对战实际进攻值÷防守方对战实际防守值）+2】*浮动范围*属性加成*克制倍数*己方增伤百分比*（1-对方减伤百分比）】
        //等级系数 =（0.4 * 精灵等级 + 2）÷50
        //浮动范围∈[217÷255，1]

        //等级系数
        float LvMultiplier = (float)(creator.Lv * 0.4 + 2) / 50;
        //技能威力
        float skillPower = skillConfig.skillPower;
        //实际能力
        float curAttack;
        float curDefense;
        if (skillConfig.skillType == SkillType.Physical)
        {
            curAttack = creator.fightAbility.PhysicalAttack;
            curDefense = target.fightAbility.PhysicalDefense;
        }
        else
        {
            curAttack = creator.fightAbility.SpecialAttack;
            curDefense = target.fightAbility.SpecialDefense;
        }
        //属性加成 todo 双属性也吃单独本系
        this.attribute = skillConfig.attribute;

        float attributeMultiplier;
        if (creator.attribute == skillConfig.attribute)
        {
            attributeMultiplier = 1.5f;
        }
        else
        {
            attributeMultiplier = 1f;
        }

        //浮动系数 [217÷255，1]

        int a = Random.Range(217, 256);

        float FloatingMultiplier = (float)a / 255;


        //todo克制倍数
        float restraintMultiplier = 1f;


        //todo增减伤


        //Debug.Log(LvMultiplier);
        //Debug.Log(skillPower);
        //Debug.Log(curAttack);
        //Debug.Log(curDefense);
        //Debug.Log(FloatingMultiplier);
        //Debug.Log(attributeMultiplier);
        //Debug.Log(restraintMultiplier);


        //伤害值=【【等级系数*技能威力*（进攻方对战实际进攻值÷防守方对战实际防守值）+2】*浮动范围*属性加成*克制倍数*己方增伤百分比*（1-对方减伤百分比）】
        damageValue = (int)((LvMultiplier * skillPower * (curAttack / curDefense) + 2) * FloatingMultiplier * attributeMultiplier * restraintMultiplier);


        LogDamageInfo();




        return this;

    }

    
    public void LogDamageInfo()
    {
        Debug.Log("此次伤害为"+ attribute+"属性的"+damageType+"类型伤害，"+"伤害值为："+ damageValue);

    }

}


public enum DamageType
{
    //攻击伤害，一般是攻击造成的伤害
    Attack,
    //百分比伤害，一般是带有百分比描述的效果产生的伤害
    Percentage,
    //固定伤害，一般是带有固定伤害数字的效果产生的伤害
    Fixed
}