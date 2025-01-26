using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillEffect_0", menuName = "Skill Effects/SkillEffect_0")]
public class SkillEffect_0 : AbstractEffect
{
    //打印的信息
    public string logMessage;

    public override void Apply(object user, object target)
    {
        Pet userPet = user as Pet;
        Pet targetPet = target as Pet;

        if (targetPet != null)
        {
            Debug.Log(userPet.petName+"使用了技能："+logMessage);
        }
    }
}

