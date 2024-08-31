using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MethodExtension 
{

    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T ret = go.GetComponent<T>();
        if (null == ret)
            ret = go.AddComponent<T>();
        return ret;
    }

    public static float GetAnlgeFromPoint(this Vector2 point, Vector2 origin)
    {
        Vector2 dir = new Vector2(point.x - origin.x, point.y - origin.y).normalized;
        float angle = Mathf.Acos(dir.y) * Mathf.Rad2Deg;
        return point.x > origin.x ? angle : 360.0f - angle;
    }

}
