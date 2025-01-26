using System;
using Newtonsoft.Json;

[Serializable]
public class PersonalityConfig
{
    [JsonProperty("ID")]
    public int PersonalityId;

    [JsonProperty("Name")]
    public string PersonalityName;

    [JsonProperty("pa")]
    public float pa;

    [JsonProperty("sa")]
    public float sa;

    [JsonProperty("pd")]
    public float pd;

    [JsonProperty("sd")]
    public float sd;

    [JsonProperty("sp")]
    public float sp;

    [JsonProperty("hp")]
    public float hp;

}




