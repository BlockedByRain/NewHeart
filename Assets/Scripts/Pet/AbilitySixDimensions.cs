using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ά����ֵ�࣬���ڱ�ʾ���������ֵ��
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
    /// ���캯��
    /// </summary>
    /// <param name="physicalAttack">�﹥ֵ</param>
    /// <param name="specialAttack">�ع�ֵ</param>
    /// <param name="physicalDefense">���ֵ</param>
    /// <param name="specialDefense">�ط�ֵ</param>
    /// <param name="speed">�ٶ�ֵ</param>
    /// <param name="hp">����ֵ</param>
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



