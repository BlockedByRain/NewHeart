using UnityEngine;

/// <summary>
/// 六维努力值类，用于表示精灵的努力值。
/// </summary>
public class EffortSixDimensions : SixDimensions<EffortSixDimensions>
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    // 总和努力值最大值常量
    private const int MaxTotalEffort = 510;
    /// <summary>
    /// 努力值下限
    /// </summary>
    protected override int GetMinValue() => 0;
    /// <summary>
    /// 努力值下限上限
    /// </summary>
    protected override int GetMaxValue() => 255;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pa">物攻值</param>
    /// <param name="sa">特攻值</param>
    /// <param name="pd">物防值</param>
    /// <param name="sd">特防值</param>
    /// <param name="sp">速度值</param>
    /// <param name="hp">体力值</param>
    /// <summary>
    /// 构造函数，直接赋值而非调用AddEffort
    /// </summary>
    public EffortSixDimensions(int pa = 0, int sa = 0, int pd = 0, int sd = 0, int sp = 0, int hp = 0)
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

    /// <summary>
    /// 增加努力值到指定属性，确保总和不超过 510。
    /// </summary>
    public bool AddEffort(int pa = 0, int sa = 0, int pd = 0, int sd = 0, int sp = 0, int hp = 0)
    {
        int currentTotal = this.physicalAttack + this.specialAttack + this.physicalDefense +
                          this.specialDefense + this.speed + this.hp;
        int newTotal = currentTotal + pa + sa + pd + sd + sp + hp;

        if (newTotal > MaxTotalEffort) return false;

        int newPA = this.physicalAttack + pa;
        int newSA = this.specialAttack + sa;
        int newPD = this.physicalDefense + pd;
        int newSD = this.specialDefense + sd;
        int newSP = this.speed + sp;
        int newHP = this.hp + hp;

        if (newPA > GetMaxValue() || newSA > GetMaxValue() ||
            newPD > GetMaxValue() || newSD > GetMaxValue() ||
            newSP > GetMaxValue() || newHP > GetMaxValue())
        {
            return false;
        }

        this.physicalAttack = newPA;
        this.specialAttack = newSA;
        this.physicalDefense = newPD;
        this.specialDefense = newSD;
        this.speed = newSP;
        this.hp = newHP;
        return true;
    }
}
