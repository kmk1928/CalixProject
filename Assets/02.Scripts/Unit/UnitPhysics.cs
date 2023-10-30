using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPhysics : MonoBehaviour
{
    public LayerMask collisionLayer;   // �浹 ������ ���̾� ����ũ
    public float capsuleRadius = 1.5f; // ĸ���� �ݰ�
    public float capsuleHeight = 2.0f; // ĸ���� ����
    public float moveSpeed = 25.0f;     // �з����� �ӵ�

    // ���ΰ��� ������ ����
    public float playerMass = 2.0f;
    public float monsterMass = 1.0f;

    private Rigidbody rb;  // ���ΰ��� Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = playerMass; // ���ΰ��� ���� ����
    }

    void Update()
    {
        // ĸ�� ������ ������ ������ �����մϴ�.
        Vector3 capsuleCenter = transform.position + Vector3.up * (capsuleHeight / 2);
        Collider[] overlappedColliders = Physics.OverlapCapsule(capsuleCenter, Vector3.up, capsuleRadius, collisionLayer);

        foreach (var collider in overlappedColliders)
        {
            // ������ ��ü���� �浹�� �ذ��մϴ�.
            Vector3 direction;
            float distance;
            if (Physics.ComputePenetration(GetComponent<Collider>(), transform.position, transform.rotation, collider, collider.transform.position, collider.transform.rotation, out direction, out distance))
            {
                // y ���� ������ �����մϴ�.
                direction.y = 0.0f;

                // �з����� �Ÿ��� �����մϴ�.
                Vector3 pushVector = direction * distance;

                // ��� ��ü�� ������ ����Ͽ� �з����� ���� �����մϴ�.
                Rigidbody otherRb = collider.GetComponent<Rigidbody>();
                if (otherRb != null)
                {
                    float massRatio = playerMass / (playerMass + monsterMass);
                    pushVector *= massRatio;
                }

                // ���������� �з������� Lerp�� ����մϴ�.
                rb.MovePosition(transform.position + pushVector * moveSpeed * Time.deltaTime);
            }
        }
    }
}
