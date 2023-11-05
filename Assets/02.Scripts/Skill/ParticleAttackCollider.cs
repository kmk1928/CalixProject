using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttackCollider : MonoBehaviour
{
    public float damage = 10;
    public float activationInterval = 0.4f; // 각 Collider의 활성화 간격 (초 단위)

    private BoxCollider[] colliders; // "ab" 게임 오브젝트의 모든 Collider 배열
    private int currentColliderIndex = 0; // 현재 활성화된 Collider의 인덱스

    private void Start()
    {
        colliders = GetComponents<BoxCollider>();

        if (colliders.Length >= 2)
        {
            // 시작 시 첫 번째 Collider 활성화
            StartCoroutine(ActivateCollider());
        }
        else
        {
            Debug.LogError("게임 오브젝트 'ab'에 적어도 2개의 Box Collider가 필요합니다.");
        }
    }

    private IEnumerator ActivateCollider()
    {
        while (currentColliderIndex < colliders.Length)
        {
            colliders[currentColliderIndex].enabled = true; // 현재 Collider를 활성화

            yield return new WaitForSeconds(activationInterval);

            colliders[currentColliderIndex].enabled = false; // 현재 Collider를 비활성화

            // 다음 Collider로 이동
            //currentColliderIndex = (currentColliderIndex + 1) % colliders.Length;
            currentColliderIndex += 1;
        }
        Debug.Log("작동된건지 확인");
    }
}
