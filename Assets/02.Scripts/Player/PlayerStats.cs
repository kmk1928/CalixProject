using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats
{
    PlayerAttacker playerAk;

    [Header("플레이어용 치명타 스탯")]
    [Tooltip("치명타 확률 0.0~1.0")]
    public float criticalChance = 0.5f; // 50%의 크리티컬 확률
    [Tooltip("치명타 피해 증가량 기본 1.5배")]
    public float criticalDamage = 1.5f;
    public float curDamageCal = 1.0f;       //데미지 계수

    public float imsi;

    [SerializeField] private int redCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int blueCount = 0;

    private void Start() {
        playerAk = GetComponent<PlayerAttacker>();
    }

    private void Update() {

        imsi = (float)curHealth / (float)maxHealth;
        UIManager.instance.UpdateHPBar(imsi);
    }

    public void ApplyItemModifiers(Item item)
    {
        // 아이템의 능력치 변경량을 적용
        //maxHealth += item.healthModifier;
        //attackDamage += item.attackModifier;
        //defense += item.defenseModifier;

        // 다른 능력치 변경량도 적용
    }

    public void RemoveItemModifiers(Item item)
    {
        // 아이템의 능력치 변경량을 제거
        //maxHealth -= item.healthModifier;
        //attackDamage -= item.attackModifier;
        //defense -= item.defenseModifier;

        // 다른 능력치 변경량도 제거
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


        //Debug.Log("현재 pl스가 받는 index값");
    }
}
