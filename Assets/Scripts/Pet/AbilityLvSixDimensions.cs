using UnityEngine;

/// <summary>
/// 能力等级变化值，用于记录战斗中精灵的能力等级变化。
/// </summary>
public class AbilityLvSixDimensions : SixDimensions<AbilityLvSixDimensions>
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    /// <summary>
    /// 能力等级下限值
    /// </summary>
    protected override int GetMinValue() => -6;
    /// <summary>
    /// 能力等级上限值
    /// </summary>
    protected override int GetMaxValue() => 6;


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="physicalAttack">物攻值</param>
    /// <param name="specialAttack">特攻值</param>
    /// <param name="physicalDefense">物防值</param>
    /// <param name="specialDefense">特防值</param>
    /// <param name="speed">速度值</param>
    /// <param name="hp">体力值</param>

    public override float PhysicalAttack
    {
        get => physicalAttack;
        set => physicalAttack = ClampAndRound(value);
    }

    public override float SpecialAttack
    {
        get => specialAttack;
        set => specialAttack = ClampAndRound(value);
    }

    public override float PhysicalDefense
    {
        get => physicalDefense;
        set => physicalDefense = ClampAndRound(value);
    }

    public override float SpecialDefense
    {
        get => specialDefense;
        set => specialDefense = ClampAndRound(value);
    }

    public override float Speed
    {
        get => speed;
        set => speed = ClampAndRound(value);
    }

    public override float HP
    {
        get => hp;
        set => hp = ClampAndRound(value);
    }

}
