using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats
{
    PlayerAttacker playerAk;

    [Header("크리티컬 확률 및 데미지")]
    [Tooltip("크리티컬 확률 0.0~1.0")]
    public float criticalChance = 0.0f; // 50%의 확률을 0.5로 표현
    [Tooltip("크리티컬 데미지의 기본값은 1.5")]
    public float criticalDamage = 1.5f;
    public float curDamageCal = 1.0f;

    public float imsi;

    [SerializeField] public int redCount = 0;
    [SerializeField] public int yellowCount = 0;
    [SerializeField] public int blueCount = 0;

    private void Start()
    {
        playerAk = GetComponent<PlayerAttacker>();
    }

    private void Update()
    {
        imsi = (float)curHealth / (float)maxHealth;
        UIManager.instance.UpdateHPBar(imsi);
        UIManager.instance.UpdateHpText(curHealth, maxHealth);
    }


    public void ApplyItemModifiers(Item item)
    {
        maxHealth += item.healthEnergy;
        attackDamage += item.attackEnergy * 0.1f;
        defense += item.defenseEnergy;

        if (item.itemType == Item.ItemType.Red)
            RedApplyStatBonuses();
        else if (item.itemType == Item.ItemType.Blue)
            BlueApplyStatBonuses();
        else if (item.itemType == Item.ItemType.Yellow)
            YellowApplyStatBonuses();
    }

    public void RemoveItemModifiers(Item item)
    {
        maxHealth -= item.healthEnergy;
        attackDamage -= item.attackEnergy * 0.1f;
        defense -= item.defenseEnergy;

        if (item.itemType == Item.ItemType.Red)
            RedRemoveStatBonuses();
        else if (item.itemType == Item.ItemType.Blue)
            BlueRemoveStatBonuses();
        else if (item.itemType == Item.ItemType.Yellow)
            YellowRemoveStatBonuses();
    }


    private void RedApplyStatBonuses()
    {
        if (redCount == 3)
        {
            maxHealth += 100;
        }
        else if (redCount == 6)
        {
            maxHealth += 200;
        }
    }

    private void YellowApplyStatBonuses()
    {
        if (yellowCount == 3)
        {
            attackDamage += 10;
        }
        else if (yellowCount == 6)
        {
            attackDamage += 20;
        }
    }

    private void BlueApplyStatBonuses()
    {
        if (blueCount == 3)
        {
            defense += 10;
            criticalChance += 0.2f;
        }
        
        else if (blueCount == 6)
        {
            defense += 20;
            criticalChance += 0.3f;
        }
    }


    private void RedRemoveStatBonuses()
    {
        if (redCount == 2)
        {
            maxHealth -= 100;
        }
        else if (redCount == 5)
        {
            maxHealth -= 200;
        }
    }

    private void YellowRemoveStatBonuses()
    {
        if (yellowCount == 2)
        {
            attackDamage -= 10;
        }
        else if (yellowCount == 5)
        {
            attackDamage -= 20;
        }
    }

    private void BlueRemoveStatBonuses()
    {
        if (blueCount == 2)
        {
            defense -= 10;
            criticalChance -= 0.2f;
        }
        else if (blueCount == 5)
        {
            defense -= 20;
            criticalChance -= 0.3f;
        }
    }


    private void LateUpdate()
    {
        if (playerAk.isNAing && !playerAk.isSkill_1ing && !playerAk.isSkill_2ing)
        {
            curDamageCal = playerAk.attackPatterns_void_normalAk[playerAk.indexValueForCalculation].damageMultiplier;
        }
        if (playerAk.isSkill_1ing && !playerAk.isNAing && !playerAk.isSkill_2ing)
        {
            curDamageCal = playerAk.attackPatterns_void_Skill_1[playerAk.indexValueForCalculation].damageMultiplier;
        }
        if (playerAk.isSkill_2ing && !playerAk.isNAing && !playerAk.isSkill_1ing)
        {
            curDamageCal = playerAk.attackPatterns_void_Skill_2[0].damageMultiplier;
        }
    }
}