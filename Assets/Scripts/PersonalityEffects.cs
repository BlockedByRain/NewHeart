using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

// 定义性格枚举
public enum Personality
{
    固执,
    保守,
    // 添加更多性格
}

/// <summary>
/// 性格影响
/// </summary>
public static class PersonalityEffects
{

    // 定义性格影响
    private static readonly Dictionary<Personality, SixDimensionsValue> personalityEffects = new Dictionary<Personality, SixDimensionsValue>()
    {
        //物攻、特攻、物防、特防、速度、体力（体力无性格影响，即最后一项恒为1）
        { Personality.固执, new SixDimensionsValue(1.1f, 0.9f, 1f, 1f, 1f, 1f) },
        { Personality.保守, new SixDimensionsValue(0.9f, 1.1f, 1f, 1f, 1f, 1f) },
        // 添加更多性格的影响
    };

    public static SixDimensionsValue GetEffect(Personality personality)
    {
        if (personalityEffects.TryGetValue(personality, out var effect))
        {
            return effect;
        }
        // 查询不到对应加成默认无影响
        return new SixDimensionsValue(1, 1, 1, 1, 1, 1); 
    }
}