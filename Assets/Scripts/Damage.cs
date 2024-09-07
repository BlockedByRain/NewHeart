using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    //�˺���ֵ
    public int damageValue;
    //�˺�����
    public DamageType damageType;
    //�˺�����
    public Attribute attribute;


    /// <summary>
    /// ���㹥���˺�
    /// </summary>
    public Damage GetAttackDamage(Pet creator, Pet target, SkillConfig skillConfig)
    {
        //�˺�ֵ=�����ȼ�ϵ��*��������*����������սʵ�ʽ���ֵ�·��ط���սʵ�ʷ���ֵ��+2��*������Χ*���Լӳ�*���Ʊ���*�������˰ٷֱ�*��1-�Է����˰ٷֱȣ���
        //�ȼ�ϵ�� =��0.4 * ����ȼ� + 2����50
        //������Χ��[217��255��1]

        //�ȼ�ϵ��
        float LvMultiplier = (float)(creator.Lv * 0.4 + 2) / 50;
        //��������
        float skillPower = skillConfig.skillPower;
        //ʵ������
        float curAttack;
        float curDefense;
        if (skillConfig.skillType == SkillType.Physical)
        {
            curAttack = creator.fightAbility.PhysicalAttack;
            curDefense = target.fightAbility.PhysicalDefense;
        }
        else
        {
            curAttack = creator.fightAbility.SpecialAttack;
            curDefense = target.fightAbility.SpecialDefense;
        }
        //���Լӳ� todo ˫����Ҳ�Ե�����ϵ
        this.attribute = skillConfig.attribute;

        float attributeMultiplier;
        if (creator.attribute == skillConfig.attribute)
        {
            attributeMultiplier = 1.5f;
        }
        else
        {
            attributeMultiplier = 1f;
        }

        //����ϵ�� [217��255��1]

        int a = Random.Range(217, 256);

        float FloatingMultiplier = (float)a / 255;


        //todo���Ʊ���
        float restraintMultiplier = 1f;


        //todo������


        //Debug.Log(LvMultiplier);
        //Debug.Log(skillPower);
        //Debug.Log(curAttack);
        //Debug.Log(curDefense);
        //Debug.Log(FloatingMultiplier);
        //Debug.Log(attributeMultiplier);
        //Debug.Log(restraintMultiplier);


        //�˺�ֵ=�����ȼ�ϵ��*��������*����������սʵ�ʽ���ֵ�·��ط���սʵ�ʷ���ֵ��+2��*������Χ*���Լӳ�*���Ʊ���*�������˰ٷֱ�*��1-�Է����˰ٷֱȣ���
        damageValue = (int)((LvMultiplier * skillPower * (curAttack / curDefense) + 2) * FloatingMultiplier * attributeMultiplier * restraintMultiplier);


        LogDamageInfo();




        return this;

    }

    
    public void LogDamageInfo()
    {
        Debug.Log("�˴��˺�Ϊ"+ attribute+"���Ե�"+damageType+"�����˺���"+"�˺�ֵΪ��"+ damageValue);

    }

}


public enum DamageType
{
    //�����˺���һ���ǹ�����ɵ��˺�
    Attack,
    //�ٷֱ��˺���һ���Ǵ��аٷֱ�������Ч���������˺�
    Percentage,
    //�̶��˺���һ���Ǵ��й̶��˺����ֵ�Ч���������˺�
    Fixed
}