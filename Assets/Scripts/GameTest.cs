using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//测试用
public class GameTest : MonoBehaviour
{

    //测试用双方
    FightPlayer player1 = new FightPlayer();
    FightPlayer player2 = new FightPlayer();

    // Start is called before the first frame update
    void Start()
    {
        //精灵数据
        AbilitySixDimensions abilitySixDimensions0 = new AbilitySixDimensions(0, 0, 0, 0, 0, 0);
        AbilitySixDimensions abilitySixDimensions50 =new AbilitySixDimensions(50, 50, 50, 50, 50, 50);
        RacialSixDimensions racialSixDimensions100=new RacialSixDimensions(100, 100, 100, 100, 100, 300);
        EffortSixDimensions effortSixDimensions0 =new EffortSixDimensions(0, 0, 0, 0, 0, 0);

        //加载技能
        // 加载 SkillSO 资源
        SkillConfigSO loadSkill = Resources.Load<SkillConfigSO>("SO/skill1");
        if (loadSkill != null)
        {
            Debug.Log($"已加载技能: {loadSkill.skillName}");
        }
        else
        {
            Debug.LogError("未找到技能 SO");
        }

        //加载buff
        // 加载 BuffSO 资源
        // todo 这两个都要封装下 传入路径直接返回
        BuffConfigSO loadBuff = Resources.Load<BuffConfigSO>("SO/buff1");
        if (loadBuff != null)
        {
            Debug.Log($"已加载buff: {loadBuff.buffName}");
        }
        else
        {
            Debug.LogError("未找到buff SO");
        }



        // 创建并初始化测试用精灵
        Pet testPet1 = new Pet
        {
            petName = "日你",
            Lv = 100,
            personality = Personality.固执,
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
        //输出所有状态
        //testPet1.PrintStatus(); 

        Pet testPet2 = new Pet
        {
            petName = "大坝",
            Lv = 100,
            personality = Personality.保守,
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

        //精灵加入背包
        player1.petBag.Add(testPet1);
        player2.petBag.Add(testPet2);

        //进入对战
        FightManager.Instance.challenger = player1;
        FightManager.Instance.challenged = player2;
        FightManager.Instance.EnterFight(player1, player2);

    }

}
