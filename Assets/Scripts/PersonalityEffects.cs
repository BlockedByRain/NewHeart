using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

/// <summary>
/// �Ը�ö��
/// </summary>
public enum Personality
{
    ��ִ,
    ����,
    // ��Ӹ����Ը�
}

/// <summary>
/// �Ը�Ӱ��
/// </summary>
public static class PersonalityEffects
{

    // Ĭ��Ч��
    private static readonly PersonalityEffectsSixDimensions defaultEffect = new PersonalityEffectsSixDimensions(1, 1, 1, 1, 1, 1);


    // �����Ը�Ӱ��
    private static readonly Dictionary<Personality, PersonalityEffectsSixDimensions> personalityEffects = new Dictionary<Personality, PersonalityEffectsSixDimensions>()
    {
        //�﹥���ع���������ط����ٶȡ��������������Ը�Ӱ�죬�����һ���Ϊ1��
        { Personality.��ִ, new PersonalityEffectsSixDimensions(1.1f, 0.9f, 1f, 1f, 1f, 1f) },
        { Personality.����, new PersonalityEffectsSixDimensions(0.9f, 1.1f, 1f, 1f, 1f, 1f) },
        // ��Ӹ����Ը��Ӱ��
    };

    public static PersonalityEffectsSixDimensions GetEffect(Personality personality)
    {
        if (personalityEffects.TryGetValue(personality, out var effect))
        {
            return effect;
        }
        // ��ѯ������Ӧ�ӳ�Ĭ����Ӱ��
        return defaultEffect;
    }
}