using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonoSingleton<T> : SerializedMonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    private bool alive = true;

    public static bool IsAlive
    {
        get
        {
            if (_instance == null)
                return false;
            return _instance.alive;
        }
    }




    /// <summary>
    /// ����ʵ��
    /// </summary>
    public static T Instance
    {
        get
        {
            //������ʱֱ�ӷ��ؿ�
            if (!IsAlive)
            {
                return null;
            }

            // ����Ѿ����ڵ���ʵ������ֱ�ӷ���
            if (_instance != null)
                return _instance;

            // ��������ڵ���ʵ������Ѱ�ҳ������Ƿ��Ѿ���ͬ���͵ĵ���ʵ��
            _instance = FindObjectOfType<T>();
            if (_instance != null)
                return _instance;

            // ���������ͬ���͵ĵ���ʵ�����򴴽�һ���µ� GameObject�����ظõ�������������ø����Ϊ��������
            GameObject go = new GameObject(typeof(T).Name);
            _instance = go.AddComponent<T>();

            //������BlockWorks�£�����������Ϊ��������
            GameObject parent = GameObject.Find("BlockWorks");
            if (parent == null)
            {
                parent = new GameObject("BlockWorks");
                DontDestroyOnLoad(parent);
            }

            //��λ�������
            go.transform.SetParent(parent.transform);
            return _instance;
        }
    }

    private void Awake()
    {
        // �������ͬ���͵ĵ���ʵ���������ٵ�ǰ��ʵ������ֻ֤��һ��ʵ������
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ��֤�õ���ʵ�����ᱻ����
        DontDestroyOnLoad(gameObject);
        _instance = this as T;
        Init();
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    protected virtual void Init()
    {
    }

    /// <summary>
    /// ����
    /// </summary>
    protected virtual void OnDestroy()
    {
        alive = false;

        //����ɱ����д��������
        //if (_instance == this)
        //    _instance = null;
    }
}
