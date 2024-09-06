using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// 运行时的buff数据类
/// </summary>
public class BuffInfo
{
    public BuffConfig buffConfig;
    public GameObject creator;
    public GameObject target;
    public int effectiveTime;
    public int curStack;
    public int maxStack;

    //战斗开始时buff效果集合
    List<BuffEffect> beginningOfTheBattleBuffEffects;
    //回合开始时buff效果集合
    List<BuffEffect> beginningOfTheRoundBuffEffects;
    //使用技能时buff效果集合
    List<BuffEffect> usingSkillBuffEffects;
    //回合结束时buff效果集合
    List<BuffEffect> endOfTheRound;


    public BuffInfo(BuffConfig buffConfig, GameObject target, GameObject creator = null)
    {
        this.buffConfig = buffConfig;
        this.target = target;
        this.creator = creator;

        //遍历buff配置，将buff效果分类
        foreach (var buffConfigBuffEffect in buffConfig.BuffEffects)
        {
            switch (buffConfigBuffEffect.triggerTiming)
            {
                case TriggerTimingEnum.BeginningOfTheBattle:
                    if (beginningOfTheBattleBuffEffects == null) beginningOfTheBattleBuffEffects = new List<BuffEffect>();
                    beginningOfTheBattleBuffEffects.Add(buffConfigBuffEffect);
                    break;
                case TriggerTimingEnum.BeginningOfTheRound:
                    if (beginningOfTheRoundBuffEffects == null) beginningOfTheRoundBuffEffects = new List<BuffEffect>();
                    beginningOfTheRoundBuffEffects.Add(buffConfigBuffEffect);
                    break;
                case TriggerTimingEnum.UsingSkill:
                    if (usingSkillBuffEffects == null) usingSkillBuffEffects = new List<BuffEffect>();
                    usingSkillBuffEffects.Add(buffConfigBuffEffect);
                    break;
                case TriggerTimingEnum.EndOfTheRound:
                    if (endOfTheRound == null) endOfTheRound = new List<BuffEffect>();
                    endOfTheRound.Add(buffConfigBuffEffect);
                    break;
            }
            
        }

        this.effectiveTime = buffConfig.effectiveTime;
        curStack = 1;
    }



    //buff的更新逻辑
    public void OnUpdate()
    {

    }

    //战斗开始时调用
    public void HandleBeginningOfTheBattleEffect(Pet owner)
    {
        if (beginningOfTheBattleBuffEffects == null) return;
        foreach (var buffEffect in beginningOfTheBattleBuffEffects)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(owner, null);
            }
        }
    }

    //回合开始时调用
    public void HandleBeginningOfTheRoundEffect()
    {
        if (beginningOfTheRoundBuffEffects == null) return;
        foreach (var buffEffect in beginningOfTheRoundBuffEffects)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(this, null);
            }
        }
    }

    //使用技能时调用
    public void HandleUsingSkilleEffect()
    {
        if (usingSkillBuffEffects == null) return;
        foreach (var buffEffect in usingSkillBuffEffects)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(this, null);
            }
        }
    }

    //回合结束时调用
    public void HandleEndOfTheRoundEffect()
    {
        if (endOfTheRound == null) return;
        foreach (var buffEffect in endOfTheRound)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(this, null);
            }
        }
    }



}


/// <summary>
/// buff配置类
/// </summary>
public class BuffConfig
{
    //buffid
    public int buffId;
    //buff名
    public string buffName;
    //buff描述
    public string buffDescribe;
    //buff类型
    public BuffType buffType;
    //buffTag
    public string[] buffTags;
    //生效时间
    public int effectiveTime;
    //是否可叠加
    public bool IsStack;
    //最大叠加层数
    public int MaxStack;
    //添加时刷新类型
    public AddTimeChangeEnum AddTimeChange;
    //结束时层数刷新类型
    public TimeOverStackChangeEnum TimeOverStackChange;
    //效果列表
    public List<BuffEffect> BuffEffects;


    public BuffConfig(int buffId, string buffName, string buffDescribe, BuffType buffType, string[] buffTags, int effectiveTime, bool IsStack,int maxStack, AddTimeChangeEnum addTimeChange, TimeOverStackChangeEnum timeOverStackChange, List<BuffEffect> buffEffects)
    {
        this.buffId = buffId;
        this.buffName = buffName;
        this.buffDescribe = buffDescribe;
        this.buffType = buffType;
        this.buffTags = buffTags;
        this.effectiveTime = effectiveTime;
        this.IsStack = IsStack;
        MaxStack = maxStack;
        AddTimeChange = addTimeChange;
        TimeOverStackChange = timeOverStackChange;
        BuffEffects = buffEffects;

    }
}

/// <summary>
/// buff效果配置类
/// </summary>
[System.Serializable]
public class BuffEffect
{
    //类型
    public BuffType buffType;
    //生效时间
    //public int effectiveTime;
    //生效节点
    public TriggerTimingEnum triggerTiming;
    //触发效果
    public AbstractEffect[] effects;


    public BuffEffect(AbstractEffect[] effects)
    {
        this.effects = effects;
    }
}

/// <summary>
/// buff类型
/// </summary>
public enum BuffType
{
    //永久类
    Permanent,
    //回合类
    Round,
    //次数类
    Frequency,
}

/// <summary>
/// 添加时刷新类型
/// </summary>
public enum AddTimeChangeEnum
{
    [LabelText("刷新时间")]
    Refresh,
    [LabelText("叠加时间")]
    Add,
}

/// <summary>
/// 结束时层数刷新类型
/// </summary>
public enum TimeOverStackChangeEnum
{
    [LabelText("清除层数")]
    Clear,
    [LabelText("递减层数")]
    Reduce
}


/// <summary>
/// 触发时机
/// </summary>
public enum TriggerTimingEnum
{
    [LabelText("战斗开始时")]
    BeginningOfTheBattle,
    [LabelText("回合开始时")]
    BeginningOfTheRound,
    [LabelText("使用技能时")]
    UsingSkill,
    [LabelText("回合结束时")]
    EndOfTheRound,

}