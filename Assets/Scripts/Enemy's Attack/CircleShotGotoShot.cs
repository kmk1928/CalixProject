using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShotGotoShot : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public float radius = 5f; // 원의 반지름
    public int numberOfBullets = 27; // 생성할 총알 수
    public float spawnInterval = 3f; // 생성 간격 (3초)
    public Transform player; // 플레이어의 Transform 컴포넌트

    private void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            // 플레이어를 향해 회전
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

            float angleStep = 360f / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleStep;

                // 각도를 라디안으로 변환
                float radians = angle * Mathf.Deg2Rad;

                // 총알 생성 위치 계산 (수직 원)
                Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radians) * radius, Mathf.Sin(radians) * radius, 0f);

                // 총알 생성
                Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

                yield return null; // 다음 프레임까지 대기
            }

            yield return new WaitForSeconds(spawnInterval); // 다음 생성 주기까지 대기
        }
    }
}