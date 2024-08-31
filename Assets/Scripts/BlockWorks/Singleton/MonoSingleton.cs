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
    /// 单例实例
    /// </summary>
    public static T Instance
    {
        get
        {
            //不启用时直接返回空
            if (!IsAlive)
            {
                return null;
            }

            // 如果已经存在单例实例，则直接返回
            if (_instance != null)
                return _instance;

            // 如果不存在单例实例，则寻找场景中是否已经有同类型的单例实例
            _instance = FindObjectOfType<T>();
            if (_instance != null)
                return _instance;

            // 如果不存在同类型的单例实例，则创建一个新的 GameObject，挂载该单例组件，并设置该组件为不被销毁
            GameObject go = new GameObject(typeof(T).Name);
            _instance = go.AddComponent<T>();

            //挂载在BlockWorks下，并将其设置为不可销毁
            GameObject parent = GameObject.Find("BlockWorks");
            if (parent == null)
            {
                parent = new GameObject("BlockWorks");
                DontDestroyOnLoad(parent);
            }

            //定位方便管理
            go.transform.SetParent(parent.transform);
            return _instance;
        }
    }

    private void Awake()
    {
        // 如果存在同类型的单例实例，则销毁当前的实例，保证只有一个实例存在
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 保证该单例实例不会被销毁
        DontDestroyOnLoad(gameObject);
        _instance = this as T;
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Init()
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    protected virtual void OnDestroy()
    {
        alive = false;

        //会造成报错的写法，弃用
        //if (_instance == this)
        //    _instance = null;
    }
}
