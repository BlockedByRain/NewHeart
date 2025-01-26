using UnityEngine;

/// <summary>
/// 六维性格影响类，用于表示精灵性格对其属性的影响，属性值为 0.9f、1f 或 1.1f。
/// </summary>
public class PersonalityEffectsSixDimensions : SixDimensions<PersonalityEffectsSixDimensions>
{
    private int personlityId;

    private string personalityName;


    // 性格对每个属性的影响（减少、不变、增加）
    private float physicalAttackMultiplier;
    private float specialAttackMultiplier;
    private float physicalDefenseMultiplier;
    private float specialDefenseMultiplier;
    private float speedMultiplier;
    private float hpMultiplier;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PersonalityEffectsSixDimensions(float pa = 1f, float sa = 1f, float pd = 1f, float sd = 1f, float sp = 1f, float hp = 1f)
    {
        PhysicalAttack = pa;
        SpecialAttack = sa;
        PhysicalDefense = pd;
        SpecialDefense = sd;
        Speed = sp;
        HP = hp;
    }

    /// <summary>
    /// 性格影响无数值范围限制（占位实现）
    /// </summary>
    protected override int GetMinValue() => 0;
    protected override int GetMaxValue() => 0;

    public override float PhysicalAttack
    {
        get => physicalAttackMultiplier;
        set => physicalAttackMultiplier = value;
    }

    public override float SpecialAttack
    {
        get => specialAttackMultiplier;
        set => specialAttackMultiplier = value;
    }

    public override float PhysicalDefense
    {
        get => physicalDefenseMultiplier;
        set => physicalDefenseMultiplier = value;
    }

    public override float SpecialDefense
    {
        get => specialDefenseMultiplier;
        set => specialDefenseMultiplier = value;
    }

    public override float Speed
    {
        get => speedMultiplier;
        set => speedMultiplier = value;
    }

    public override float HP
    {
        get => hpMultiplier;
        set => hpMultiplier = value;
    }

}
