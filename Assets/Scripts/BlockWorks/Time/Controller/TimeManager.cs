using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 该类演示用，一般情况下调用TimeController进行回溯前会有一部分检测逻辑
/// 你可以先将输入逻辑传给Manager，再由Manager调用Controller
/// </summary>
public class TimeManager : MonoSingleton<TimeManager>
{
    [LabelText("记录按键"), SerializeField]
    private KeyCode record = KeyCode.UpArrow;
    [LabelText("回放按键"), SerializeField]
    private KeyCode stepBack = KeyCode.LeftArrow;
    [LabelText("取消按键"), SerializeField]
    private KeyCode cancel = KeyCode.DownArrow;


    private void Update()
    {
        if (Input.GetKeyDown(record))
        {
            if (TimeController.Instance.CurrentState == TimeController.TIMESTATE.normal)
            {
                TimeController.Instance.RecordAll();
            }
        }

        if (Input.GetKeyDown(stepBack))
        {
            if (TimeController.Instance.CurrentState == TimeController.TIMESTATE.record)
                TimeController.Instance.RecallAll();
        }

        if (Input.GetKeyDown(cancel))
        {
            if (TimeController.Instance.CurrentState == TimeController.TIMESTATE.record)
                TimeController.Instance.ShutdownAll();
        }
    }
}

