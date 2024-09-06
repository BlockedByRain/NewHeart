using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffConfig", menuName = "Buff System/BuffConfig")]
public class BuffConfigSO : ScriptableObject
{
    public int id;
    public string buffName;
    public string description;
    public BuffType buffType;
    public string[] buffTags;
    public int effectiveTime; // ����ʱ��
    public bool isStackable;   // �Ƿ�ɵ���
    public int maxStack;       // �����Ӳ���
    public AddTimeChangeEnum addTimeChange;  // ���ʱ��ˢ������
    public TimeOverStackChangeEnum timeOverStackChange;  // ����ʱ�Ĳ���ˢ������
    public BuffEffect[] buffEffects; // Buff Ч���б�
}
