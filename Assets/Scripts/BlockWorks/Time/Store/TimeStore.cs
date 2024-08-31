using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TimeStore : MonoBehaviour
{
    [SerializeField, LabelText("锁定回溯器")]
    protected bool locked;
    /// <summary>
    /// 回溯栈
    /// </summary>
    protected List<TransformStep> list;

    protected virtual void Start()
    {

        TimeController.Instance.Add(this);

        list = new List<TransformStep>(TimeController.Instance.Capacity);

    }

    protected virtual void OnDestroy()
    {

        TimeController.Instance.Remove(this);
        TimeController.Instance.OnRecallEndEvent -= TimeStoreOver;
        TimeController.Instance.OnRecallEvent -= RecallTick;
        TimeController.Instance.OnRecordEvent -= RecordTick;

        list.Clear();
    }

    /// <summary>
    /// 锁定回溯器
    /// </summary>
    /// <param name="state"></param>
    public void LockTimeStore(bool state)
    {
        this.locked = state;
    }

    /// <summary>
    /// 开始记录
    /// </summary>
    public virtual void Record()
    {
        list.Clear();
        if (locked)
            return;
        TimeController.Instance.OnRecordEvent += RecordTick;
    }

    /// <summary>
    /// 开始回溯
    /// </summary>
    public virtual void Recall()
    {
        TimeController.Instance.OnRecordEvent -= RecordTick;
        if (locked)
            return;
        TimeController.Instance.OnRecallEvent += RecallTick;
        TimeController.Instance.OnRecallEndEvent += TimeStoreOver;
    }

    /// <summary>
    /// 终止回溯和记录
    /// </summary>
    public virtual void TimeStoreOver()
    {
        list.Clear();
        TimeController.Instance.OnRecallEndEvent -= TimeStoreOver;
        TimeController.Instance.OnRecallEvent -= RecallTick;
        TimeController.Instance.OnRecordEvent -= RecordTick;
    }

    /// <summary>
    /// 强制关闭回溯器
    /// </summary>
    public virtual void ShutDown()
    {
        TimeStoreOver();
    }

    /// <summary>
    /// 记录时刻
    /// </summary>
    public virtual void RecordTick()
    {

        TransformStep newStep = new TransformStep();
        newStep.position = transform.position;
        newStep.rotation = transform.rotation;
        list.Insert(0, newStep);

    }

    /// <summary>
    /// 回溯时刻
    /// </summary>
    public virtual void RecallTick()
    {
        if (list.Count == 0)
            return;
        var oldStep = list[0];
        transform.position = oldStep.position;
        transform.rotation = oldStep.rotation;
        list.RemoveAt(0);
    }


}

