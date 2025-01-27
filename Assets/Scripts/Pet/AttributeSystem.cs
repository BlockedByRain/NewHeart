using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
/// <summary>
/// 属性系统核心类
/// </summary>
public static class AttributeSystem
{
    private static bool _isInitialized;

    // 核心数据存储
    private static Dictionary<int, AttributeDefinition> _attributes = new();
    private static Dictionary<(int, int), float> _baseRelations = new();

    /// <summary>
    /// 初始化属性系统
    /// </summary>
    /// <param name="jsonPath">Resources文件夹下的JSON路径（无需后缀）</param>
    public static void Initialize(string jsonPath = "Json/AttributesConfig")
    {
        if (_isInitialized) return;

        try
        {
            // 加载JSON文件
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);
            if (jsonFile == null) throw new Exception($"属性配置文件未找到: {jsonPath}");

            // 反序列化配置
            AttributeConfig config = JsonConvert.DeserializeObject<AttributeConfig>(jsonFile.text);

            // 加载属性定义
            LoadAttributes(config.Attributes);

            // 加载基础克制关系
            LoadBaseRelations(config.Relations);

            _isInitialized = true;
            Debug.Log("属性系统初始化完成");
        }
        catch (Exception ex)
        {
            Debug.LogError($"属性系统初始化失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 获取属性名
    /// </summary>
    public static string GetAttributeName(int attributeId)
    {
        if (!_isInitialized) throw new Exception("属性系统未初始化");

        AttributeDefinition attribute = GetAttribute(attributeId);

        return attribute.Name;
    }


    /// <summary>
    /// 获取属性克制倍数
    /// </summary>
    public static float GetMultiplier(int attackerId, int defenderId)
    {
        if (!_isInitialized) throw new Exception("属性系统未初始化");

        AttributeDefinition attacker = GetAttribute(attackerId);
        AttributeDefinition defender = GetAttribute(defenderId);

        return CalculateMultiplier(attacker, defender);
    }

    //=== 核心计算逻辑 ===//
    private static float CalculateMultiplier(AttributeDefinition attacker, AttributeDefinition defender)
    {
        // 情况1：单属性攻击单属性
        if (attacker.IsSingle && defender.IsSingle)
        {
            return GetBaseMultiplier(attacker.Id, defender.Id);
        }

        // 情况2：单属性攻击双属性
        if (attacker.IsSingle && defender.IsDual)
        {
            return HandleSingleVsDual(attacker, defender);
        }

        // 情况3：双属性攻击单属性
        if (attacker.IsDual && defender.IsSingle)
        {
            return HandleDualVsSingle(attacker, defender);
        }

        // 情况4：双属性攻击双属性
        if (attacker.IsDual && defender.IsDual)
        {
            return HandleDualVsDual(attacker, defender);
        }

        throw new ArgumentException("无效的属性组合");
    }

    //=== 具体处理逻辑 ===//
    private static float HandleSingleVsDual(AttributeDefinition attacker, AttributeDefinition defender)
    {
        int[] defComponents = defender.Components;
        float m1 = GetBaseMultiplier(attacker.Id, defComponents[0]);
        float m2 = GetBaseMultiplier(attacker.Id, defComponents[1]);
        return CalculateCombinedMultiplier(m1, m2);
    }

    private static float HandleDualVsSingle(AttributeDefinition attacker, AttributeDefinition defender)
    {
        int[] atkComponents = attacker.Components;
        float m1 = GetBaseMultiplier(atkComponents[0], defender.Id);
        float m2 = GetBaseMultiplier(atkComponents[1], defender.Id);
        return CalculateCombinedMultiplier(m1, m2);
    }

    private static float HandleDualVsDual(AttributeDefinition attacker, AttributeDefinition defender)
    {
        int[] defComponents = defender.Components;
        float m1 = CalculateMultiplier(attacker, GetAttribute(defComponents[0]));
        float m2 = CalculateMultiplier(attacker, GetAttribute(defComponents[1]));
        return (m1 + m2) / 2f;
    }

    //=== 组合系数计算规则 ===//
    private static float CalculateCombinedMultiplier(float m1, float m2)
    {
        // 规则1：双2倍情况
        if (Mathf.Approximately(m1, 2f) && Mathf.Approximately(m2, 2f))
            return 4f;

        // 规则2：存在0倍情况
        if (Mathf.Approximately(m1, 0f) || Mathf.Approximately(m2, 0f))
            return (m1 + m2) / 4f;

        // 规则3：常规情况
        return (m1 + m2) / 2f;
    }

    //=== 辅助方法 ===//
    public static AttributeDefinition GetAttribute(int id)
    {
        if (_attributes.TryGetValue(id, out var attr)) return attr;
        throw new Exception($"未定义的属性ID: {id}");
    }

    private static float GetBaseMultiplier(int attackerId, int defenderId)
    {
        // 未定义关系默认1倍
        bool found = _baseRelations.TryGetValue((attackerId, defenderId), out var value);
        //Debug.Log($"查询克制关系: {attackerId}→{defenderId}, 找到: {found}, 值: {value}");
        return found ? value : 1f;
    }

    //=== 初始化辅助方法 ===//
    private static void LoadAttributes(List<AttributeDefinition> attributes)
    {
        foreach (var attr in attributes)
        {
            ValidateAttributeComponents(attr);
            _attributes[attr.Id] = attr;
        }
    }

    private static void ValidateAttributeComponents(AttributeDefinition attr)
    {
        if (attr.IsDual)
        {
            if (attr.Components.Length != 2)
                throw new Exception($"双属性配置错误: {attr.Id} 需要2个组件");

            foreach (int compId in attr.Components)
            {
                if (!_attributes.ContainsKey(compId))
                    throw new Exception($"无效的组件ID: {compId} 在属性 {attr.Id} 中");
            }
        }
    }

    private static void LoadBaseRelations(List<BaseRelation> relations)
    {
        foreach (var relation in relations)
        {
            var key = (relation.AttackerId, relation.DefenderId);
            _baseRelations[key] = relation.Multiplier;
            //Debug.Log($"加载克制关系: {relation.AttackerId}→{relation.DefenderId} = {relation.Multiplier}");
        }
    }

    public static void PrintAllAttributeInfo()
    {
        foreach (KeyValuePair<int, AttributeDefinition> kvp in _attributes)
        {
            PrintAttributeInfo(kvp.Key);
        }
    }

    public static void PrintAttributeInfo(int id)
    {
        var attr = GetAttribute(id);
        Debug.Log($"属性ID: {attr.Id}\n" +
                  $"名称: {attr.Name}\n" +
                  $"组成: {string.Join(",", attr.Components)}");
    }

    public static void PrintAllRelationInfo()
    {
        foreach (KeyValuePair<(int, int), float> kvp in _baseRelations)
        {
            //Debug.Log("======");
            //Debug.Log(kvp.Key.Item1);
            //Debug.Log(kvp.Key.Item2);
            //Debug.Log("======");

            float attr=GetMultiplier(kvp.Key.Item1,kvp.Key.Item2);
            Debug.Log($"属性1ID: {kvp.Key.Item1}\n" +
                      $"属性2ID: {kvp.Key.Item2}\n" +
                      $"倍数: {attr}\n");
        }



    }



}