using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPhysics : MonoBehaviour
{
    public LayerMask collisionLayer;   // 충돌 감지할 레이어 마스크
    public float capsuleRadius = 1.5f; // 캡슐의 반경
    public float capsuleHeight = 2.0f; // 캡슐의 높이
    public float moveSpeed = 25.0f;     // 밀려나는 속도

    // 주인공과 몬스터의 질량
    public float playerMass = 2.0f;
    public float monsterMass = 1.0f;

    private Rigidbody rb;  // 주인공의 Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = playerMass; // 주인공의 질량 설정
    }

    void Update()
    {
        // 캡슐 형태의 오버랩 영역을 정의합니다.
        Vector3 capsuleCenter = transform.position + Vector3.up * (capsuleHeight / 2);
        Collider[] overlappedColliders = Physics.OverlapCapsule(capsuleCenter, Vector3.up, capsuleRadius, collisionLayer);

        foreach (var collider in overlappedColliders)
        {
            // 겹쳐진 개체와의 충돌을 해결합니다.
            Vector3 direction;
            float distance;
            if (Physics.ComputePenetration(GetComponent<Collider>(), transform.position, transform.rotation, collider, collider.transform.position, collider.transform.rotation, out direction, out distance))
            {
                // y 축의 방향을 무시합니다.
                direction.y = 0.0f;

                // 밀려나는 거리를 정의합니다.
                Vector3 pushVector = direction * distance;

                // 상대 개체의 질량을 고려하여 밀려나는 힘을 조정합니다.
                Rigidbody otherRb = collider.GetComponent<Rigidbody>();
                if (otherRb != null)
                {
                    float massRatio = playerMass / (playerMass + monsterMass);
                    pushVector *= massRatio;
                }

                // 점차적으로 밀려나도록 Lerp를 사용합니다.
                rb.MovePosition(transform.position + pushVector * moveSpeed * Time.deltaTime);
            }
        }
    }
}
