using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    // 池的存储，使用字典存储
    private Dictionary<string, Queue<GameObject>> pool;

    // 每个类型的物体是否需要预加载，以及预加载数量
    private Dictionary<string, int> preloadDict = new Dictionary<string, int>();

    // 每个池中最大数量，可以在Unity Inspector中设置
    [SerializeField]
    private int maxCount = int.MaxValue;
    public int MaxCount
    {
        get { return maxCount; }
        set
        {
            maxCount = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }


    // 全局对象池最大数量，可以在Unity Inspector中设置
    [SerializeField]
    private int maxPoolSize = int.MaxValue;
    public int MaxPoolSize
    {
        get { return maxPoolSize; }
        set
        {
            maxPoolSize = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }


    // 初始化池
    protected override void Init()
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
        // 如果池中没有对应类型的物体，则新建一个队列
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        // 如果池空了就创建新物体
        if (pool[go.name].Count == 0)
        {
            GameObject newObject = Instantiate(go, position, rotation);
            //确认名字一样，防止系统加一个(clone),或序号累加之类的
            newObject.name = go.name;
            return newObject;
        }
        // 从池中获取物体
        GameObject nextObject = pool[go.name].Dequeue();
        // 要先启动再设置属性，否则可能会被OnEnable重置
        nextObject.SetActive(true);
        nextObject.transform.position = position;
        nextObject.transform.rotation = rotation;
        return nextObject;
    }

    /// <summary>
    /// 把物体放回池里
    /// </summary>
    /// <param name="go">需要放回队列的物品</param>
    /// <param name="t">延迟执行的时间</param>
    public void PutObject(GameObject go, float t)
    {

        // 如果未初始化过 初始化池
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        // 如果池中已经有最大数量的物体，则销毁这个物体
        if (pool[go.name].Count >= MaxCount)
            Destroy(go, t);
        // 否则将物体加入池中
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
            newObject.name = go.name;
            newObject.SetActive(false);
            pool[go.name].Enqueue(newObject);
        }
    }

    public void Test()
    {
        print("hello");
    }



}
