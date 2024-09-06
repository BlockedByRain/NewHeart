using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff������
/// </summary>
public class BuffHandler : MonoBehaviour
{
    // ������Ч��buff
    List<BuffInfo> buffs = new List<BuffInfo>();
    // �����Ƴ���buff
    List<BuffInfo> removeBuffs = new List<BuffInfo>();
    // ������ӵ�buff
    List<BuffInfo> addBuffs = new List<BuffInfo>();


    public void AddBuff(BuffInfo buffInfo)
    {
        addBuffs.Add(buffInfo);
    }

    public void RemoveBuff(BuffInfo buffInfo)
    {
        removeBuffs.Add(buffInfo);
    }


    void Update()
    {
        HandleAddBuffs();
        HandleBuffs();
        HandleRemoveBuffs();
    }

    void HandleBuffs()
    {
        if (buffs.Count == 0) return;
        foreach (var buffInfo in buffs)
        {
            RemoveBuff(buffInfo);
        }
    }
    void HandleRemoveBuffs()
    {
        if (removeBuffs.Count == 0) return;
        foreach (var buffInfo in removeBuffs)
        {
            if (!buffInfo.buffConfig.IsStack || buffInfo.curStack == 1)
            {
                buffs.Remove(buffInfo);
            }
            else
            {
                switch (buffInfo.buffConfig.TimeOverStackChange)
                {
                    case TimeOverStackChangeEnum.Clear:
                        buffs.Remove(buffInfo);
                        break;
                    case TimeOverStackChangeEnum.Reduce:
                        buffInfo.curStack--;
                        buffInfo.effectiveTime = buffInfo.buffConfig.effectiveTime;
                        break;
                }
            }
        }
        removeBuffs.Clear();
    }

    /// <summary>
    /// �������ӵ�buff
    /// </summary>
    void HandleAddBuffs()
    {
        if (addBuffs.Count == 0) return;
        foreach (var buffInfo in addBuffs)
        {
            //�������е�Buff�������ظ����
            var find = buffs.Find(x => x.buffConfig.buffId == buffInfo.buffConfig.buffId);
            if (find == null)
            {
                buffs.Add(buffInfo);
            }
            else
            {
                //�������ô������ʱ��ķ�ʽ
                switch (find.buffConfig.AddTimeChange)
                {
                    case AddTimeChangeEnum.Add:
                        find.effectiveTime += buffInfo.effectiveTime;
                        break;
                    case AddTimeChangeEnum.Refresh:
                        find.effectiveTime = buffInfo.effectiveTime;
                        break;
                }
                //���Buff�Ƿ���Ե���
                if (!find.buffConfig.IsStack || find.curStack >= find.buffConfig.MaxStack)
                    continue;
                find.curStack++;
            }
        }
        addBuffs.Clear();

    }

}
