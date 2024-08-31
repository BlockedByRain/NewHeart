using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    //池的存储
    //TODO 此处value使用自定义封装类型而不是单纯的queue更健全
    private Dictionary<string, Queue<GameObject>> pool;

    //每个池中最大数量
    //TODO 应该每个池设置每个池单独的数量
    private int maxCount = int.MaxValue;
    public int MaxCount
    {
        get { return maxCount; }
        set
        {
            maxCount = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    //初始化
    void Awake()
    {
        pool = new Dictionary<string, Queue<GameObject>>();

    }

    /// <summary>
    /// 从池中获取物体
    /// </summary>
    /// <param name="go">需要取得的物体</param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject go, Vector3 position, Quaternion rotation)
    {
        //如果未初始化过 初始化池
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        //如果池空了就创建新物体
        if (pool[go.name].Count == 0)
        {
            GameObject newObject = Instantiate(go, position, rotation);
            newObject.name = go.name;/*
            确认名字一样，防止系统加一个(clone),或序号累加之类的
            实际上为了更健全可以给每一个物体加一个key，防止对象的name一样但实际上不同
             */

            return newObject;
        }
        //从池中获取物体
        GameObject nextObject = pool[go.name].Dequeue();
        nextObject.SetActive(true);//要先启动再设置属性，否则可能会被OnEnable重置
        nextObject.transform.position = position;
        nextObject.transform.rotation = rotation;
        return nextObject;
    }

    /// <summary>
    /// 把物体放回池里
    /// </summary>
    /// <param name="go">需要放回队列的物品</param>
    /// <param name="t">延迟执行的时间</param>
    /// TODO 应该做个检查put的gameobject的池有没有创建过池
    public void PutObject(GameObject go, float t)
    {
        if (pool[go.name].Count >= MaxCount)
            Destroy(go, t);
        else
            StartCoroutine(ExecutePut(go, t));
    }

    private IEnumerator ExecutePut(GameObject go, float t)
    {
        yield return new WaitForSeconds(t);
        go.SetActive(false);
        pool[go.name].Enqueue(go);
    }

    /// <summary>
    /// 物体预热/预加载
    /// </summary>
    /// <param name="go">需要预热的物体</param>
    /// <param name="number">需要预热的数量</param>
    /// TODO 既然有预热用空间换时间 应该要做一个清理用时间换空间的功能
    public void Preload(GameObject go, int number)
    {
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        for (int i = 0; i < number; i++)
        {
            GameObject newObject = Instantiate(go);
            newObject.name = go.name;//确认名字一样，防止系统加一个(clone),或序号累加之类的
            newObject.SetActive(false);
            pool[go.name].Enqueue(newObject);
        }
    }

}