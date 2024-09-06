using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������
public class GameTest : MonoBehaviour
{

    //������˫��
    FightPlayer player1 = new FightPlayer();
    FightPlayer player2 = new FightPlayer();

    // Start is called before the first frame update
    void Start()
    {
        //��������
        AbilitySixDimensions abilitySixDimensions0 = new AbilitySixDimensions(0, 0, 0, 0, 0, 0);
        AbilitySixDimensions abilitySixDimensions50 =new AbilitySixDimensions(50, 50, 50, 50, 50, 50);
        RacialSixDimensions racialSixDimensions100=new RacialSixDimensions(100, 100, 100, 100, 100, 300);
        EffortSixDimensions effortSixDimensions0 =new EffortSixDimensions(0, 0, 0, 0, 0, 0);

        //���ؼ���
        // ���� SkillSO ��Դ
        SkillConfigSO loadSkill = Resources.Load<SkillConfigSO>("SO/skill1");
        if (loadSkill != null)
        {
            Debug.Log($"�Ѽ��ؼ���: {loadSkill.skillName}");
        }
        else
        {
            Debug.LogError("δ�ҵ����� SO");
        }

        //����buff
        // ���� BuffSO ��Դ
        // todo ��������Ҫ��װ�� ����·��ֱ�ӷ���
        BuffConfigSO loadBuff = Resources.Load<BuffConfigSO>("SO/buff1");
        if (loadBuff != null)
        {
            Debug.Log($"�Ѽ���buff: {loadBuff.buffName}");
        }
        else
        {
            Debug.LogError("δ�ҵ�buff SO");
        }



        // ��������ʼ�������þ���
        Pet testPet1 = new Pet
        {
            petName = "����",
            Lv = 100,
            personality = Personality.��ִ,
            ability = abilitySixDimensions50,
            racial = racialSixDimensions100,
            effort = effortSixDimensions0,
            extra = abilitySixDimensions0,
            currentSkills = new List<SkillInfo>(),
            buffInfos= new List<BuffInfo>(),
        };
        testPet1.currentSkills.Add(Pet.CreateSkillInfoFromConfig(loadSkill));
        testPet1.buffInfos.Add(Pet.CreateBuffInfoFromConfig(loadBuff));

        testPet1.RefreshCapability();
        //�������״̬
        //testPet1.PrintStatus(); 

        Pet testPet2 = new Pet
        {
            petName = "���",
            Lv = 100,
            personality = Personality.����,
            ability = abilitySixDimensions50,
            racial = racialSixDimensions100,
            effort = effortSixDimensions0,
            extra = abilitySixDimensions0,
            currentSkills = new List<SkillInfo>(),
            buffInfos = new List<BuffInfo>(),
        };
        testPet2.currentSkills.Add(Pet.CreateSkillInfoFromConfig(loadSkill));

        testPet2.RefreshCapability();
        //testPet2.PrintStatus();

        //������뱳��
        player1.petBag.Add(testPet1);
        player2.petBag.Add(testPet2);

        //�����ս
        FightManager.Instance.challenger = player1;
        FightManager.Instance.challenged = player2;
        FightManager.Instance.EnterFight(player1, player2);

    }

}
