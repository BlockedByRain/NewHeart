using UnityEngine;

/// <summary>
/// 能力等级变化值，用于记录战斗中精灵的能力等级变化。
/// </summary>
public class AbilityLvSixDimensions : SixDimensions
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
    private const int MinLvValue = -6;
    /// <summary>
    /// 能力等级上限值
    /// </summary>
    private const int MaxLvValue = 6;

    public override float PhysicalAttack
    {
        get => physicalAttack;
        set => physicalAttack = Mathf.Clamp(Mathf.RoundToInt(value), MinLvValue, MaxLvValue);
    }

    public override float SpecialAttack
    {
        get => specialAttack;
        set => specialAttack = Mathf.Clamp(Mathf.RoundToInt(value), MinLvValue, MaxLvValue);
    }

    public override float PhysicalDefense
    {
        get => physicalDefense;
        set => physicalDefense = Mathf.Clamp(Mathf.RoundToInt(value), MinLvValue, MaxLvValue);
    }

    public override float SpecialDefense
    {
        get => specialDefense;
        set => specialDefense = Mathf.Clamp(Mathf.RoundToInt(value), MinLvValue, MaxLvValue);
    }

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(Mathf.RoundToInt(value), MinLvValue, MaxLvValue);
    }

    public override float HP
    {
        get => hp;
        set => hp = Mathf.Clamp(Mathf.RoundToInt(value), MinLvValue, MaxLvValue);
    }
}
