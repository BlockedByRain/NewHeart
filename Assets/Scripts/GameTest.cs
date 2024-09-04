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
        SixDimensionsValue sixZero=new SixDimensionsValue(0, 0, 0, 0, 0, 0);

        //�����þ���
        Pet testPet1 = new Pet("���");
        testPet1.Lv = 100;
        testPet1.personality = Personality.��ִ;
        testPet1.abilityValue = sixZero;
        testPet1.racialValue = new SixDimensionsValue(100f, 100f, 100f, 100f, 100f, 300f);
        testPet1.effortValue = sixZero;
        testPet1.extraValue = sixZero;
        testPet1.RefreshCapability();

        Debug.Log(testPet1.petName + "�﹥Ϊ��" + testPet1.abilityValue.physicalAttack);
        //Debug.Log(testPet1.petName+"�ٶ�Ϊ��"+testPet1.abilityValue.speed);

        Pet testPet2 = new Pet("����");
        testPet2.Lv = 100;
        testPet2.personality = Personality.����;
        testPet2.abilityValue = sixZero;
        testPet2.racialValue = new SixDimensionsValue(100f, 100f, 100f, 100f, 100f, 300f);
        testPet2.effortValue = sixZero;
        testPet2.extraValue = sixZero;
        testPet2.RefreshCapability();
        Debug.Log(testPet2.petName + "�﹥Ϊ��" + testPet1.abilityValue.physicalAttack);
        //Debug.Log(testPet2.petName + "�ٶ�Ϊ��" + testPet2.abilityValue.speed);

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
