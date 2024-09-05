using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

//������
public class GameTest : MonoBehaviour
{

    //������˫��
    FightPlayer player1 = new FightPlayer();
    FightPlayer player2 = new FightPlayer();

    // Start is called before the first frame update
    void Start()
    {

        AbilitySixDimensions abilitySixDimensions0 = new AbilitySixDimensions(0, 0, 0, 0, 0, 0);
        AbilitySixDimensions abilitySixDimensions50 =new AbilitySixDimensions(50, 50, 50, 50, 50, 50);
        RacialSixDimensions racialSixDimensions100=new RacialSixDimensions(100, 100, 100, 100, 100, 300);
        EffortSixDimensions effortSixDimensions0 =new EffortSixDimensions(0, 0, 0, 0, 0, 0);

        //�����þ���
        // ��������ʼ������
        Pet testPet1 = new Pet
        {
            petName = "����",
            Lv = 100,
            personality = Personality.��ִ,
            ability = abilitySixDimensions50,
            racial = racialSixDimensions100,
            effort = effortSixDimensions0,
            extra = abilitySixDimensions0
        };
        testPet1.RefreshCapability();
        testPet1.PrintStatus(); // �������״̬

        Pet testPet2 = new Pet
        {
            petName = "���",
            Lv = 100,
            personality = Personality.����,
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
