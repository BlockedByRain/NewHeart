using Newtonsoft.Json;
using System.Collections.Generic;
using System;

[Serializable]
public class AttributeDefinition
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("components")] public int[] Components = Array.Empty<int>();

    public bool IsSingle => Components.Length == 0;
    public bool IsDual => Components.Length == 2;
}

// 基础克制关系定义
[Serializable]
public class BaseRelation
{
    [JsonProperty("attackerId")] public int AttackerId;
    [JsonProperty("defenderId")] public int DefenderId;
    [JsonProperty("multiplier")] public float Multiplier;
}

// 配置文件结构
[Serializable]
public class AttributeConfig
{
    [JsonProperty("attributes")] public List<AttributeDefinition> Attributes;
    [JsonProperty("relations")] public List<BaseRelation> Relations;
}