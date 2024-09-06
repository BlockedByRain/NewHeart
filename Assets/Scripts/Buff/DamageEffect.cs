using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Skill Effects/DamageEffect")]
public class DamageEffect : AbstractEffect
{
    public int damageAmount;

    public override void Apply(object user, object target)
    {
        Pet userPet = user as Pet;
        Pet targetPet = target as Pet;

        if (targetPet != null)
        {
            // �˺��߼��������Ŀ�������ֵ�п۳��˺�ֵ
            //targetPet.TakeDamage(damageAmount);
            Debug.Log($"{userPet.petName} �� {targetPet.petName} ����� {damageAmount} ���˺���");
        }
    }
}

