using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;
using UnityEngine;

public static class PersonalitySystem
{
    private static bool _isInitialized;
    private static Dictionary<int, PersonalityDefinition> _personalities = new();

    /// <summary>
    /// 初始化性格系统
    /// </summary>
    /// <param name="jsonPath">JSON 文件路径（无需后缀）</param>
    public static void Initialize(string jsonPath = "Json/PersonalityConfig")
    {
        if (_isInitialized) return;

        try
        {
            // 加载 JSON 文件
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);
            if (jsonFile == null) throw new Exception($"性格配置文件未找到: {jsonPath}");

            // 反序列化配置
            var config = JsonConvert.DeserializeObject<PersonalityConfig>(jsonFile.text);

            // 加载性格定义
            LoadPersonalities(config.Personalities);

            _isInitialized = true;
            Debug.Log("性格系统初始化完成");
        }
        catch (Exception ex)
        {
            Debug.LogError($"性格系统初始化失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 获取性格的影响系数
    /// </summary>
    public static PersonalityEffectsSixDimensions GetPersonalityEffect(int personalityId)
    {
        if (!_isInitialized) throw new Exception("性格系统未初始化");
        PersonalityEffectsSixDimensions personalityEffectsSixDimensions=new();

        if (_personalities.TryGetValue(personalityId, out var personality))
        {
            personalityEffectsSixDimensions.PhysicalAttack = personality.PhysicalAttackMultiplier;
            personalityEffectsSixDimensions.SpecialAttack = personality.SpecialAttackMultiplier;
            personalityEffectsSixDimensions.PhysicalDefense = personality.PhysicalDefenseMultiplier;
            personalityEffectsSixDimensions.SpecialDefense = personality.SpecialDefenseMultiplier;
            personalityEffectsSixDimensions.Speed = personality.SpeedMultiplier;
            personalityEffectsSixDimensions.HP = personality.HPMultiplier;
            return personalityEffectsSixDimensions;
        }


        throw new Exception($"未定义的性格ID: {personalityId}");
    }

    /// <summary>
    /// 获取性格名
    /// </summary>
    public static string GetPersonalityName(int personalityId)
    {
        if (!_isInitialized) throw new Exception("性格系统未初始化");

        PersonalityDefinition personality = GetPersonality(personalityId);
        return personality.Name;
    }


    private static void LoadPersonalities(List<PersonalityDefinition> personalities)
    {
        foreach (var personality in personalities)
        {
            _personalities[personality.Id] = personality;
        }
    }

    //=== 辅助方法 ===//
    public static PersonalityDefinition GetPersonality(int id)
    {      
        if (_personalities.TryGetValue(id, out var per)) return per;
        throw new Exception($"未定义的属性ID: {id}");
    }


}

