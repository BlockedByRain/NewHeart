using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    // �صĴ洢��ʹ���ֵ�洢
    private Dictionary<string, Queue<GameObject>> pool;

    // ÿ�����͵������Ƿ���ҪԤ���أ��Լ�Ԥ��������
    private Dictionary<string, int> preloadDict = new Dictionary<string, int>();

    // ÿ���������������������Unity Inspector������
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


    // ȫ�ֶ�������������������Unity Inspector������
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


    // ��ʼ����
    protected override void Init()
    {
        pool = new Dictionary<string, Queue<GameObject>>();
    }

    /// <summary>
    /// �ӳ��л�ȡ����
    /// </summary>
    /// <param name="go">��Ҫȡ�õ�����</param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject go, Vector3 position, Quaternion rotation)
    {
        // �������û�ж�Ӧ���͵����壬���½�һ������
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        // ����ؿ��˾ʹ���������
        if (pool[go.name].Count == 0)
        {
            GameObject newObject = Instantiate(go, position, rotation);
            //ȷ������һ������ֹϵͳ��һ��(clone),������ۼ�֮���
            newObject.name = go.name;
            return newObject;
        }
        // �ӳ��л�ȡ����
        GameObject nextObject = pool[go.name].Dequeue();
        // Ҫ���������������ԣ�������ܻᱻOnEnable����
        nextObject.SetActive(true);
        nextObject.transform.position = position;
        nextObject.transform.rotation = rotation;
        return nextObject;
    }

    /// <summary>
    /// ������Żس���
    /// </summary>
    /// <param name="go">��Ҫ�Żض��е���Ʒ</param>
    /// <param name="t">�ӳ�ִ�е�ʱ��</param>
    public void PutObject(GameObject go, float t)
    {

        // ���δ��ʼ���� ��ʼ����
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        // ��������Ѿ���������������壬�������������
        if (pool[go.name].Count >= MaxCount)
            Destroy(go, t);
        // ��������������
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
    /// ����Ԥ��/Ԥ����
    /// </summary>
    /// <param name="go">��ҪԤ�ȵ�����</param>
    /// <param name="number">��ҪԤ�ȵ�����</param>
    /// TODO ��Ȼ��Ԥ���ÿռ任ʱ�� Ӧ��Ҫ��һ��������ʱ�任�ռ�Ĺ���
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
