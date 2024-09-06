using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillEffect_0", menuName = "Skill Effects/SkillEffect_0")]
public class SkillEffect_0 : AbstractEffect
{
    //��ӡ����Ϣ
    public string logMessage;

    public override void Apply(object user, object target)
    {
        Pet userPet = user as Pet;
        Pet targetPet = target as Pet;

        if (targetPet != null)
        {
            Debug.Log(userPet.petName+"ʹ���˼��ܣ�"+logMessage);
        }
    }
}

