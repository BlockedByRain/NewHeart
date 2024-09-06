using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer
{
    //��װ

    //�ƺ�

    //Ŀ��

    //����
    public List<Pet> petBag = new List<Pet>(6);

    //��ǰ��ս�ľ�������
    public int activePetIndex = 0;
    //����Ƿ���ѡ�����
    public bool actionChosen = false;

    public bool HasChosenAction()
    {
        return actionChosen;
    }

    public void DefaultAction()
    {
        // ��������趨ΪĬ�Ϲ��������
        actionChosen = true;
        Debug.Log("ʱ���þ����Զ�ִ��Ĭ�ϲ�����");
    }

    public void ExecuteAction(Pet user, Pet target)
    {
        Pet activePet = GetActivePet();
        SkillInfo chosenSkill = ChooseSkill(); 
        
        // �������ѡ���˼���
        int playerChoose =0;

        activePet.GetSelectedSkill(playerChoose).Execute(user,target);
        actionChosen = false;
    }

    public Pet GetActivePet()
    {
        return petBag[activePetIndex];
    }

    /// <summary>
    /// �Ƿ����о��鶼����
    /// </summary>
    /// <returns></returns>
    public bool AreAllPetsDefeated()
    {
        foreach (Pet pet in petBag)
        {
            if (pet.fightAbility.HP > 0)
                return false;
        }
        return true;
    }

    public SkillInfo ChooseSkill()
    {
        // ģ�⼼��ѡ���߼�������ѡ��ǰ�����б��еĵ�һ������
        return GetActivePet().currentSkills[0];
    }
}
