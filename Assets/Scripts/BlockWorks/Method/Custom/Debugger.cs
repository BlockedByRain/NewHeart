using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger
{
    public static void LogColorful(object msg,string color)
    {
        Debug.Log($"<color=#{color}>{msg}</color>");
    }
}
