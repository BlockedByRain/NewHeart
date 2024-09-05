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
        while (battleOngoing)
        {
            // ��ʼ����ʱ����˫�����ѡ���ж�
            yield return StartCoroutine(PlayerActionSelection());

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
            Debug.Log($"ʣ��ѡ��ʱ��: {timer}");

            if (challenger.HasChosenAction() && challenged.HasChosenAction())
                break;

            yield return null;
        }

        if (!challenger.HasChosenAction())
            challenger.DefaultAction();
        if (!challenged.HasChosenAction())
            challenged.DefaultAction();
    }

    // ִ�����ѡ��Ĳ���
    IEnumerator ExecuteActions()
    {
        // ��ȡ˫����ǰ��ս�ľ���
        Pet challengerPet = challenger.GetActivePet();
        Pet challengedPet = challenged.GetActivePet();

        // �Ƚ�˫��������ٶȣ��ٶȸߵ����ж�
        if (challengerPet.ability.Speed > challengedPet.ability.Speed)
        {
            // ��ս���ȹ���
            challenger.ExecuteAction(challengedPet);
            if (challengedPet.fightAbility.HP > 0)  // ������ط�û�б�����
                challenged.ExecuteAction(challengerPet);
        }
        else
        {
            // ���ط��ȹ���
            challenged.ExecuteAction(challengerPet);
            if (challengerPet.fightAbility.HP > 0)  // �����ս��û�б�����
                challenger.ExecuteAction(challengedPet);
        }

        // �ȴ����ܶ�����Ч���������
        yield return new WaitForSeconds(1f);
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

    }


}
