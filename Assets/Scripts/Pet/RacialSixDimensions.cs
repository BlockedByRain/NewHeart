using UnityEngine;

/// <summary>
/// 六维种族值类，用于表示精灵自身种族的基础属性值。
/// </summary>
public class RacialSixDimensions : SixDimensions<RacialSixDimensions>
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    // 最大种族值常量
    private const int MaxRacialValue = 200;

    protected override int GetMinValue() => 1;
    protected override int GetMaxValue() => MaxRacialValue;

    /// <summary>
    /// 构造函数，强制数值在1-200之间
    /// </summary>
    public RacialSixDimensions(int pa = 1, int sa = 1, int pd = 1, int sd = 1, int sp = 1, int hp = 1)
    {
        PhysicalAttack = pa;
        SpecialAttack = sa;
        PhysicalDefense = pd;
        SpecialDefense = sd;
        Speed = sp;
        HP = hp;
    }


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
