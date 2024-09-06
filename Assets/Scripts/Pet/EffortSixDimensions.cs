using UnityEngine;

/// <summary>
/// ��άŬ��ֵ�࣬���ڱ�ʾ�����Ŭ��ֵ��
/// </summary>
public class EffortSixDimensions : SixDimensions
{
    private int physicalAttack;
    private int specialAttack;
    private int physicalDefense;
    private int specialDefense;
    private int speed;
    private int hp;

    // ����Ŭ��ֵ���ֵ����
    private const int MaxEffortValue = 255;
    // �ܺ�Ŭ��ֵ���ֵ����
    private const int MaxTotalEffort = 510;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="physicalAttack">�﹥ֵ</param>
    /// <param name="specialAttack">�ع�ֵ</param>
    /// <param name="physicalDefense">���ֵ</param>
    /// <param name="specialDefense">�ط�ֵ</param>
    /// <param name="speed">�ٶ�ֵ</param>
    /// <param name="hp">����ֵ</param>
    public EffortSixDimensions(int physicalAttack = 0, int specialAttack = 0, int physicalDefense = 0, int specialDefense = 0, int speed = 0, int hp = 0)
    {
        //ʹ��AddEffort ������ʼ����ȷ���Ϸ���
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
    /// ����Ŭ��ֵ��ָ�����ԣ�ȷ���ܺͲ����� 510��
    /// </summary>
    /// <param name="physicalAttack">�﹥����ֵ</param>
    /// <param name="specialAttack">�ع�����ֵ</param>
    /// <param name="physicalDefense">�������ֵ</param>
    /// <param name="specialDefense">�ط�����ֵ</param>
    /// <param name="speed">�ٶ�����ֵ</param>
    /// <param name="hp">��������ֵ</param>
    /// <returns>�Ƿ�ɹ����</returns>
    public bool AddEffort(int physicalAttack = 0, int specialAttack = 0, int physicalDefense = 0, int specialDefense = 0, int speed = 0, int hp = 0)
    {
        // ���㵱ǰŬ��ֵ�ܺ�
        int currentTotalEffort = this.physicalAttack + this.specialAttack + this.physicalDefense + this.specialDefense + this.speed + this.hp;

        // ������Ŭ��ֵ�ܺ�
        int newTotalEffort = currentTotalEffort + physicalAttack + specialAttack + physicalDefense + specialDefense + speed + hp;
        if (newTotalEffort > MaxTotalEffort)
        {
            // ������ܺͳ���������ƣ������ʧ��
            return false;
        }

        // ����ÿ��ά�ȵ���ֵ
        int newPhysicalAttack = this.physicalAttack + physicalAttack;
        int newSpecialAttack = this.specialAttack + specialAttack;
        int newPhysicalDefense = this.physicalDefense + physicalDefense;
        int newSpecialDefense = this.specialDefense + specialDefense;
        int newSpeed = this.speed + speed;
        int newHP = this.hp + hp;

        // ȷ�������Ӻ󲻻ᳬ���������ֵ
        if (newPhysicalAttack > MaxEffortValue || newSpecialAttack > MaxEffortValue || newPhysicalDefense > MaxEffortValue || newSpecialDefense > MaxEffortValue || newSpeed > MaxEffortValue || newHP > MaxEffortValue)
        {
            // ���ĳ��ά�ȳ����������ƣ������ʧ��
            return false;
        }
        // ����Ŭ��ֵ
        this.physicalAttack = newPhysicalAttack;
        this.specialAttack = newSpecialAttack;
        this.physicalDefense = newPhysicalDefense;
        this.specialDefense = newSpecialDefense;
        this.speed = newSpeed;
        this.hp = newHP;
        return true;
    }
}
