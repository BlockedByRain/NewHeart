using System.Collections;
using System.Collections.Generic;
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
    public SixDimensionsValue racialValue;


    /// <summary>
    /// 能力值
    /// </summary>
    public SixDimensionsValue abilityValue;


    /// <summary>
    /// 努力值
    /// </summary>
    public SixDimensionsValue effortValue;


    /// <summary>
    /// 额外能力值
    /// </summary>
    public SixDimensionsValue extraValue;


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
    /// 战斗中当前能力
    /// </summary>
    public SixDimensionsValue currentValue;

    /// <summary>
    /// 战斗中能力等级
    /// </summary>
    public SixDimensionsValue abilityLv;











    /// <summary>
    /// 刷新能力
    /// </summary>
    public void RefreshCapability()
    {
        SixDimensionsValue personalityEffects = PersonalityEffects.GetEffect(this.personality);


        //Debug.Log(abilityValue.physicalAttack);
        //Debug.Log(racialValue.physicalAttack);
        //Debug.Log(effortValue.physicalAttack);
        //Debug.Log(personalityEffects.physicalAttack);
        //Debug.Log(extraValue.physicalAttack);


        //常规能力值=【【【(种族值*2+努力值÷4+个体)*(精灵等级÷100)+5】*性格修正】*套装百分比（暂无）】+外部加成
        abilityValue.physicalAttack = (int)(((((racialValue.physicalAttack * 2) + (effortValue.physicalAttack / 4) + 31) * (Lv / 100) + 5) * personalityEffects.physicalAttack) + extraValue.physicalAttack);
        abilityValue.specialAttack = (int)(((((racialValue.specialAttack * 2) + (effortValue.specialAttack / 4) + 31) * (Lv / 100) + 5) * personalityEffects.specialAttack) + extraValue.specialAttack);
        abilityValue.physicalDefense = (int)(((((racialValue.physicalDefense * 2) + (effortValue.physicalDefense / 4) + 31) * (Lv / 100) + 5) * personalityEffects.physicalDefense) + extraValue.physicalDefense);
        abilityValue.specialDefense = (int)(((((racialValue.specialDefense * 2) + (effortValue.specialDefense / 4) + 31) * (Lv / 100) + 5) * personalityEffects.specialDefense) + extraValue.specialDefense);
        abilityValue.speed = (int)(((((racialValue.speed * 2) + (effortValue.speed / 4) + 31) * (Lv / 100) + 5) * personalityEffects.speed) + extraValue.speed);

        //体力能力值=【【(种族值*2+努力值÷4+100+个体)*(精灵等级÷100)+10】*套装百分比加成（暂无）】+外部加成
        abilityValue.HP = (int)(((((racialValue.HP * 2) + (effortValue.HP / 4) + 100 + 31) * (Lv / 100) + 5) * personalityEffects.HP) + extraValue.HP);


    }


    public Pet(string name)
    {
        petName = name;
    }



}

/// <summary>
/// 六维值
/// </summary>
public class SixDimensionsValue
{
    /// <summary>
    /// 物攻
    /// </summary>
    public float physicalAttack=0;
    /// <summary>
    /// 特攻
    /// </summary>
    public float specialAttack = 0;
    /// <summary>
    /// 物防
    /// </summary>
    public float physicalDefense = 0;
    /// <summary>
    /// 特防
    /// </summary>
    public float specialDefense = 0;
    /// <summary>
    /// 速度
    /// </summary>
    public float speed = 0;
    /// <summary>
    /// 体力
    /// </summary>
    public float HP = 0;


    public SixDimensionsValue(float physicalAttack, float specialAttack, float physicalDefense, float specialDefense, float speed, float HP)
    {
        this.physicalAttack = physicalAttack;
        this.specialAttack = specialAttack;
        this.physicalDefense = physicalDefense;
        this.specialDefense = specialDefense;
        this.speed = speed;
        this.HP = HP;
    }
}


