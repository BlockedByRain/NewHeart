using UnityEditor;
using UnityEngine;
public static class CustomHierarchyMenu
{
    //[InitializeOnLoadMethod]
    //public static void CustomHierarchyDelete()
    //{
    //    EditorApplication.hierarchyWindowItemOnGUI = delegate (int instanceID, Rect selectionRect)
    //    {
    //        // ���δ�� Hierarchy ��ͼ��ѡ�����壬ֱ�ӷ���
    //        if (!Selection.activeObject || instanceID != Selection.activeObject.GetInstanceID())
    //        {
    //            return;
    //        }

    //        const float width = 50f;
    //        const float height = 18f;
    //        selectionRect.x += selectionRect.width - width;
    //        selectionRect.width = width;
    //        selectionRect.height = height;

    //        // ����¼�
    //        if (GUI.Button(selectionRect, "Delete"))
    //        {
    //            Debug.LogFormat("Delete : {0}", Selection.activeObject.name);
    //            Object.DestroyImmediate(Selection.activeObject);
    //        }
    //    };
    //}
   
}
