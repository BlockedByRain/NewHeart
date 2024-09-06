using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    //技能id
    public int id;
    //技能名
    public string name;
    //技能描述
    public string description;
    //技能类型
    public SkillType skillType;
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



    public SkillInfo(int skillId, string name, string description, SkillType skillType, int skillPower, int maxPP, bool isPredestinate, int initialCritical, int skillSpeed, List<SkillEffect> skillEffects)
    {
        this.id = skillId;
        this.name = name;
        this.description = description;
        this.skillType = skillType;
        this.skillPower = skillPower;
        this.maxPP = maxPP;
        this.isPredestinate = isPredestinate;
        this.initialCritical = initialCritical;
        this.skillSpeed = skillSpeed;
        this.skillEffects = skillEffects;
    }


    public void Execute(Pet user, Pet target)
    {
        if (skillEffects == null || skillEffects.Count == 0)
        {
            Debug.Log("此技能无效果");
            return;
        }
        else
        {
            // 逐一执行技能效果
            foreach (var skillEffect in skillEffects)
            {
                try
                {
                    skillEffect.Execute(user, target);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"技能效果执行失败: {ex.Message}");
                }
            }
        }
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