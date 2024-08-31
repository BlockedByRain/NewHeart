using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ObjPoolTest : MonoBehaviour
{

    public GameObject testObject;
    // Start is called before the first frame update
    void Start()
    {
        //Ԥ��
        ObjectPool.Instance.Preload(testObject, 500);
    }

    //�޶���ز���
    public void TestOfNotOP()
    {
        StartCoroutine(CreateOfNotOP());
    }


    private IEnumerator CreateOfNotOP()
    {
        //ͳ��500֡����ʱ��
        float t = 0.0f;
        //ÿһ֡����һ�����󣬶�ʱ2����Զ�����
        for (int i = 0; i < 500; i++)
        {
            int x = Random.Range(-30, 30);
            int y = Random.Range(-30, 30);
            int z = Random.Range(-30, 30);
            GameObject newObject = Instantiate(testObject, new Vector3(x, y, z), Quaternion.identity);
            Destroy(newObject, 2.0f);

            yield return null;
            t += Time.deltaTime;
        }
        Debug.Log("�޶����500֡ʹ������:" + t);
    }

    //ʹ�ö���ز���
    public void TestOfOP()
    {

        StartCoroutine(CreateOfOP());
    }

    private IEnumerator CreateOfOP()
    {
        //ͳ��500֡����ʱ��
        float t = 0.0f;
        //ÿһ֡����һ�����󣬶�ʱ2����Զ�����
        for (int i = 0; i < 500; i++)
        {
            int x = Random.Range(-30, 30);
            int y = Random.Range(-30, 30);
            int z = Random.Range(-30, 30);
            GameObject newObject = ObjectPool.Instance.GetObject(testObject, new Vector3(x, y, z), Quaternion.identity);
            ObjectPool.Instance.PutObject(newObject, 2.0f);
            yield return null;
            t += Time.deltaTime;
        }
        Debug.Log("ʹ�ö����500֡ʹ������:" + t);
    }
}