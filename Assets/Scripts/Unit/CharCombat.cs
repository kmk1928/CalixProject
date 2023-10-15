using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    CharStats myStats;

    /*[Header("������ ���õ� �ൿ ����")]
    //public float atkSpd = 1f;           //���� �ӵ�
      private float atkCdw = 0f;          //���� ������
     private void Update() {
         atkCdw -= Time.deltaTime;
    }
     */

    private float finalDamage;

    private void Start() {
        myStats = GetComponent<CharStats>();
    }

    #region ���ظ� �޴� �ʿ��� ������ ���
    //targetStats�� ������ �����̴�. �� �������� �ڽſ��� �ִ� ������Ʈ�� ���� 
    public void Hitted(CharStats targetStats) {     //���� �������� ����
        finalDamage = targetStats.attackDamage;
        float randomValue = Random.Range(0f, 1f);
        if(randomValue < targetStats.criticalChance) {
            finalDamage *= targetStats.criticalDamage;
        }
        myStats.TakeADDamage(finalDamage);
    }

    public void Guard(CharStats targetStats) {
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




