using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    CharStats charStats;
    CharCombat combat;
    PlayerStats playerStats;
    public GameObject hitParticle;

    public float damage = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
            playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeAPDamage(damage);
        }
        GameObject hitPar =  Instantiate(hitParticle, this.transform.position, Quaternion.identity);
        // �Ѿ˰� �ٸ� ������Ʈ���� �浹 ó��
        Destroy(gameObject, 0.03f); // �Ѿ��� ����
        Destroy(hitPar, 1.5f);
    }
}