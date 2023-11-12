using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShotGotoShot : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public float radius = 5f; // ���� ������
    public int numberOfBullets = 27; // ������ �Ѿ� ��
    public float spawnInterval = 3f; // ���� ���� (3��)
    public Transform player; // �÷��̾��� Transform ������Ʈ

    private void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            // �÷��̾ ���� ȸ��
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

            float angleStep = 360f / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleStep;

                // ������ �������� ��ȯ
                float radians = angle * Mathf.Deg2Rad;

                // �Ѿ� ���� ��ġ ��� (���� ��)
                Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radians) * radius, Mathf.Sin(radians) * radius, 0f);

                // �Ѿ� ����
                Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

                yield return null; // ���� �����ӱ��� ���
            }

            yield return new WaitForSeconds(spawnInterval); // ���� ���� �ֱ���� ���
        }
    }
}