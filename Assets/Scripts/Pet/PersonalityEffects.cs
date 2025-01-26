using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 性格枚举
/// </summary>
public enum Personality
{
    固执,
    保守,
    开朗,
    // 添加更多性格
}

/// <summary>
/// 性格影响
/// </summary>
public static class PersonalityEffects
{
    private static PersonalityEffectConfig _config;

    public static PersonalityEffectConfig.EffectEntry GetEffect(Personality personality)
    {
        if (_config == null)
        {
            _config = Resources.Load<PersonalityEffectConfig>("Configs/PersonalityEffectConfig");
            if (_config == null) Debug.LogError("未找到性格修正配置文件！");
        }

        foreach (var entry in _config.effects)
        {
            if (entry.personality == personality)
                return entry;
        }



        return new PersonalityEffectConfig.EffectEntry(); // 返回默认值
    }



    // 默认效果
    private static readonly PersonalityEffectsSixDimensions defaultEffect = new PersonalityEffectsSixDimensions(1, 1, 1, 1, 1, 1);


    // 定义性格影响
    private static readonly Dictionary<Personality, PersonalityEffectsSixDimensions> personalityEffects = new Dictionary<Personality, PersonalityEffectsSixDimensions>()
    {
        //物攻、特攻、物防、特防、速度、体力（体力无性格影响，即最后一项恒为1）
        { Personality.固执, new PersonalityEffectsSixDimensions(1.1f, 0.9f, 1f, 1f, 1f, 1f) },
        { Personality.保守, new PersonalityEffectsSixDimensions(0.9f, 1.1f, 1f, 1f, 1f, 1f) },
        // 添加更多性格的影响
    };

}