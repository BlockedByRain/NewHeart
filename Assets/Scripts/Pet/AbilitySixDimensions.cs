using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 六维能力值类，用于表示精灵的能力值。
/// </summary>
public class AbilitySixDimensions : SixDimensions
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="physicalAttack">物攻值</param>
    /// <param name="specialAttack">特攻值</param>
    /// <param name="physicalDefense">物防值</param>
    /// <param name="specialDefense">特防值</param>
    /// <param name="speed">速度值</param>
    /// <param name="hp">体力值</param>
    public AbilitySixDimensions(int physicalAttack = 1, int specialAttack = 1, int physicalDefense = 1, int specialDefense = 1, int speed = 1, int hp = 1)
    {
        this.physicalAttack = Mathf.Max(1, physicalAttack);
        this.specialAttack = Mathf.Max(1, specialAttack);
        this.physicalDefense = Mathf.Max(1, physicalDefense);
        this.specialDefense = Mathf.Max(1, specialDefense);
        this.speed = Mathf.Max(1, speed);
        this.hp = Mathf.Max(1, hp);
    }

    public override float PhysicalAttack
    {
        get => physicalAttack;
        set => physicalAttack = Mathf.Max(1, Mathf.RoundToInt(value));
    }

    public override float SpecialAttack
    {
        get => specialAttack;
        set => specialAttack = Mathf.Max(1, Mathf.RoundToInt(value));
    }

    public override float PhysicalDefense
    {
        get => physicalDefense;
        set => physicalDefense = Mathf.Max(1, Mathf.RoundToInt(value));
    }

    public override float SpecialDefense
    {
        get => specialDefense;
        set => specialDefense = Mathf.Max(1, Mathf.RoundToInt(value));
    }

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Max(1, Mathf.RoundToInt(value));
    }

    public override float HP
    {
        get => hp;
        set => hp = Mathf.Max(1, Mathf.RoundToInt(value));
    }
}



