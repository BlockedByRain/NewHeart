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
            // 伤害逻辑：比如从目标的生命值中扣除伤害值
            //targetPet.TakeDamage(damageAmount);
            Debug.Log($"{userPet.petName} 对 {targetPet.petName} 造成了 {damageAmount} 点伤害！");
        }
    }
}

