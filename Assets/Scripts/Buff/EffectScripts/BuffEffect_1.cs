using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffEffect_1", menuName = "Buff Effects/BuffEffect_1")]
public class BuffEffect_1 : AbstractEffect
{
    public int speedUpValue;

    public override void Apply(object onwer, object other)
    {
        Pet onwerPet = onwer as Pet;
        Pet otherPet = other as Pet;


        Debug.Log(onwerPet.fightAbility.Speed);

        onwerPet.fightAbility.Speed += speedUpValue;

        Debug.Log(onwerPet.petName + "速度提升了十点！");

        Debug.Log(onwerPet.fightAbility.Speed);




    }

}
