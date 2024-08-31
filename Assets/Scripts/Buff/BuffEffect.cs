using System.Collections.Generic;
using UnityEngine;

// BuffEffect 是所有 Buff 类型都需要继承的基类
public abstract class BuffEffect:ScriptableObject
{
    // 抽象方法，需要在子类中实现具体的 Buff 效果
    public abstract void Apply(GameObject target);

    // 抽象方法，需要在子类中实现具体的 Buff 消失效果
    public abstract void Clear(GameObject target);
}