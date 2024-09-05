using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// buff运行的数据类
/// </summary>
public class BuffInfo
{
    public BuffConfig BuffConfig;
    public GameObject creator;
    public GameObject target;
    public int curDuration;
    public int curStack;
    public int maxStack;

    public BuffInfo(BuffConfig buffConfig, GameObject target, GameObject creator = null)
    {
        buffConfig = buffConfig;
        Target = target;
        Creator = creator;
        CurDuration = buffConfig.Duration;
        CurStack = 1;
    }



    //buff的更新逻辑
    public void OnUpdate()
    {

    }


    public void HandleCreateEffect()
    {
        if (createBuffEffects == null) return;
        foreach (var buffEffect in createBuffEffects)
        {
            foreach (var effect in buffEffect.Effects)
            {
                effect.Apply(this);
            }
        }
    }

    public void HandleRemoveEffect()
    {
        if (removeBuffEffects == null) return;
        foreach (var buffEffect in removeBuffEffects)
        {
            foreach (var effect in buffEffect.Effects)
            {
                effect.Apply(this);
            }
        }
    }



}


/// <summary>
/// buff配置类
/// </summary>
public class BuffConfig
{
    public int BuffId;
    public int Priority;
    public bool IsPermanent;
    public float Duration;
    public bool IsStack;
    public int MaxStack;
    public AddTimeChangeEnum AddTimeChange;
    public TimeOverStackChangeEnum TimeOverStackChange;
    public BuffEffect[] BuffEffects;
}

/// <summary>
/// buff效果配置类
/// </summary>
public class BuffEffect
{
    //周期时间
    public float PeriodTime;
    //触发效果
    public AbstractEffect[] Effects;
}

public enum AddTimeChangeEnum
{
    [LabelText("刷新时间")]
    Refresh,
    [LabelText("叠加时间")]
    Add,
}

public enum TimeOverStackChangeEnum
{
    [LabelText("清除层数")]
    Clear,
    [LabelText("递减层数")]
    Reduce
}