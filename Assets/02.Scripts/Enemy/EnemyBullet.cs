using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    CharStats CharStats;
    CharCombat combat;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
        
        }

        // �Ѿ˰� �ٸ� ������Ʈ���� �浹 ó��
        Destroy(gameObject, 0.05f); // �Ѿ��� ����

    }
}