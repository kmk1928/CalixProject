using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats
{
    PlayerAttacker playerAk;

    [Header("�÷��̾�� ġ��Ÿ ����")]
    [Tooltip("ġ��Ÿ Ȯ�� 0.0~1.0")]
    public float criticalChance = 0.5f; // 50%�� ũ��Ƽ�� Ȯ��
    [Tooltip("ġ��Ÿ ���� ������ �⺻ 1.5��")]
    public float criticalDamage = 1.5f;
    public float curDamageCal = 1.0f;       //������ ���

    public float imsi;

    [SerializeField] public int redCount = 0;
    [SerializeField] public int yellowCount = 0;
    [SerializeField] public int blueCount = 0;

    private void Start() {
        playerAk = GetComponent<PlayerAttacker>();
    }

    private void Update() {

        imsi = (float)curHealth / (float)maxHealth;
        UIManager.instance.UpdateHPBar(imsi);
        UIManager.instance.UpdateHpText(curHealth, maxHealth);
    }

    public void ApplyItemModifiers(Item item)
    {
        maxHealth += item.healthEnergy;
        attackDamage += item.attackEnergy;
        defense += item.defenseEnergy;

        PlusStatBonus1();
        PlusStatBonus2();
    }

    public void RemoveItemModifiers(Item item)
    {
        maxHealth -= item.healthEnergy;
        attackDamage -= item.attackEnergy;
        defense -= item.defenseEnergy;

        MinusStatBonus1();
        MinusStatBonus2();
    }




    public void PlusStatBonus1()
    {
        if(redCount == 3)
            maxHealth = maxHealth + 100;

        else if (yellowCount == 3)
            attackDamage = attackDamage + 10;

        else if (blueCount == 3)
            defense = defense + 10;
    }

    public void PlusStatBonus2()
    {
        if(redCount == 6)
            maxHealth = maxHealth + 100;

        else if (yellowCount == 6)
            attackDamage = attackDamage + 10;

        else if (blueCount == 6)
            defense = defense + 10;
    }



    public void MinusStatBonus1()
    {
        if(redCount == 3)
            maxHealth = maxHealth - 100;

        else if (yellowCount == 3)
            attackDamage = attackDamage - 10;

        else if (blueCount == 3)
            defense = defense - 10;
    }

    public void MinusStatBonus2()
    {
        if(redCount == 6)
            maxHealth = maxHealth - 100;

        else if (yellowCount == 6)
            attackDamage = attackDamage - 10;

        else if (blueCount == 6)
            defense = defense - 10;
    }


    private void LateUpdate() { 
        if (playerAk.isNAing && !playerAk.isSkill_1ing && !playerAk.isSkill_2ing) {
            curDamageCal = playerAk.attackPatterns_void_normalAk[playerAk.indexValueForCalculation].damageMultiplier;
        }
        if (playerAk.isSkill_1ing && !playerAk.isNAing && !playerAk.isSkill_2ing) {
            curDamageCal = playerAk.attackPatterns_void_Skill_1[playerAk.indexValueForCalculation].damageMultiplier;
        }
        if (playerAk.isSkill_2ing && !playerAk.isNAing && !playerAk.isSkill_1ing) {
            curDamageCal = playerAk.attackPatterns_void_Skill_2[0].damageMultiplier;
        }
    }
}
