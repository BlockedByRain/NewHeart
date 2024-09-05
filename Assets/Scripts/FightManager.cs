using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoSingleton<FightManager>
{
    //获取挑战方Player
    public FightPlayer challenger;
    //获取被挑战方Player
    public FightPlayer challenged;
    //倒计时
    public float countdownTime = 10f;  
    private bool battleOngoing = true;


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
        while (battleOngoing)
        {
            // 开始倒计时，让双方玩家选择行动
            yield return StartCoroutine(PlayerActionSelection());

            // 根据速度决定行动顺序并执行
            yield return StartCoroutine(ExecuteActions());

            // 检查是否有玩家的精灵全部阵亡
            if (challenger.AreAllPetsDefeated() || challenged.AreAllPetsDefeated())
            {
                EndBattle();
                yield break;
            }

            EndRound();

        }

    }


    private IEnumerator PlayerActionSelection()
    {
        float timer = countdownTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log($"剩余选择时间: {timer}");

            if (challenger.HasChosenAction() && challenged.HasChosenAction())
                break;

            yield return null;
        }

        if (!challenger.HasChosenAction())
            challenger.DefaultAction();
        if (!challenged.HasChosenAction())
            challenged.DefaultAction();
    }

    // 执行玩家选择的操作
    IEnumerator ExecuteActions()
    {
        // 获取双方当前出战的精灵
        Pet challengerPet = challenger.GetActivePet();
        Pet challengedPet = challenged.GetActivePet();

        // 比较双方精灵的速度，速度高的先行动
        if (challengerPet.ability.Speed > challengedPet.ability.Speed)
        {
            // 挑战方先攻击
            challenger.ExecuteAction(challengedPet);
            if (challengedPet.fightAbility.HP > 0)  // 如果防守方没有被击败
                challenged.ExecuteAction(challengerPet);
        }
        else
        {
            // 防守方先攻击
            challenged.ExecuteAction(challengerPet);
            if (challengerPet.fightAbility.HP > 0)  // 如果挑战方没有被击败
                challenger.ExecuteAction(challengedPet);
        }

        // 等待技能动画或效果播放完成
        yield return new WaitForSeconds(1f);
    }

    // 结束战斗
    void EndBattle()
    {
        battleOngoing = false;

        if (challenger.AreAllPetsDefeated())
        {
            Debug.Log("挑战方失败，防守方获胜！");
        }
        else if (challenged.AreAllPetsDefeated())
        {
            Debug.Log("防守方失败，挑战方获胜！");
        }
    }


    // 结束回合
    void EndRound()
    {

    }


}
