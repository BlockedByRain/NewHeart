using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffConfig", menuName = "Fight System/BuffConfig")]
public class BuffConfigSO : ScriptableObject
{
    //buffid
    public int buffId;
    //buff名
    public string buffName;
    //buff描述
    public string buffDescribe;
    //buff类型
    public BuffType buffType;
    //buffTag
    public string[] buffTags;
    //生效时间
    public int effectiveTime;
    //是否可叠加
    public bool isStackable;
    //最大叠加层数
    public int maxStack;
    //添加时刷新类型
    public AddTimeChangeEnum addTimeChange;
    //结束时层数刷新类型
    public TimeOverStackChangeEnum timeOverStackChange;
    //效果列表
    public AbstractEffect[] buffEffects;



}
