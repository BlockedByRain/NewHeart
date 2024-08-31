using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class UIManager:MonoSingleton<UIManager>
{
    // ���ڵ���
    private readonly string rootName = "UICanvas";
    // Ԥ����·��
    private readonly string panelPath = "Prefab/Panel/";

    private Transform _uiRoot;
    // ·�������ֵ�
    private Dictionary<string, string> pathDict;
    // Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefabDict;
    // �Ѵ򿪽���Ļ����ֵ�
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
        // ����Ƿ��Ѵ�
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.LogError("�����Ѵ�: " + name);
            return null;
        }

        // ���·���Ƿ�����
        string path;
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.LogError("�������ƴ��󣬻�δ����·��: " + name);
            return null;
        }

        // ʹ�û���Ԥ�Ƽ�
        GameObject panelPrefab;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = panelPath + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }

        // �򿪽���
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
            Debug.LogError("����δ��: " + name);
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
