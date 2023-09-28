using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    [Header("������ ���õ� �ൿ ����")]
    //public float atkSpd = 1f;           //���� �ӵ�
   // private float atkCdw = 0f;          //���� ������

    CharStats myStats;

    private void Start() {
        myStats = GetComponent<CharStats>();
    }

   /* private void Update() {
        atkCdw -= Time.deltaTime;
    }*/

    public void Attack(CharStats targetStats) {     //���� �������� ��
            targetStats.TakeDamage(myStats.damage.GetStat());

    }

    public void Guard(CharStats targetStats) {
            targetStats.GuardDamage(myStats.damage.GetStat());
        }

    public void Hitted(CharStats targetStats) {     //���� �������� ����
        myStats.TakeDamage(targetStats.damage.GetStat());
    }

}




