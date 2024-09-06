using UnityEngine;

/// <summary>
/// ��ά����ֵ�࣬���ڱ�ʾ������������Ļ�������ֵ��
/// </summary>
public class RacialSixDimensions : SixDimensions
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    // �������ֵ����
    private const int MaxRacialValue = 200;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="physicalAttack">�﹥ֵ</param>
    /// <param name="specialAttack">�ع�ֵ</param>
    /// <param name="physicalDefense">���ֵ</param>
    /// <param name="specialDefense">�ط�ֵ</param>
    /// <param name="speed">�ٶ�ֵ</param>
    /// <param name="hp">����ֵ</param>
    public RacialSixDimensions(int physicalAttack = 1, int specialAttack = 1, int physicalDefense = 1, int specialDefense = 1, int speed = 1, int hp = 1)
    {
        this.physicalAttack = Mathf.Clamp(physicalAttack, 1, MaxRacialValue);
        this.specialAttack = Mathf.Clamp(specialAttack, 1, MaxRacialValue);
        this.physicalDefense = Mathf.Clamp(physicalDefense, 1, MaxRacialValue);
        this.specialDefense = Mathf.Clamp(specialDefense, 1, MaxRacialValue);
        this.speed = Mathf.Clamp(speed, 1, MaxRacialValue);
        this.hp = Mathf.Clamp(hp, 1, MaxRacialValue);
    }

    public override float PhysicalAttack
    {
        get => physicalAttack;
        set => physicalAttack = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxRacialValue);
    }

    public override float SpecialAttack
    {
        get => specialAttack;
        set => specialAttack = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxRacialValue);
    }

    public override float PhysicalDefense
    {
        get => physicalDefense;
        set => physicalDefense = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxRacialValue);
    }

    public override float SpecialDefense
    {
        get => specialDefense;
        set => specialDefense = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxRacialValue);
    }

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxRacialValue);
    }

    public override float HP
    {
        get => hp;
        set => hp = Mathf.Clamp(Mathf.RoundToInt(value), 0, MaxRacialValue);
    }

    
}
