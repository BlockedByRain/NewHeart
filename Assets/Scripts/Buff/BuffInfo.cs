using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// buff���е�������
/// </summary>
public class BuffInfo
{
    public BuffConfig BuffConfig;
    public GameObject creator;
    public GameObject target;
    public int curDuration;
    public int curStack;
    public int maxStack;

    public BuffInfo(BuffConfig buffConfig, GameObject target, GameObject creator = null)
    {
        buffConfig = buffConfig;
        Target = target;
        Creator = creator;
        CurDuration = buffConfig.Duration;
        CurStack = 1;
    }



    //buff�ĸ����߼�
    public void OnUpdate()
    {

    }


    public void HandleCreateEffect()
    {
        if (createBuffEffects == null) return;
        foreach (var buffEffect in createBuffEffects)
        {
            foreach (var effect in buffEffect.Effects)
            {
                effect.Apply(this);
            }
        }
    }

    public void HandleRemoveEffect()
    {
        if (removeBuffEffects == null) return;
        foreach (var buffEffect in removeBuffEffects)
        {
            foreach (var effect in buffEffect.Effects)
            {
                effect.Apply(this);
            }
        }
    }



}


/// <summary>
/// buff������
/// </summary>
public class BuffConfig
{
    public int BuffId;
    public int Priority;
    public bool IsPermanent;
    public float Duration;
    public bool IsStack;
    public int MaxStack;
    public AddTimeChangeEnum AddTimeChange;
    public TimeOverStackChangeEnum TimeOverStackChange;
    public BuffEffect[] BuffEffects;
}

/// <summary>
/// buffЧ��������
/// </summary>
public class BuffEffect
{
    //����ʱ��
    public float PeriodTime;
    //����Ч��
    public AbstractEffect[] Effects;
}

public enum AddTimeChangeEnum
{
    [LabelText("ˢ��ʱ��")]
    Refresh,
    [LabelText("����ʱ��")]
    Add,
}

public enum TimeOverStackChangeEnum
{
    [LabelText("�������")]
    Clear,
    [LabelText("�ݼ�����")]
    Reduce
}