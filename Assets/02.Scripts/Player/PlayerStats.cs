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
        // �������� �ɷ�ġ ���淮�� ����
        //maxHealth += item.healthModifier;
        //attackDamage += item.attackModifier;
        //defense += item.defenseModifier;

        // �ٸ� �ɷ�ġ ���淮�� ����
    }

    public void RemoveItemModifiers(Item item)
    {
        // �������� �ɷ�ġ ���淮�� ����
        //maxHealth -= item.healthModifier;
        //attackDamage -= item.attackModifier;
        //defense -= item.defenseModifier;

        // �ٸ� �ɷ�ġ ���淮�� ����
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


        //Debug.Log("���� pl���� �޴� index��");
    }
}
