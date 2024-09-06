using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffEffect_0", menuName = "Buff Effects/BuffEffect_0")]
public class BuffEffect_0 : AbstractEffect
{
    public override void Apply(object onwer, object other)
    {
        Pet onwerPet = onwer as Pet;
        Pet otherPet = other as Pet;

        Debug.Log(onwerPet.petName + effectDescription);
        //Debug.Log(userPet.petName+"开启了光之惩戒形态！");



    }

}
