using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoSingleton<FightManager>
{
    //��ȡ��ս��Player
    public FightPlayer challenger;
    //��ȡ����ս��Player
    public FightPlayer challenged;
    //����ʱ
    public float countdownTime = 10f;

    private bool battleOngoing = true;

    List<Pet> challengerPetBag = null;
    List<Pet> challengedPetBag = null;

    void Start()
    {


    }

    /// <summary>
    /// ����ս��
    /// </summary>
    public void EnterFight(FightPlayer player1, FightPlayer player2)
    {

        challenger = player1;
        challenged = player2;

        challengerPetBag = player1.petBag;
        challengedPetBag = player2.petBag;


        //���ó�ս
        challenger.activePetIndex = 0;
        challenged.activePetIndex = 0;

        //��ʼ�����飨�����������Ļ�ã�
        InitPetBag(challengerPetBag);
        InitPetBag(challengedPetBag);


        StartCoroutine(UpdateRound());

    }

    /// <summary>
    /// �غ�ѭ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateRound()
    {
        while (battleOngoing)
        {
            // ��ʼ����ʱ����˫�����ѡ���ж�
            yield return StartCoroutine(PlayerActionSelection());


            //todo ��ս����ʼʱһ����Ҫ���пգ�֮��������װ��������ע�͵�
            //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleBeginningOfTheRoundEffect();
            //}
            //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleBeginningOfTheRoundEffect();
            //}


            //todo �����Ѿ��õ�˫��ѡ��ļ����ˣ������������ʹ��ʱ����ǿ������

            // �����ٶȾ����ж�˳��ִ��
            yield return StartCoroutine(ExecuteActions());

            // ����Ƿ�����ҵľ���ȫ������
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
            //Debug.Log($"ʣ��ѡ��ʱ��: {timer}");

            if (challenger.HasChosenAction() && challenged.HasChosenAction())
                break;

            yield return null;
        }

        if (!challenger.HasChosenAction())
        {
            challenger.DefaultAction();
            Debug.Log("challenger ִ����Ĭ�ϲ�����");
        }
                        
        
        if (!challenged.HasChosenAction())
        {
            challenged.DefaultAction();
            Debug.Log("challenged ִ����Ĭ�ϲ�����");
        }
            
    }

    // ִ�����ѡ��Ĳ���
    IEnumerator ExecuteActions()
    {
        // ��ȡ˫����ǰ��ս�ľ���
        Pet challengerPet = challenger.GetActivePet();
        Pet challengedPet = challenged.GetActivePet();

        // �Ƚ�˫��������ٶȣ��ٶȸߵ����ж�
        // todo ���ж�ѡ��ļ������ƣ����ж��ٶȣ����������������ʱ�ڵ������ǿ����
        if (challengerPet.ability.Speed > challengedPet.ability.Speed)
        {
            // ��ս���ȹ���
            challenger.ExecuteAction(challengerPet, challengedPet);
            
            
            //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}

            if (challengedPet.fightAbility.HP > 0)  // ������ط�û�б�����
                challenged.ExecuteAction(challengedPet, challengerPet);
            
            
            //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}
        }
        else
        {
            // ���ط��ȹ���
            challenged.ExecuteAction(challengedPet, challengerPet);

            //todo ��ս����ʼʱһ����Ҫ���пգ�֮��������װ��������ע�͵�
            //foreach (var buffInfo in challengedPetBag[challenged.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}

            

            if (challengerPet.fightAbility.HP > 0)  // �����ս��û�б�����
                challenger.ExecuteAction(challengerPet, challengedPet);

            //foreach (var buffInfo in challengerPetBag[challenger.activePetIndex].buffInfos)
            //{
            //    buffInfo.HandleUsingSkilleEffect();
            //}

        }

        // �ȴ����ܶ�����Ч���������
        yield return new WaitForSeconds(3f);
    }

    // ����ս��
    void EndBattle()
    {
        battleOngoing = false;

        if (challenger.AreAllPetsDefeated())
        {
            Debug.Log("��ս��ʧ�ܣ����ط���ʤ��");
        }
        else if (challenged.AreAllPetsDefeated())
        {
            Debug.Log("���ط�ʧ�ܣ���ս����ʤ��");
        }
    }


    // �����غ�
    void EndRound()
    {
        //todo ͬ�����ڵ�Ҫ���п�
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
    /// ��ʼ�����鱳������Ҫ�����ӡЧ����Ч
    /// </summary>
    /// <param name="petBag"></param>
    private void InitPetBag(List<Pet> petBag)
    {
        List<Pet> challengerPetBag = petBag;
        // ��� challengerPetBag �Ƿ���Ч
        if (challengerPetBag != null)
        {
            // �������� challengerPetBag �б�
            foreach (var pet in challengerPetBag)
            {
                // ��� pet �Ƿ�Ϊ��
                if (pet != null)
                {
                    // ��� pet �� buffInfos �Ƿ�Ϊ��
                    if (pet.buffInfos != null)
                    {
                        foreach (var buffInfo in pet.buffInfos)
                        {
                            // ����ÿ�� buffInfo
                            buffInfo.HandleBeginningOfTheBattleEffect(pet);
                        }
                    }


                    //ˢ������
                    //���ǵ���ӡֱ��+100Ѫ+50�ٵ�����Ч�����ȳ�ʼ��buffȻ���������
                    pet.RefreshFightAbility();
                }

            }
        }
    }



}
