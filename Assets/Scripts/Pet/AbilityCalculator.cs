using UnityEngine;

public static class AbilityCalculator
{
    // 常规能力值公式常数
    private const int BaseValueNormal = 31;
    // 体力值公式常数
    private const int BaseValueHP = 100 + BaseValueNormal;

    /// <summary>
    /// 计算常规能力值（物攻、特攻、物防、特防、速度）
    /// 公式：[((种族值×2 + 努力值÷4 + 31) × 等级÷100 + 5) × 性格修正] + 额外加成
    /// </summary>
    public static int CalculateNormal(
        float racialValue,
        float effortValue,
        int level,
        float personalityMultiplier,
        float extraValue
    )
    {
        float baseValue = (racialValue * 2) + (effortValue / 4) + BaseValueNormal;
        float levelFactor = level / 100f;
        return Mathf.FloorToInt(((baseValue * levelFactor + 5) * personalityMultiplier) + extraValue);
    }

    /// <summary>
    /// 计算体力值（HP）
    /// 公式：[((种族值×2 + 努力值÷4 + 100 + 31) × 等级÷100 + 10) × 性格修正] + 额外加成
    /// </summary>
    public static int CalculateHP(
        float racialValue,
        float effortValue,
        int level,
        float personalityMultiplier,
        float extraValue
    )
    {
        float baseValue = (racialValue * 2) + (effortValue / 4) + BaseValueHP;
        float levelFactor = level / 100f;
        return Mathf.FloorToInt(((baseValue * levelFactor + 10) * personalityMultiplier) + extraValue);
    }

    /// <summary>
    /// 计算所有能力值
    /// </summary>
    public static AbilitySixDimensions CalculateAllAbilities(
        RacialSixDimensions racial,
        EffortSixDimensions effort,
        int level,
        PersonalityEffectsSixDimensions personalityEffect,
        AbilitySixDimensions extra
    )
    {
        return new AbilitySixDimensions
        {
            PhysicalAttack = CalculateNormal(
                racial.PhysicalAttack,
                effort.PhysicalAttack,
                level,
                personalityEffect.PhysicalAttack,
                extra.PhysicalAttack
            ),
            SpecialAttack = CalculateNormal(
                racial.SpecialAttack,
                effort.SpecialAttack,
                level,
                personalityEffect.SpecialAttack,
                extra.SpecialAttack
            ),
            PhysicalDefense = CalculateNormal(
                racial.PhysicalDefense,
                effort.PhysicalDefense,
                level,
                personalityEffect.PhysicalDefense,
                extra.PhysicalDefense
            ),
            SpecialDefense = CalculateNormal(
                racial.SpecialDefense,
                effort.SpecialDefense,
                level,
                personalityEffect.SpecialDefense,
                extra.SpecialDefense
            ),
            Speed = CalculateNormal(
                racial.Speed,
                effort.Speed,
                level,
                personalityEffect.Speed,
                extra.Speed
            ),
            HP = CalculateHP(
                racial.HP,
                effort.HP,
                level,
                personalityEffect.HP,
                extra.HP
            )
        };
    }
}