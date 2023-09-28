using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Scripts : MonoBehaviour
{
    public Transform player; // Player ������Ʈ�� Transform ������Ʈ�� �����ؾ� �մϴ�.

    private bool isHit = false; // �浹 ���θ� ��Ÿ���� ����
    private Rigidbody rb; // Cube�� Rigidbody ������Ʈ

    void Start()
    {
        // Rigidbody ������Ʈ ��������
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Cube�� �浹���� �ʾ��� ���� ȸ�� �ڵ带 �����մϴ�.
        if (!isHit)
        {
            // Cube�� ��ġ���� Player�� ��ġ�� ���ϵ��� ���� ���͸� ���մϴ�.
            Vector3 directionToPlayer = player.position - transform.position;

            // ���� ���͸� ȸ�� ������ ��ȯ�մϴ�.
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);

            // Cube�� ȸ���� �����մϴ�.
            transform.rotation = rotationToPlayer;
        }
    }

    // �浹 �߻� �� ȣ��Ǵ� �Լ�
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player" �Ǵ� "Ground"�� ��
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            // Rigidbody�� �ӵ��� 0���� �����Ͽ� ��� �������� ����ϴ�.
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // �浹 ���¸� true�� ����
            isHit = true;

            // 1�� �Ŀ� ��������� �ڷ�ƾ�� �����մϴ�.
            StartCoroutine(DisappearAfterDelay(1f));
        }
    }

    // ���� �ð� �Ŀ� ��������� �ϴ� �ڷ�ƾ �Լ�
    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 1�� �Ŀ� Cube�� �����մϴ�.
        Destroy(gameObject);
    }
}