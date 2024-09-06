using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillConfig", menuName = "Skill System/SkillConfig")]
public class SkillConfigSO : ScriptableObject
{
    //技能id
    public int skillId;
    //技能名
    public string skillName;
    //技能描述
    public string skillDescription;
    //技能类型
    public SkillType skillType;
    //技能威力
    public int skillPower;
    //最大次数
    public int maxPP;
    //是否必中
    public bool isPredestinate;
    //初始暴击，按十六分之多少来表示，即1代表1/16=6.25%概率暴击
    public int initialCritical;
    //技能先制
    public int skillSpeed;
    //技能效果
    public AbstractEffect[] skillEffects;


}
