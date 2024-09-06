using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffConfig", menuName = "Fight System/BuffConfig")]
public class BuffConfigSO : ScriptableObject
{
    //buffid
    public int buffId;
    //buff��
    public string buffName;
    //buff����
    public string buffDescribe;
    //buff����
    public BuffType buffType;
    //buffTag
    public string[] buffTags;
    //��Чʱ��
    public int effectiveTime;
    //�Ƿ�ɵ���
    public bool isStackable;
    //�����Ӳ���
    public int maxStack;
    //���ʱˢ������
    public AddTimeChangeEnum addTimeChange;
    //����ʱ����ˢ������
    public TimeOverStackChangeEnum timeOverStackChange;
    //Ч���б�
    public AbstractEffect[] buffEffects;



}
