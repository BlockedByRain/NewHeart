using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    //����id
    public int id;
    //������
    public string name;
    //��������
    public string description;
    //��������
    public SkillType skillType;
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



    public SkillInfo(int skillId, string name, string description, SkillType skillType, int skillPower, int maxPP, bool isPredestinate, int initialCritical, int skillSpeed, List<SkillEffect> skillEffects)
    {
        this.id = skillId;
        this.name = name;
        this.description = description;
        this.skillType = skillType;
        this.skillPower = skillPower;
        this.maxPP = maxPP;
        this.isPredestinate = isPredestinate;
        this.initialCritical = initialCritical;
        this.skillSpeed = skillSpeed;
        this.skillEffects = skillEffects;
    }


    public void Execute(Pet user, Pet target)
    {
        if (skillEffects == null || skillEffects.Count == 0)
        {
            Debug.Log("�˼�����Ч��");
            return;
        }
        else
        {
            // ��һִ�м���Ч��
            foreach (var skillEffect in skillEffects)
            {
                try
                {
                    skillEffect.Execute(user, target);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"����Ч��ִ��ʧ��: {ex.Message}");
                }
            }
        }
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