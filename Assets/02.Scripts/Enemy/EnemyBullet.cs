using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어에 닿으면
        {
            // 플레이어에 닿았을 때의 처리 코드
            Destroy(gameObject); // 총알을 제거
        }
        else
        {
            // 총알과 다른 오브젝트와의 충돌 처리
            Destroy(gameObject); // 총알을 제거
        }
    }
}