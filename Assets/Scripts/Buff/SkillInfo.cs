using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʱ�ļ���������
/// </summary>
public class SkillInfo
{
    public SkillConfig skillConfig;
    public GameObject user;
    public GameObject target;
   

    //todo

    public void Execute(Pet user, Pet target)
    {
        if (skillConfig.skillEffects == null || skillConfig.skillEffects.Count == 0)
        {
            Debug.Log("�˼�������Ч��");
        }
        else
        {
            // ��һִ�м���Ч��
            foreach (var skillEffect in skillConfig.skillEffects)
            {
                try
                {
                    skillEffect.Execute(user, target);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(skillEffect.effect.effectDescription+ $"����Ч��ִ��ʧ��: {ex.Message}");
                }
            }
        }



        //todo �˺�����
        Damage damage = new Damage();
        damage = damage.GetAttackDamage(user, target, skillConfig);

        //ǰ����Ҫ ���Կ��ơ����ԡ�������ϵͳ
    }


}

/// <summary>
/// ����������
/// </summary>
public class SkillConfig
{
    //����id
    public int skillId;
    //������
    public string skillName;
    //��������
    public string skillDescription;
    //��������
    public SkillType skillType;
    //��������
    public Attribute attribute;
    //��������
    public int skillPower;
    //������
    public int maxPP;
    //�Ƿ����
    public bool isPredestinate;
    //��ʼ��������ʮ����֮��������ʾ����1����1/16=6.25%���ʱ���
    public int initialCritical;
    //��������
    public int skillSpeed;
    //����Ч��
    public List<SkillEffect> skillEffects;

    public SkillConfig(int skillId, string name, string description, SkillType skillType, Attribute attribute, int skillPower, int maxPP, bool isPredestinate, int initialCritical, int skillSpeed, List<SkillEffect> skillEffects)
    {
        this.skillId = skillId;
        this.skillName = name;
        this.skillDescription = description;
        this.skillType = skillType;
        this.attribute = attribute;
        this.skillPower = skillPower;
        this.maxPP = maxPP;
        this.isPredestinate = isPredestinate;
        this.initialCritical = initialCritical;
        this.skillSpeed = skillSpeed;
        this.skillEffects = skillEffects;
    }

}


[System.Serializable]
public class SkillEffect
{
    public AbstractEffect effect;

    public SkillEffect(AbstractEffect effect)
    {
        this.effect = effect;
    }

    // ִ��Ч�������ÿ��Ч������ִ��
    public void Execute(object user, object target)
    {

        effect.Apply(user, target);

    }
}

public enum SkillType
{
    //������
    Physical,
    //���⹥��
    Special,
    //���Լ���
    Attribute,
}