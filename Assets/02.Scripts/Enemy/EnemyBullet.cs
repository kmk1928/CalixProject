using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    CharStats charStats;
    CharCombat combat;
    PlayerStats playerStats;
    public float damage = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
            playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeAPDamage(damage);
        }

        // �Ѿ˰� �ٸ� ������Ʈ���� �浹 ó��
        Destroy(gameObject, 0.03f); // �Ѿ��� ����

    }
}