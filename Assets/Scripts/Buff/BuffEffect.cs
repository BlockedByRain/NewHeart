using System.Collections.Generic;
using UnityEngine;

// BuffEffect ������ Buff ���Ͷ���Ҫ�̳еĻ���
public abstract class BuffEffect:ScriptableObject
{
    // ���󷽷�����Ҫ��������ʵ�־���� Buff Ч��
    public abstract void Apply(GameObject target);

    // ���󷽷�����Ҫ��������ʵ�־���� Buff ��ʧЧ��
    public abstract void Clear(GameObject target);
}