using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffConfig", menuName = "Buff System/BuffConfig")]
public class BuffConfigSO : ScriptableObject
{
    public int id;
    public string buffName;
    public string description;
    public BuffType buffType;
    public string[] buffTags;
    public int effectiveTime; // 持续时间
    public bool isStackable;   // 是否可叠加
    public int maxStack;       // 最大叠加层数
    public AddTimeChangeEnum addTimeChange;  // 添加时的刷新类型
    public TimeOverStackChangeEnum timeOverStackChange;  // 结束时的层数刷新类型
    public BuffEffect[] buffEffects; // Buff 效果列表
}
