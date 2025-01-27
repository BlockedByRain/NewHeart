using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class PersonalityDefinition
{
    [JsonProperty("id")] 
    public int Id;
    [JsonProperty("name")] 
    public string Name;
    [JsonProperty("physicalAttackMultiplier")] 
    public float PhysicalAttackMultiplier;
    [JsonProperty("specialAttackMultiplier")] 
    public float SpecialAttackMultiplier;
    [JsonProperty("physicalDefenseMultiplier")] 
    public float PhysicalDefenseMultiplier;
    [JsonProperty("specialDefenseMultiplier")] 
    public float SpecialDefenseMultiplier;
    [JsonProperty("speedMultiplier")] 
    public float SpeedMultiplier;
    [JsonProperty("hpMultiplier")] 
    public float HPMultiplier;
    [JsonProperty("desc")]
    public string Description;
}

// 配置文件结构
[Serializable]
public class PersonalityConfig
{
    [JsonProperty("personalities")] public List<PersonalityDefinition> Personalities;
}