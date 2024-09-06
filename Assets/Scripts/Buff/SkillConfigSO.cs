using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillConfig", menuName = "Skill System/SkillConfig")]
public class SkillConfigSO : ScriptableObject
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
    public AbstractEffect[] skillEffects;


}
