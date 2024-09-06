using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// buff���е�������
/// </summary>
public class BuffInfo
{
    public BuffConfig buffConfig;
    public GameObject creator;
    public GameObject target;
    public int effectiveTime;
    public int curStack;
    public int maxStack;

    //ս����ʼʱbuffЧ������
    List<BuffEffect> beginningOfTheBattleBuffEffects;
    //�غϿ�ʼʱbuffЧ������
    List<BuffEffect> beginningOfTheRoundBuffEffects;
    //ʹ�ü���ʱbuffЧ������
    List<BuffEffect> usingSkillBuffEffects;
    //�غϽ���ʱbuffЧ������
    List<BuffEffect> endOfTheRound;


    public BuffInfo(BuffConfig buffConfig, GameObject target, GameObject creator = null)
    {
        this.buffConfig = buffConfig;
        this.target = target;
        this.creator = creator;



        //����buff���ã���buffЧ������
        foreach (var buffConfigBuffEffect in buffConfig.BuffEffects)
        {
            switch (buffConfigBuffEffect.triggerTiming)
            {
                case TriggerTimingEnum.BeginningOfTheBattle:
                    if (beginningOfTheBattleBuffEffects == null) beginningOfTheBattleBuffEffects = new List<BuffEffect>();
                    beginningOfTheBattleBuffEffects.Add(buffConfigBuffEffect);
                    break;
                case TriggerTimingEnum.BeginningOfTheRound:
                    if (beginningOfTheRoundBuffEffects == null) beginningOfTheRoundBuffEffects = new List<BuffEffect>();
                    beginningOfTheRoundBuffEffects.Add(buffConfigBuffEffect);
                    break;
                case TriggerTimingEnum.UsingSkill:
                    if (usingSkillBuffEffects == null) usingSkillBuffEffects = new List<BuffEffect>();
                    usingSkillBuffEffects.Add(buffConfigBuffEffect);
                    break;
                case TriggerTimingEnum.EndOfTheRound:
                    if (endOfTheRound == null) endOfTheRound = new List<BuffEffect>();
                    endOfTheRound.Add(buffConfigBuffEffect);
                    break;
            }
            
        }

        this.effectiveTime = buffConfig.effectiveTime;
        curStack = 1;
    }



    //buff�ĸ����߼�
    public void OnUpdate()
    {

    }

    //ս����ʼʱ����
    public void HandleBeginningOfTheBattleEffect()
    {
        if (beginningOfTheBattleBuffEffects == null) return;
        foreach (var buffEffect in beginningOfTheBattleBuffEffects)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(this);
            }
        }
    }

    //�غϿ�ʼʱ����
    public void HandleBeginningOfTheRoundEffect()
    {
        if (beginningOfTheRoundBuffEffects == null) return;
        foreach (var buffEffect in beginningOfTheRoundBuffEffects)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(this);
            }
        }
    }

    //ʹ�ü���ʱ����
    public void HandleUsingSkilleEffect()
    {
        if (usingSkillBuffEffects == null) return;
        foreach (var buffEffect in usingSkillBuffEffects)
        {
            foreach (var effect in buffEffect.effects)
            {
                effect.Apply(this);
            }
        }
    }

    //�غϽ���ʱ����
    public void HandleEndOfTheRoundEffect()
    {
        if (endOfTheRound == null) return;
        foreach (var buffEffect in endOfTheRound)
        {
            foreach (var effect in buffEffect.effects)
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
    //buffid
    public int id;
    //buff��
    public string name;
    //buff����
    public string describe;
    //buff����
    public BuffType buffType;
    //��Чʱ��
    public int effectiveTime;
    //�Ƿ�ɵ���
    public bool IsStack;
    //�����Ӳ���
    public int MaxStack;
    //���ʱˢ������
    public AddTimeChangeEnum AddTimeChange;
    //����ʱ����ˢ������
    public TimeOverStackChangeEnum TimeOverStackChange;
    //Ч���б�
    public BuffEffect[] BuffEffects;
}

/// <summary>
/// buffЧ��������
/// </summary>
public class BuffEffect
{
    //����
    public BuffType buffType;
    //��Чʱ��
    public int effectiveTime;
    //��Ч�ڵ�
    public TriggerTimingEnum triggerTiming;
    //����Ч��
    public AbstractEffect[] effects;
}

public enum BuffType
{
    //������
    Permanent,
    //�غ���
    Round,
    //������
    Frequency,
}

/// <summary>
/// ���ʱˢ������
/// </summary>
public enum AddTimeChangeEnum
{
    [LabelText("ˢ��ʱ��")]
    Refresh,
    [LabelText("����ʱ��")]
    Add,
}

/// <summary>
/// ����ʱ����ˢ������
/// </summary>
public enum TimeOverStackChangeEnum
{
    [LabelText("�������")]
    Clear,
    [LabelText("�ݼ�����")]
    Reduce
}


/// <summary>
/// ����ʱ��
/// </summary>
public enum TriggerTimingEnum
{
    [LabelText("ս����ʼʱ")]
    BeginningOfTheBattle,
    [LabelText("�غϿ�ʼʱ")]
    BeginningOfTheRound,
    [LabelText("ʹ�ü���ʱ")]
    UsingSkill,
    [LabelText("�غϽ���ʱ")]
    EndOfTheRound,

}