using UnityEngine;

[CreateAssetMenu(fileName = "PersonalityEffectConfig", menuName = "Fight System/Personality Effects")]
public class PersonalityEffectConfig : ScriptableObject
{
    [System.Serializable]
    public class EffectEntry
    {
        public Personality personality;
        public float physicalAttackMultiplier = 1f;
        public float specialAttackMultiplier = 1f;
        public float physicalDefenseMultiplier = 1f;
        public float specialDefenseMultiplier = 1f;
        public float speedMultiplier = 1f;
        public float hpMultiplier = 1f;
    }

    public EffectEntry[] effects;
}