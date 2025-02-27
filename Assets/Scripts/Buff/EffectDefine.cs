﻿using UnityEngine;

/// <summary>
/// 效果定义部分，技能或buff触发效果后的逻辑
/// </summary>
public interface IEffect
{
    public void Apply(object user, object target);
}

public abstract class AbstractEffect : ScriptableObject,IEffect
{
    public int effectId;

    public string effectDescription;

    public abstract void Apply(object user, object target);
}

/// <summary>
/// 示例，打印指定字符串
/// </summary>
public class PrintEffect : AbstractEffect
{
    public string Message;
    public override void Apply(object user, object target)
    {
        Debug.Log(Message);
    }
}