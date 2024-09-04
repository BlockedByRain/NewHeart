using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoSingleton<FightManager>
{
    //获取挑战方Player
    public FightPlayer challenger;
    //获取被挑战方Player
    public FightPlayer challenged;


    void Start()
    {
        
    }

    /// <summary>
    /// 进入战斗
    /// </summary>
    public void EnterFight()
    {
        //获取双方背包
        //初始化精灵（如天生能力的获得）


        StartCoroutine(UpdateRound());

    }

    /// <summary>
    /// 回合循环
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateRound()
    {
        //通过胜负判断跳出
        while (true)
        {
            //战斗

            Debug.Log("执行测试");
            //yield return null;
            yield return new WaitForSeconds(11f);

        }

    }

}
