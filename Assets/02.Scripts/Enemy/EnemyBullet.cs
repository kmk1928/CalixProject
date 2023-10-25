using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    CharStats CharStats;
    CharCombat combat;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어에 닿으면
        {
        
        }

        // 총알과 다른 오브젝트와의 충돌 처리
        Destroy(gameObject, 0.05f); // 총알을 제거

    }
}