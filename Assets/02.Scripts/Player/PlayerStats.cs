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

    private void Start() {
        playerAk = GetComponent<PlayerAttacker>();
    }

    private void LateUpdate() {
        curDamageCal = playerAk.attackPatterns_void[playerAk.indexValueForCalculation].damageMultiplier;
        //Debug.Log("���� pl���� �޴� index��");
    }
}
