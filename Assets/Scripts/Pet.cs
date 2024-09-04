using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����
/// </summary>
public class Pet
{
    /// <summary>
    /// ����id
    /// </summary>
    public int petId;

    /// <summary>
    /// ������
    /// </summary>
    public string petName;

    /// <summary>
    /// ����ȼ�
    /// </summary>
    public int Lv;

    /// <summary>
    /// �¼����辭��
    /// </summary>
    public int nextLvExp;

    /// <summary>
    /// ����ֵ
    /// </summary>
    public SixDimensionsValue racialValue;


    /// <summary>
    /// ����ֵ
    /// </summary>
    public SixDimensionsValue abilityValue;


    /// <summary>
    /// Ŭ��ֵ
    /// </summary>
    public SixDimensionsValue effortValue;


    /// <summary>
    /// ��������ֵ
    /// </summary>
    public SixDimensionsValue extraValue;


    /// <summary>
    /// �Ը�
    /// </summary>
    public Personality personality;

    /// <summary>
    /// ����
    /// </summary>
    public Feature feature;

    /// <summary>
    /// ���ü�����
    /// </summary>
    public List<Skill> availableSkills;

    /// <summary>
    /// ��ǰ������
    /// </summary>
    public List<Skill> currentSkills;

    /// <summary>
    /// ��ӡid
    /// </summary>
    public string soulSealId;

    /// <summary>
    /// ս���е�ǰ����
    /// </summary>
    public SixDimensionsValue currentValue;

    /// <summary>
    /// ս���������ȼ�
    /// </summary>
    public SixDimensionsValue abilityLv;











    /// <summary>
    /// ˢ������
    /// </summary>
    public void RefreshCapability()
    {
        SixDimensionsValue personalityEffects = PersonalityEffects.GetEffect(this.personality);


        //Debug.Log(abilityValue.physicalAttack);
        //Debug.Log(racialValue.physicalAttack);
        //Debug.Log(effortValue.physicalAttack);
        //Debug.Log(personalityEffects.physicalAttack);
        //Debug.Log(extraValue.physicalAttack);


        //��������ֵ=������(����ֵ*2+Ŭ��ֵ��4+����)*(����ȼ���100)+5��*�Ը�������*��װ�ٷֱȣ����ޣ���+�ⲿ�ӳ�
        abilityValue.physicalAttack = (int)(((((racialValue.physicalAttack * 2) + (effortValue.physicalAttack / 4) + 31) * (Lv / 100) + 5) * personalityEffects.physicalAttack) + extraValue.physicalAttack);
        abilityValue.specialAttack = (int)(((((racialValue.specialAttack * 2) + (effortValue.specialAttack / 4) + 31) * (Lv / 100) + 5) * personalityEffects.specialAttack) + extraValue.specialAttack);
        abilityValue.physicalDefense = (int)(((((racialValue.physicalDefense * 2) + (effortValue.physicalDefense / 4) + 31) * (Lv / 100) + 5) * personalityEffects.physicalDefense) + extraValue.physicalDefense);
        abilityValue.specialDefense = (int)(((((racialValue.specialDefense * 2) + (effortValue.specialDefense / 4) + 31) * (Lv / 100) + 5) * personalityEffects.specialDefense) + extraValue.specialDefense);
        abilityValue.speed = (int)(((((racialValue.speed * 2) + (effortValue.speed / 4) + 31) * (Lv / 100) + 5) * personalityEffects.speed) + extraValue.speed);

        //��������ֵ=����(����ֵ*2+Ŭ��ֵ��4+100+����)*(����ȼ���100)+10��*��װ�ٷֱȼӳɣ����ޣ���+�ⲿ�ӳ�
        abilityValue.HP = (int)(((((racialValue.HP * 2) + (effortValue.HP / 4) + 100 + 31) * (Lv / 100) + 5) * personalityEffects.HP) + extraValue.HP);


    }


    public Pet(string name)
    {
        petName = name;
    }



}

/// <summary>
/// ��άֵ
/// </summary>
public class SixDimensionsValue
{
    /// <summary>
    /// �﹥
    /// </summary>
    public float physicalAttack=0;
    /// <summary>
    /// �ع�
    /// </summary>
    public float specialAttack = 0;
    /// <summary>
    /// ���
    /// </summary>
    public float physicalDefense = 0;
    /// <summary>
    /// �ط�
    /// </summary>
    public float specialDefense = 0;
    /// <summary>
    /// �ٶ�
    /// </summary>
    public float speed = 0;
    /// <summary>
    /// ����
    /// </summary>
    public float HP = 0;


    public SixDimensionsValue(float physicalAttack, float specialAttack, float physicalDefense, float specialDefense, float speed, float HP)
    {
        this.physicalAttack = physicalAttack;
        this.specialAttack = specialAttack;
        this.physicalDefense = physicalDefense;
        this.specialDefense = specialDefense;
        this.speed = speed;
        this.HP = HP;
    }
}


