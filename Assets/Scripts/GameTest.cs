using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

//测试用
public class GameTest : MonoBehaviour
{

    //测试用双方
    FightPlayer player1 = new FightPlayer();
    FightPlayer player2 = new FightPlayer();

    // Start is called before the first frame update
    void Start()
    {
        SixDimensionsValue sixZero=new SixDimensionsValue(0, 0, 0, 0, 0, 0);

        //测试用精灵
        Pet testPet1 = new Pet("大坝");
        testPet1.Lv = 100;
        testPet1.personality = Personality.固执;
        testPet1.abilityValue = sixZero;
        testPet1.racialValue = new SixDimensionsValue(100f, 100f, 100f, 100f, 100f, 300f);
        testPet1.effortValue = sixZero;
        testPet1.extraValue = sixZero;
        testPet1.RefreshCapability();

        Debug.Log(testPet1.petName + "物攻为：" + testPet1.abilityValue.physicalAttack);
        //Debug.Log(testPet1.petName+"速度为："+testPet1.abilityValue.speed);

        Pet testPet2 = new Pet("日你");
        testPet2.Lv = 100;
        testPet2.personality = Personality.保守;
        testPet2.abilityValue = sixZero;
        testPet2.racialValue = new SixDimensionsValue(100f, 100f, 100f, 100f, 100f, 300f);
        testPet2.effortValue = sixZero;
        testPet2.extraValue = sixZero;
        testPet2.RefreshCapability();
        Debug.Log(testPet2.petName + "物攻为：" + testPet1.abilityValue.physicalAttack);
        //Debug.Log(testPet2.petName + "速度为：" + testPet2.abilityValue.speed);

        player1.PetList.Add(testPet1);
        player2.PetList.Add(testPet2);

        FightManager.Instance.challenger = player1;
        FightManager.Instance.challenged = player2;


        FightManager.Instance.EnterFight();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
