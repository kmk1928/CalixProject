using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
            // �÷��̾ ����� ���� ó�� �ڵ�
            Destroy(gameObject); // �Ѿ��� ����
        }
        else
        {
            // �Ѿ˰� �ٸ� ������Ʈ���� �浹 ó��
            Destroy(gameObject); // �Ѿ��� ����
        }
    }
}