using UnityEngine;

/// <summary>
/// ��ά�Ը�Ӱ���࣬���ڱ�ʾ�����Ը�������Ե�Ӱ�죬����ֵΪ 0.9f��1f �� 1.1f��
/// </summary>
public class PersonalityEffectsSixDimensions : SixDimensions
{
    // �Ը��ÿ�����Ե�Ӱ�죨���١����䡢���ӣ�
    private float physicalAttackMultiplier;
    private float specialAttackMultiplier;
    private float physicalDefenseMultiplier;
    private float specialDefenseMultiplier;
    private float speedMultiplier;
    private float hpMultiplier;

    // ���캯��
    public PersonalityEffectsSixDimensions(float physicalAttackMultiplier = 1f, float specialAttackMultiplier = 1f, float physicalDefenseMultiplier = 1f, float specialDefenseMultiplier = 1f, float speedMultiplier = 1f, float hpMultiplier = 1f)
    {
        this.physicalAttackMultiplier = physicalAttackMultiplier;
        this.specialAttackMultiplier = specialAttackMultiplier;
        this.physicalDefenseMultiplier = physicalDefenseMultiplier;
        this.specialDefenseMultiplier = specialDefenseMultiplier;
        this.speedMultiplier = speedMultiplier;
        this.hpMultiplier = hpMultiplier;
    }

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
