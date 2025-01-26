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

    List<Pet> challengerPetBag = null;
    List<Pet> challengedPetBag = null;

    void Start()
    {


    }

    /// <summary>
    /// 进入战斗
    /// </summary>
    public void EnterFight(FightPlayer player1, FightPlayer player2)
    {

        challenger = player1;
        challenged = player2;

        challengerPetBag = player1.petBag;
        challengedPetBag = player2.petBag;


        //设置出战
        challenger.activePetIndex = 0;
        challenged.activePetIndex = 0;

        //初始化精灵（如天生能力的获得）
        InitPetBag(challengerPetBag);
        InitPetBag(challengedPetBag);


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


            //todo 和战斗开始时一样需要先判空，之后考虑做封装，这里先注释掉
            //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleBeginningOfTheRoundEffect();
            //}
            //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleBeginningOfTheRoundEffect();
            //}


            //todo 这里已经拿到双方选择的技能了，考虑如何引入使用时无视强化弱化
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
            //Debug.Log($"剩余选择时间: {timer}");

            if (challenger.HasChosenAction() && challenged.HasChosenAction())
                break;

            yield return null;
        }

        if (!challenger.HasChosenAction())
        {
            challenger.DefaultAction();
            Debug.Log("challenger 执行了默认操作！");
        }
                        
        
        if (!challenged.HasChosenAction())
        {
            challenged.DefaultAction();
            Debug.Log("challenged 执行了默认操作！");
        }
            
    }

    // 执行玩家选择的操作
    IEnumerator ExecuteActions()
    {
        // 获取双方当前出战的精灵
        Pet challengerPet = challenger.GetActivePet();
        Pet challengedPet = challenged.GetActivePet();

        // 比较双方精灵的速度，速度高的先行动
        // todo 先判定选择的技能先制，后判定速度，后续考虑引入出手时节点的无视强弱化

        if (challengerPet.fightAbility.Speed > challengedPet.fightAbility.Speed)
        {
            // 挑战方先攻击
            challenger.ExecuteAction(challengerPet, challengedPet);
            
            
            //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}

            if (challengedPet.fightAbility.HP > 0)  // 如果防守方没有被击败
                challenged.ExecuteAction(challengedPet, challengerPet);
            
            
            //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}
        }
        else
        {
            // 防守方先攻击
            challenged.ExecuteAction(challengedPet, challengerPet);

            //todo 和战斗开始时一样需要先判空，之后考虑做封装，这里先注释掉
            //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}

            

            if (challengerPet.fightAbility.HP > 0)  // 如果挑战方没有被击败
                challenger.ExecuteAction(challengerPet, challengedPet);

            //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}

        }

        // 等待技能动画或效果播放完成
        yield return new WaitForSeconds(3f);
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
        //todo 同其他节点要做判空
        //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
        //{
        //    buffInfo.HandleEndOfTheRoundEffect();
        //}
        //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
        //{
        //    buffInfo.HandleEndOfTheRoundEffect();
        //}
    }


    /// <summary>
    /// 初始化精灵背包，主要是令魂印效果生效
    /// </summary>
    /// <param name="petBag"></param>
    private void InitPetBag(List<Pet> petBag)
    {
        List<Pet> challengerPetBag = petBag;
        // 检查 challengerPetBag 是否有效
        if (challengerPetBag != null)
        {
            // 遍历整个 challengerPetBag 列表
            foreach (var pet in challengerPetBag)
            {
                // 检查 pet 是否为空
                if (pet != null)
                {

                    //刷新能力
                    //考虑到魂印直接+100血+50速的类似效果，先计算能力然后初始化buff
                    //感觉有点奇怪，但是一时间想不到，留着看看

                    pet.RefreshFightAbility();

                    // 检查 pet 的 buffInfos 是否为空
                    if (pet.buffInfos != null)
                    {
                        foreach (var buffInfo in pet.buffInfos)
                        {
                            // 处理每个 buffInfo
                            buffInfo.HandleBeginningOfTheBattleEffect(pet);
                        }
                    }


                    
                }

            }
        }
    }



}
