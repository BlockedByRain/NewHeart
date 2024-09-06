using UnityEngine;
/// <summary>
/// Ч�����岿�֣����ܻ�buff����Ч������߼�
/// </summary>
public interface IEffect
{
    public void Apply(object target);
}

public abstract class AbstractEffect : IEffect
{
    public abstract void Apply(object target);
}


//public interface IEffect
//{
//    public void Apply(object user, object target);
//}

//public abstract class AbstractEffect : IEffect
//{
//    public abstract void Apply(object user, object target);
//}

/// <summary>
/// ʾ������ӡָ���ַ���
/// </summary>
public class PrintEffect : AbstractEffect
{
    public string Message;
    public override void Apply(object target)
    {
        Debug.Log(Message);
    }
}