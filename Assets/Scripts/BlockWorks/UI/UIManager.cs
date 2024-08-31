using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class UIManager:MonoSingleton<UIManager>
{
    // 根节点名
    private readonly string rootName = "UICanvas";
    // 预制体路径
    private readonly string panelPath = "Prefab/Panel/";

    private Transform _uiRoot;
    // 路径配置字典
    private Dictionary<string, string> pathDict;
    // 预制件缓存字典
    private Dictionary<string, GameObject> prefabDict;
    // 已打开界面的缓存字典
    public Dictionary<string, BasePanel> panelDict;


    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                if (GameObject.Find(rootName))
                {
                    _uiRoot = GameObject.Find(rootName).transform;
                }
                else
                {
                    _uiRoot = new GameObject(rootName).transform;
                }
                return _uiRoot;
            };
            return _uiRoot;
        }
    }

    protected override void Init()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();

        pathDict = new Dictionary<string, string>()
        {
            {UIConst.AllCardPanel, "Menu/AllCardPanel"},

        };
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel;
        // 检查是否已打开
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.LogError("界面已打开: " + name);
            return null;
        }

        // 检查路径是否配置
        string path;
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.LogError("界面名称错误，或未配置路径: " + name);
            return null;
        }

        // 使用缓存预制件
        GameObject panelPrefab;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = panelPath + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }

        // 打开界面
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        BasePanel panel;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.LogError("界面未打开: " + name);
            return false;
        }

        panel.ClosePanel();
        // panelDict.Remove(name);
        return true;
    }

}

public class UIConst
{
    public const string AllCardPanel = "AllCardPanel";

}
