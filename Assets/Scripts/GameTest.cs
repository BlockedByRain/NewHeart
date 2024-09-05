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

        AbilitySixDimensions abilitySixDimensions0 = new AbilitySixDimensions(0, 0, 0, 0, 0, 0);
        AbilitySixDimensions abilitySixDimensions50 =new AbilitySixDimensions(50, 50, 50, 50, 50, 50);
        RacialSixDimensions racialSixDimensions100=new RacialSixDimensions(100, 100, 100, 100, 100, 300);
        EffortSixDimensions effortSixDimensions0 =new EffortSixDimensions(0, 0, 0, 0, 0, 0);

        //测试用精灵
        // 创建并初始化精灵
        Pet testPet1 = new Pet
        {
            petName = "日你",
            Lv = 100,
            personality = Personality.固执,
            ability = abilitySixDimensions50,
            racial = racialSixDimensions100,
            effort = effortSixDimensions0,
            extra = abilitySixDimensions0
        };
        testPet1.RefreshCapability();
        testPet1.PrintStatus(); // 输出所有状态

        Pet testPet2 = new Pet
        {
            petName = "大坝",
            Lv = 100,
            personality = Personality.保守,
            ability = abilitySixDimensions50,
            racial = racialSixDimensions100,
            effort = effortSixDimensions0,
            extra = abilitySixDimensions0
        };
        testPet2.RefreshCapability();
        testPet2.PrintStatus();

        player1.petBag.Add(testPet1);
        player2.petBag.Add(testPet2);

        FightManager.Instance.challenger = player1;
        FightManager.Instance.challenged = player2;


        FightManager.Instance.EnterFight();

    }

}
