using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoSingleton<FightManager>
{
    //��ȡ��ս��Player
    public FightPlayer challenger;
    //��ȡ����ս��Player
    public FightPlayer challenged;


    void Start()
    {
        
    }

    /// <summary>
    /// ����ս��
    /// </summary>
    public void EnterFight()
    {
        //��ȡ˫������
        //��ʼ�����飨�����������Ļ�ã�


        StartCoroutine(UpdateRound());

    }

    /// <summary>
    /// �غ�ѭ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateRound()
    {
        //ͨ��ʤ���ж�����
        while (true)
        {
            //ս��

            Debug.Log("ִ�в���");
            //yield return null;
            yield return new WaitForSeconds(11f);

        }

    }

}
