using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;


//泛型单例
public abstract class Singleton<T> where T : class, new()
{
    private static T _instance;

    /// <summary>
    /// 单例实例
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            _instance = Activator.CreateInstance<T>();
            if (_instance is Singleton<T> singleton)
            {
                singleton.Init();
            }

            return _instance;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
    }

    /// <summary>
    /// 释放 Release() 方法则是一个静态方法，用于将单例实例清空。
    /// </summary>
    public static void Release()
    {
        _instance = null;
    }

    /// <summary>
    /// 销毁 Dispose() 方法是一个抽象方法，它的具体实现由继承 Singleton 的子类来实现。
    /// </summary>
    public abstract void Dispose();


}
