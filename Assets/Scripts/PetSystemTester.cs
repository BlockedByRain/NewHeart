using UnityEngine;

/// <summary>
/// 精灵系统功能测试脚本，挂载到空物体上运行测试用例。
/// </summary>
public class PetSystemTester : MonoBehaviour
{
    [Header("测试参数配置")]
    [SerializeField] private int testRacialPhysicalAttack = 100;  // 测试种族值（物攻）
    [SerializeField] private int testEffortPhysicalAttack = 252;  // 测试努力值（物攻）
    [SerializeField] private int testLevel = 50;                  // 测试等级
    [SerializeField] private Personality testPersonality = Personality.固执; // 测试性格

    [Header("ScriptableObject 配置")]
    [SerializeField] private PersonalityEffectConfig personalityConfig; // 直接拖拽配置文件到 Inspector

    private void Start()
    {
        // 确保配置文件自动加载
        LoadPersonalityConfig();

        // 运行测试用例
        TestAbilityCalculator();
        TestPersonalityEffects();
    }

    /// <summary>
    /// 自动加载性格修正配置文件
    /// </summary>
    private void LoadPersonalityConfig()
    {
        // 配置文件路径（无需文件扩展名）
        const string configPath = "Configs/PersonalityEffectConfig";
        var config = Resources.Load<PersonalityEffectConfig>(configPath);

        if (config == null)
        {
            Debug.LogError($"未找到性格修正配置文件！请检查：\n" +
                           $"1. 文件是否命名为 PersonalityEffectConfig\n" +
                           $"2. 是否放置在 Resources/Configs 目录下\n" +
                           $"3. 文件类型是否为 ScriptableObject");
        }
        else
        {
            Debug.Log($"成功加载配置文件：{config.name}");
        }
    }


    /// <summary>
    /// 测试能力值计算公式
    /// </summary>
    private void TestAbilityCalculator()
    {
        Debug.Log("=== 开始测试能力值计算 ===");

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
            racialValue: 100,  // 假设种族值HP=100
            effortValue: 252,  // 努力值HP=252
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
        Debug.Log("=== 开始测试性格修正获取 ===");

        // 测试已配置的性格（如 固执）
        var effect = PersonalityEffects.GetEffect(testPersonality);
        Debug.Log($"性格 {testPersonality} 的物攻修正: {effect.physicalAttackMultiplier}");

        // 测试未配置的性格（如 开朗）
        var unknownEffect = PersonalityEffects.GetEffect(Personality.开朗);
        Debug.Log($"未配置性格的默认物攻修正: {unknownEffect.physicalAttackMultiplier}（应为1.0）");
        Debug.Assert(unknownEffect.physicalAttackMultiplier == 1.0f, "默认性格修正错误！");

        Debug.Log("=== 性格修正获取测试完成 ===");
    }
}