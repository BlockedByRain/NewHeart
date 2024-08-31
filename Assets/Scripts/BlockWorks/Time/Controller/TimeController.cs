using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.UI;

/// <summary>
/// 时间stepBack控制器
/// </summary>
public class TimeController : MonoSingleton<TimeController>
{

    public enum TIMESTATE
    {
        normal, stepBack, record
    }



    [LabelText("stepBack器列表"), SerializeField]
    private List<TimeStore> stores = new List<TimeStore>();

    private TimeStore playerStore;

    [LabelText("当前状态"), ReadOnly, SerializeField]
    private TIMESTATE state;

    public TIMESTATE CurrentState
    {
        get { return state; }
    }

    /// <summary>
    /// 为所有stepBack器预设容量,你可以测试内存占用后提高上限，因为动态扩容会1.5倍增加容量带来浪费,尽量不要在游戏时扩容
    /// </summary>
    [SerializeField, LabelText("记录上限"), DisableInPlayMode]
    private int capacity = 3000;

    public int Capacity
    {
        get { return capacity; }
    }

    [SerializeField, LabelText("当前记录数"), ProgressBar(0, "capacity"), ReadOnly]
    private int currentCount;

    /// <summary>
    /// record步长,因为使用Updaterecord会有更大误差，建议使用FixedUpdaterecord数据，FixedDeltaTime默认为0.02f
    /// </summary>
    [SerializeField, LabelText("记录步长"), Range(0.01f, 0.2f), Tooltip("每多少秒record一次"), DisableIf("state", TIMESTATE.record)]
    private float recordStep = 0.02f;
    [SerializeField, LabelText("回溯步长"), Range(0.01f, 0.2f), Tooltip("每多少秒stepBack一次"), DisableIf("state", TIMESTATE.stepBack)]
    private float recallStep = 0.02f;

    /// <summary>
    /// 当前record步长
    /// </summary>
    /// <value></value>
    public float RecordStep
    {
        get { return recordStep; }
    }

    private float timer;

    public event Action OnRecordStartEvent;

    /// <summary>
    /// stepBack开始事件
    /// </summary>
    public event Action OnRecallStartEvent;

    /// <summary>
    /// stepBack结束事件
    /// </summary>
    public event Action OnRecallEndEvent;

    /// <summary>
    /// record中事件
    /// </summary>
    public event Action OnRecordEvent;

    /// <summary>
    /// stepBack中事件
    /// </summary>
    public event Action OnRecallEvent;

    /// <summary>
    /// record数变更事件（适用于UI更新）
    /// </summary>
    public event Action<float> OnStepChangeEvent;

    /// <summary>
    /// 控制器状态变更事件（适用于多人游戏状态同步）
    /// </summary>
    public event Action<TIMESTATE> OnStateChangeEvent;


    [LabelText("固定更新"), SerializeField]
    private bool useFixedUpdate = true;

    /// <summary>
    /// 使用物理更新FixedUpdateMode
    /// </summary>
    /// <value></value>
    public bool UseFixedUpdate
    {
        get { return useFixedUpdate; }
    }


    public void Add(TimeStore store)
    {
        if (!stores.Contains(store))
        {
            stores.Add(store);
        }
    }
    public void Remove(TimeStore store)
    {
        if (stores.Contains(store))
        {
            stores.Remove(store);
        }
    }
    /// <summary>
    /// stepBack器开始record
    /// </summary>
    [Button("record"), HideInEditorMode, EnableIf("state",TIMESTATE.normal)]
    public void RecordAll()
    {
        timer = recordStep;
        UpdateState(TIMESTATE.record);
        OnRecordStartEvent?.Invoke();
        foreach (var store in stores)
        {
            store.Record();
        }

    }
    /// <summary>
    /// stepBack器开始stepBack
    /// </summary>
    [Button("stepBack"), HideInEditorMode, EnableIf("state",TIMESTATE.record)]
    public void RecallAll()
    {
        timer = 0;
        UpdateState(TIMESTATE.stepBack);
        OnRecallStartEvent?.Invoke();
        foreach (var store in stores)
        {
            store.Recall();
        }
        recallCount = currentCount;
    }
    /// <summary>
    /// 强制关闭所有stepBack器
    /// </summary>
    public void ShutdownAll()
    {
        state = TIMESTATE.normal;
        currentCount = 0;
        foreach (var store in stores)
        {
            store.ShutDown();
        }
    }
    private void Update()
    {
        if (!useFixedUpdate)
            UpdateStore(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (useFixedUpdate)
            UpdateStore(Time.fixedDeltaTime);
    }
    int recallCount;
    void UpdateStore(float deltaTime)
    {

        switch (state)
        {
            case TIMESTATE.normal:
                {
                    break;
                }
            case TIMESTATE.record:
                {
                    if (currentCount >= capacity)
                    {
                        //RecallAll();
                        break;
                    }
                    timer += deltaTime;
                    if (timer >= recordStep)
                    {
                        timer = 0;
                        currentCount += 1;
                        OnRecordEvent?.Invoke();
                        //调用record时刻
                        OnStepChangeEvent?.Invoke((float)currentCount / Capacity);
                    }
                    break;
                }
            case TIMESTATE.stepBack:
                {
                    if (currentCount == 0)//stepBack结束调用stepBack结束事件
                    {
                        OnRecallEndEvent?.Invoke();
                        UpdateState(TIMESTATE.normal);
                        break;
                    }
                    timer += Time.deltaTime;
                    if (timer >= recallStep)
                    {
                        timer = 0;
                        currentCount -= 1;
                        OnStepChangeEvent?.Invoke((float)currentCount / Capacity);
                        OnRecallEvent?.Invoke();//未结束则调用stepBack时刻
                    }
                    break;
                }
        }
    }
    void UpdateState(TIMESTATE newState)
    {
        state = newState;
        OnStateChangeEvent?.Invoke(state);
    }
}
