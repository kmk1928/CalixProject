using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitParticle : MonoBehaviour
{

    public ParticleSystem hitEffect; // 파티클 시스템 연결
    public ParticleSystem hitBlood;

    private void OnCollisionEnter(Collision collision) {
         if (collision.gameObject.CompareTag("melee")) {
            // 충돌 지점을 가져옵니다.
            ContactPoint contact = collision.GetContact(0);
            Debug.LogWarning("되는겅ㅁ?");
            Vector3 contactPoint = contact.point;
            Vector3 contactNormal = contact.normal; //방향추가
            Debug.LogWarning("파티클 나옴");

                // 파티클 시스템의 위치와 방향을 설정하고 재생합니다.
                hitBlood.transform.position = contactPoint;
                // 파티클 시스템의 회전만 설정합니다.
                hitBlood.transform.rotation = Quaternion.LookRotation(contactNormal); // 충돌 법선 방향으로 회전
                hitBlood.Play();
            
                hitEffect.transform.position = contactPoint;
                hitEffect.Play();
        }
    }

}
