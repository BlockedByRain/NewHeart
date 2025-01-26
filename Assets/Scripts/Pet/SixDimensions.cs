using UnityEngine;

/// <summary>
/// 六维值
/// </summary>
/// 
public abstract class SixDimensions<T> where T : SixDimensions<T>
{
    protected abstract int GetMinValue();
    protected abstract int GetMaxValue();

    protected int ClampAndRound(float value)
    {
        int min = GetMinValue();
        int max = GetMaxValue();
        return Mathf.Clamp(Mathf.RoundToInt(value), min, max);
    }

    // 抽象属性定义

    /// <summary>
    /// 物攻
    /// </summary>
    public abstract float PhysicalAttack { get; set; }

    /// <summary>
    /// 特攻
    /// </summary>
    public abstract float SpecialAttack { get; set; }
    /// <summary>
    /// 物防
    /// </summary>
    public abstract float PhysicalDefense { get; set; }
    /// <summary>
    /// 特防
    /// </summary>
    public abstract float SpecialDefense { get; set; }
    /// <summary>
    /// 速度
    /// </summary>
    public abstract float Speed { get; set; }
    /// <summary>
    /// 体力
    /// </summary>
    public abstract float HP { get; set; }
}






