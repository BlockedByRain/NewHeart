using UnityEngine;

/// <summary>
/// 精灵系统功能测试脚本，挂载到空物体上运行测试用例。
/// </summary>
public class PetSystemTester : MonoBehaviour
{

    private void Start()
    {
        // 初始化性格系统
        PersonalitySystem.Initialize();
        // 初始化属性系统
        AttributeSystem.Initialize();
        testSystem();
        //testSystem2();

    }


    //属性系统测试
    private void testSystem2()
    {
        

        AttributeSystem.PrintAllAttributeInfo();
        AttributeSystem.PrintAllRelationInfo();


        // 获取属性ID
        int fireId = AttributeSystem.GetAttribute(1).Id; // 1
        int waterGrassId = AttributeSystem.GetAttribute(4).Id; //4

        // 计算倍数
        float multiplier = AttributeSystem.GetMultiplier(fireId, waterGrassId);
        Debug.Log($"最终倍数: {multiplier}"); // 应输出1.25



        // 示例：火(1) 攻击 火电(4)
        //float multiplier = AttributeSystem.GetMultiplier(1, 4);
        //Debug.Log(multiplier);
        // 计算过程：
        // 1. 拆分防御方为火(1)和电(5)
        // 2. 火→火(未定义) → 1倍
        // 3. 火→电(未定义) → 1倍
        // 4. 应用规则：(1 + 1)/2 = 1倍

        // 示例：火(1) 攻击 水草(6)
        float multiplier2 = AttributeSystem.GetMultiplier(1, 6);
        Debug.Log(multiplier2);
        // 计算过程：
        // 1. 拆分防御方为火(1)和电(5)
        // 2. 火→火(未定义) → 1倍
        // 3. 火→电(未定义) → 1倍
        // 4. 应用规则：(1 + 1)/2 = 1倍
        // mv计算为1.25
        // ai流程：
        //        火（单属性）
        //↓
        //水草（双属性）→ 拆分为 水 +草
        //↓
        //计算火→水（0.5）和火→草（2.0）
        //↓
        //应用规则： (0.5 + 2.0) / 2 = 1.25
    }


    //性格系统测试
    private void testSystem()
    {

        // 运行测试用例
        TestAbilityCalculator();
        TestPersonalityEffects();
    }


    /// <summary>
    /// 测试能力值计算公式
    /// </summary>
    /// <summary>
    /// 测试能力值计算公式
    /// </summary>
    private void TestAbilityCalculator()
    {
        Debug.Log("=== 开始测试能力值计算 ===");

        // 定义测试变量
        float testRacialPhysicalAttack = 100f; // 种族值（物攻）
        float testEffortPhysicalAttack = 252f; // 努力值（物攻）
        int testLevel = 50; // 等级

        // 测试常规能力值（物攻）
        int calculatedPhysicalAttack = AbilityCalculator.CalculateNormal(
            racialValue: testRacialPhysicalAttack,
            effortValue: testEffortPhysicalAttack,
            level: testLevel,
            personalityMultiplier: 1.0f, // 假设性格修正为1.0
            extraValue: 0
        );

        // 预期值计算（手动验证公式）
        // ((100*2 + 252/4 +31) * 0.5 +5) *1 = (200+63+31)*0.5 +5 = 147 +5 = 152
        int expectedPhysicalAttack = 152;

        Debug.Log($"物理攻击计算结果: {calculatedPhysicalAttack}，预期值: {expectedPhysicalAttack}");
        Debug.Assert(calculatedPhysicalAttack == expectedPhysicalAttack, "常规能力值计算错误！");

        // 测试体力值（HP）
        int calculatedHP = AbilityCalculator.CalculateHP(
            racialValue: 100f,  // 假设种族值HP=100
            effortValue: 252f,  // 努力值HP=252
            level: 50,
            personalityMultiplier: 1.0f,
            extraValue: 0
        );

        // 预期值计算（手动验证公式）
        // ((100*2 + 252/4 +131) *0.5 +10) *1 = (200+63+131)*0.5 +10 = 197 +10 = 207
        int expectedHP = 207;

        Debug.Log($"体力值计算结果: {calculatedHP}，预期值: {expectedHP}");
        Debug.Assert(calculatedHP == expectedHP, "体力值计算错误！");

        Debug.Log("=== 能力值计算测试完成 ===");
    }



    /// <summary>
    /// 测试性格修正获取功能
    /// </summary>
    private void TestPersonalityEffects()
    {
        

        // 创建精灵并设置初始值
        Pet myPet = new Pet
        {
            petName = "小火龙",
            Lv = 5,
            attribute = 1,
            racial = new RacialSixDimensions(60, 50, 40, 50, 65, 45),
            effort = new EffortSixDimensions(0, 0, 0, 0, 0, 0),
            extra = new AbilitySixDimensions(0, 0, 0, 0, 0, 0),
            personality = 1
        };

        // 刷新能力值
        myPet.RefreshCapability();
        myPet.PrintStatus();
    }
}