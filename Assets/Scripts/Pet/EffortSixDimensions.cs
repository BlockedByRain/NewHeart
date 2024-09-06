using UnityEngine;

/// <summary>
/// 六维努力值类，用于表示精灵的努力值。
/// </summary>
public class EffortSixDimensions : SixDimensions
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    // 单项努力值最大值常量
    private const int MaxEffortValue = 255;
    // 总和努力值最大值常量
    private const int MaxTotalEffort = 510;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="physicalAttack">物攻值</param>
    /// <param name="specialAttack">特攻值</param>
    /// <param name="physicalDefense">物防值</param>
    /// <param name="specialDefense">特防值</param>
    /// <param name="speed">速度值</param>
    /// <param name="hp">体力值</param>
    public EffortSixDimensions(int physicalAttack = 0, int specialAttack = 0, int physicalDefense = 0, int specialDefense = 0, int speed = 0, int hp = 0)
    {
        //使用AddEffort 方法初始化，确保合法性
        AddEffort(physicalAttack, specialAttack, physicalDefense, specialDefense, speed, hp);
    }

    public override float PhysicalAttack
    {
        get => physicalAttack;
        set => physicalAttack = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxEffortValue);
    }

    public override float SpecialAttack
    {
        get => specialAttack;
        set => specialAttack = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxEffortValue);
    }

    public override float PhysicalDefense
    {
        get => physicalDefense;
        set => physicalDefense = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxEffortValue);
    }

    public override float SpecialDefense
    {
        get => specialDefense;
        set => specialDefense = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxEffortValue);
    }

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxEffortValue);
    }

    public override float HP
    {
        get => hp;
        set => hp = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxEffortValue);
    }

    /// <summary>
    /// 增加努力值到指定属性，确保总和不超过 510。
    /// </summary>
    /// <param name="physicalAttack">物攻增加值</param>
    /// <param name="specialAttack">特攻增加值</param>
    /// <param name="physicalDefense">物防增加值</param>
    /// <param name="specialDefense">特防增加值</param>
    /// <param name="speed">速度增加值</param>
    /// <param name="hp">体力增加值</param>
    /// <returns>是否成功添加</returns>
    public bool AddEffort(int physicalAttack = 0, int specialAttack = 0, int physicalDefense = 0, int specialDefense = 0, int speed = 0, int hp = 0)
    {
        // 计算当前努力值总和
        int currentTotalEffort = this.physicalAttack + this.specialAttack + this.physicalDefense + this.specialDefense + this.speed + this.hp;

        // 计算新努力值总和
        int newTotalEffort = currentTotalEffort + physicalAttack + specialAttack + physicalDefense + specialDefense + speed + hp;
        if (newTotalEffort > MaxTotalEffort)
        {
            // 如果新总和超过最大限制，则添加失败
            return false;
        }

        // 计算每个维度的新值
        int newPhysicalAttack = this.physicalAttack + physicalAttack;
        int newSpecialAttack = this.specialAttack + specialAttack;
        int newPhysicalDefense = this.physicalDefense + physicalDefense;
        int newSpecialDefense = this.specialDefense + specialDefense;
        int newSpeed = this.speed + speed;
        int newHP = this.hp + hp;

        // 确保在增加后不会超过单项最大值
        if (newPhysicalAttack > MaxEffortValue || newSpecialAttack > MaxEffortValue || newPhysicalDefense > MaxEffortValue || newSpecialDefense > MaxEffortValue || newSpeed > MaxEffortValue || newHP > MaxEffortValue)
        {
            // 如果某个维度超出单项限制，则添加失败
            return false;
        }
        // 更新努力值
        this.physicalAttack = newPhysicalAttack;
        this.specialAttack = newSpecialAttack;
        this.physicalDefense = newPhysicalDefense;
        this.specialDefense = newSpecialDefense;
        this.speed = newSpeed;
        this.hp = newHP;
        return true;
    }
}
