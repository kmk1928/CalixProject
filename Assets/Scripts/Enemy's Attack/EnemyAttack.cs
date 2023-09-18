using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public Transform player; // Player ������Ʈ�� Transform ������Ʈ�� �����ؾ� �մϴ�.
    public GameObject cubePrefab; // Cube ������Ʈ�� �������� �����ؾ� �մϴ�.
    public float attackDelay = 1.0f; // ���� ������ (1�ʷ� ����)

    private float timer = 0.0f;

    private void Update()
    {
        // Ÿ�̸Ӹ� ������Ʈ�Ͽ� ���� �����̸� ī��Ʈ�մϴ�.
        timer += Time.deltaTime;

        // ���� �����̰� ������ �� Cube�� �����ϴ�.
        if (timer >= attackDelay)
        {
            timer = 0.0f; // Ÿ�̸Ӹ� �ʱ�ȭ�մϴ�.

            // Player ������Ʈ�� ��ġ�� �����ɴϴ�.
            Vector3 playerPosition = player.position;

            // Cube ������Ʈ�� �ν��Ͻ�ȭ�մϴ�.
            GameObject cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);

            // 1�� �ڿ� Cube�� �����̵��� �����մϴ�.
            StartCoroutine(MoveCubeAfterDelay(cube, playerPosition));
        }
    }

    // Cube�� 1�� �ڿ� �����̴� �Լ�
    private IEnumerator MoveCubeAfterDelay(GameObject cube, Vector3 playerPosition)
    {
        yield return new WaitForSeconds(1.0f); // 1�� ���

        // Cube�� Player �������� �����ϴ�.
        Vector3 direction = (playerPosition - cube.transform.position).normalized;
        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
        cubeRigidbody.velocity = direction * 100.0f; // 10�� ���ư��� �ӵ��Դϴ�. �ʿ信 ���� ������ �� �ֽ��ϴ�.
    }

   
}
