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
        if (other.CompareTag("Player")) // 플레이어에 닿으면
        {
            playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeAPDamage(damage);
        }

        // 총알과 다른 오브젝트와의 충돌 처리
        Destroy(gameObject, 0.03f); // 총알을 제거

    }
}