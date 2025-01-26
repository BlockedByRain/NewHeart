using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public static class PersonalitySystem
{
    private static bool _isInitialized;
    private static PersonalityConfig _config;
    private static Dictionary<(string, string), float> _relationMap = new();

    // 初始化系统（建议在游戏启动时调用）
    public static void Initialize(string jsonPath = "PersonalityConfig")
    {
        if (_isInitialized) return;

        // 加载JSON文本
        var jsonFile = Resources.Load<TextAsset>(jsonPath);
        if (jsonFile == null)
        {
            Debug.LogError($"性格配置文件未找到: {jsonPath}");
            return;
        }

        // 反序列化配置
        try
        {
            _config = JsonConvert.DeserializeObject<PersonalityConfig>(jsonFile.text);
            //BuildRelationMap();
            _isInitialized = true;
        }
        catch (JsonException ex)
        {
            Debug.LogError($"JSON解析错误: {ex.Message}");
        }
    }

    //private static void BuildRelationMap()
    //{
    //    foreach (var relation in _config.Relations)
    //    {
    //        var key = (relation.Attacking, relation.Defending);
    //        _relationMap[key] = relation.Multiplier;
    //    }
    //}

    ///// <summary>
    ///// 获取属性克制倍数
    ///// </summary>
    //public static float GetMultiplier(Attribute[] attackerAttrs, Attribute[] defenderAttrs)
    //{
    //    if (!_isInitialized)
    //    {
    //        Debug.LogWarning("属性系统未初始化！");
    //        return _config.Settings.DefaultMultiplier;
    //    }

    //    float total = 1f;
    //    foreach (var attackAttr in attackerAttrs)
    //    {
    //        foreach (var defendAttr in defenderAttrs)
    //        {
    //            var multiplier = GetSingleRelation(
    //                attackAttr.ToString(),
    //                defendAttr.ToString()
    //            );
    //            total *= multiplier;
    //        }
    //    }

    //}

    //private static float GetSingleRelation(string attacking, string defending)
    //{
    //    if (_relationMap.TryGetValue((attacking, defending), out var value))
    //        return value;

    //    Debug.LogWarning($"未定义克制关系: {attacking} → {defending}");
    //    return _config.Settings.DefaultMultiplier;
    //}
}