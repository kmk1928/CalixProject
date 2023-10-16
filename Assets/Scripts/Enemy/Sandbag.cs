using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : MonoBehaviour
{
    Material mat;
    public BoxCollider attackArea;
    public BoxCollider powerAttackArea;
    bool isAttack = false;
    int attackCount = 0;

    private void Awake() {
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    private void FixedUpdate() {
        Targeting();
    }

    void Targeting() {              // 적을 인식하기 위한 타겟팅 함수
        float targetRadius = 5f;  // 탐지 스피어의 반지름 설정
        float targetRange = 8f;     // 탐지 스피어 캐스트의 최대 거리 설정

        // 스피어 캐스트를 통해 플레이어를 탐지
        RaycastHit[] rayHits = Physics.SphereCastAll(
            transform.position,                 // 스피어 캐스트의 시작 위치
            targetRadius,                       // 스피어의 반지름
            transform.forward,                  // 스피어 캐스트의 방향 (전방)
            targetRange,                        // 스피어 캐스트의 최대 탐지 거리
            LayerMask.GetMask("Player")         // 플레이어 레이어에 해당하는 대상만 검색
        );

        // 만약 스피어 캐스트로 플레이어가 검출되고, 적이 공격 중이 아닌 경우
        if (rayHits.Length > 0 && !isAttack) {
            // 적의 공격 코루틴인 SandBag_Attack()을 실행
            StartCoroutine(SandBag_Attack());
        }
    }

    IEnumerator SandBag_Attack() {
        isAttack = true;

        if (attackCount < 2) {             //enemyAttack 일반공격
            mat.color = Color.green;
            yield return new WaitForSeconds(1f);
            mat.color = Color.yellow;
            yield return new WaitForSeconds(1f);
            mat.color = Color.magenta;
            attackArea.enabled = true;
            yield return new WaitForSeconds(0.1f);
            attackArea.enabled = false;
            yield return new WaitForSeconds(0.5f);
            attackCount++;
        }
        else if (attackCount >= 2) {         //enemyPowerAttack 강한공격
            attackCount = 0;
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.magenta;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.magenta;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.cyan;
            powerAttackArea.enabled = true;
            yield return new WaitForSeconds(0.1f);
            powerAttackArea.enabled = false;
            yield return new WaitForSeconds(1f);
        }


        isAttack = false;
    }
}
