using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 六维能力值类，用于表示精灵的能力值。
/// </summary>
public class AbilitySixDimensions : SixDimensions<AbilitySixDimensions>
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    /// <summary>
    /// 构造函数，初始化六维能力值
    /// </summary>
    public AbilitySixDimensions(
        int physicalAttack = 1,
        int specialAttack = 1,
        int physicalDefense = 1,
        int specialDefense = 1,
        int speed = 1,
        int hp = 1
    )
    {
        // 通过属性赋值，自动应用数值约束（最小值、取整等）
        PhysicalAttack = physicalAttack;
        SpecialAttack = specialAttack;
        PhysicalDefense = physicalDefense;
        SpecialDefense = specialDefense;
        Speed = speed;
        HP = hp;
    }



    /// <summary>
    /// 能力值下限（固定为1）
    /// </summary>
    protected override int GetMinValue() => 1;

    /// <summary>
    /// 能力值无上限
    /// </summary>
    protected override int GetMaxValue() => int.MaxValue;
    public override float PhysicalAttack
    {
        get => physicalAttack;
        set => physicalAttack = Mathf.Max(1, ClampAndRound(value));
    }

    public override float SpecialAttack
    {
        get => specialAttack;
        set => specialAttack = Mathf.Max(1, ClampAndRound(value));
    }

    public override float PhysicalDefense
    {
        get => physicalDefense;
        set => physicalDefense = Mathf.Max(1, ClampAndRound(value));
    }

    public override float SpecialDefense
    {
        get => specialDefense;
        set => specialDefense = Mathf.Max(1, ClampAndRound(value));
    }

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Max(1, ClampAndRound(value));
    }

    public override float HP
    {
        get => hp;
        set => hp = Mathf.Max(1, ClampAndRound(value));
    }

    public static implicit operator AbilitySixDimensions((int, int, int, int, int, int) v)
    {
        throw new NotImplementedException();
    }
}



