using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    //ĳ���͵��� �������� ����Ѵ�. ����� ���� �ڽ��� CharStats�� �Ѱܼ� ü���� ����
    CharStats myStats;

    [Header("������ ���õ� �ൿ ����")]
    public float atkSpd = 1f;           //���� �ӵ�
    private float atkCdw = 0f;          //���� ������\

    private float finalDamage;

    private void Start() {
        myStats = GetComponent<CharStats>();
    }
    private void Update() {
        atkCdw -= Time.deltaTime;
    }

    #region ���ظ� �޴� �ʿ��� ������ ���
    //targetStats�� ������ �����̴�. �� �������� �ڽſ��� �ִ� ������Ʈ�� ���� 
    public void EnemyHitted(PlayerStats targetStats) {     //���� �������� ����
        finalDamage = (targetStats.attackDamage * targetStats.curDamageCal);
        float randomValue = Random.Range(0f, 1f);
        if(randomValue < targetStats.criticalChance) {
            finalDamage *= targetStats.criticalDamage;
            Debug.Log("Critical!! - CharCombat");
        }
        Debug.LogWarning("���� ���� ������" + finalDamage);
        myStats.TakeADDamage(finalDamage);
    }
    public void PlayerHitted(CharStats targetStats) {     //���� �������� ����
        finalDamage = targetStats.attackDamage;
        myStats.TakeADDamage(finalDamage);
    }

    public void Guard(CharStats targetStats) {  //���� �� ������ %����
        finalDamage = targetStats.attackDamage;
        myStats.GuardDamage(finalDamage);
        }

    private void DamageCalculation() {
        /* �ӽ� ������ ����
        // float damage = ���ݷ� * [���� �߰� ������] * [���� ȿ��];
        //Random.Range() �Լ��� ����Ͽ� 0�� 1 ������ ������ ���� ����
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < criticalChance) {
            // ũ��Ƽ�� ������ ����
            damage *= (1 + ũ��Ƽ�� ������);
        }
         */
    }
    #endregion


}




