using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff处理类
/// </summary>
public class BuffHandler : MonoBehaviour
{
    // 正在有效的buff
    List<BuffInfo> buffs = new List<BuffInfo>();
    // 即将移除的buff
    List<BuffInfo> removeBuffs = new List<BuffInfo>();
    // 即将添加的buff
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
    /// 处理待添加的buff
    /// </summary>
    void HandleAddBuffs()
    {
        if (addBuffs.Count == 0) return;
        foreach (var buffInfo in addBuffs)
        {
            //查找已有的Buff，避免重复添加
            var find = buffs.Find(x => x.buffConfig.buffId == buffInfo.buffConfig.buffId);
            if (find == null)
            {
                buffs.Add(buffInfo);
            }
            else
            {
                //根据配置处理添加时间的方式
                switch (find.buffConfig.AddTimeChange)
                {
                    case AddTimeChangeEnum.Add:
                        find.effectiveTime += buffInfo.effectiveTime;
                        break;
                    case AddTimeChangeEnum.Refresh:
                        find.effectiveTime = buffInfo.effectiveTime;
                        break;
                }
                //检查Buff是否可以叠加
                if (!find.buffConfig.IsStack || find.curStack >= find.buffConfig.MaxStack)
                    continue;
                find.curStack++;
            }
        }
        addBuffs.Clear();

    }

}
