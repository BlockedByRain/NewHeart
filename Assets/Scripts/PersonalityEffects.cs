using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

// �����Ը�ö��
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

    // �����Ը�Ӱ��
    private static readonly Dictionary<Personality, SixDimensionsValue> personalityEffects = new Dictionary<Personality, SixDimensionsValue>()
    {
        //�﹥���ع���������ط����ٶȡ��������������Ը�Ӱ�죬�����һ���Ϊ1��
        { Personality.��ִ, new SixDimensionsValue(1.1f, 0.9f, 1f, 1f, 1f, 1f) },
        { Personality.����, new SixDimensionsValue(0.9f, 1.1f, 1f, 1f, 1f, 1f) },
        // ��Ӹ����Ը��Ӱ��
    };

    public static SixDimensionsValue GetEffect(Personality personality)
    {
        if (personalityEffects.TryGetValue(personality, out var effect))
        {
            return effect;
        }
        // ��ѯ������Ӧ�ӳ�Ĭ����Ӱ��
        return new SixDimensionsValue(1, 1, 1, 1, 1, 1); 
    }
}