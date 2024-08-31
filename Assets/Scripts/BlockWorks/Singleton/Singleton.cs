using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;


//���͵���
public abstract class Singleton<T> where T : class, new()
{
    private static T _instance;

    /// <summary>
    /// ����ʵ��
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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {
    }

    /// <summary>
    /// �ͷ� Release() ��������һ����̬���������ڽ�����ʵ����ա�
    /// </summary>
    public static void Release()
    {
        _instance = null;
    }

    /// <summary>
    /// ���� Dispose() ������һ�����󷽷������ľ���ʵ���ɼ̳� Singleton ��������ʵ�֡�
    /// </summary>
    public abstract void Dispose();


}
