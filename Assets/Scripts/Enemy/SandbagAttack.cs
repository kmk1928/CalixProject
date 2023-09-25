using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagAttack : MonoBehaviour
{
    Material mat;
    public BoxCollider attackArea;
    bool isAttack = false;

    private void Awake()
    {
        mat = GetComponentInChildren<MeshRenderer>().material; 
    }

    void Targeting() {                  //적을 인식하기 위한 타겟팅
        float targetRadius = 1.5f;
        float targetRange = 3f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
                                  targetRadius,                     //범위
                                  transform.forward,              //전방의
                                  targetRange,                    //추적 거리
                                  LayerMask.GetMask("Player"));   //레이어가 플레이어인 대상

        if (rayHits.Length > 0 && !isAttack) {       //rayhit변수에 데이터가 들어오면 공격 코루틴 실행 
            StartCoroutine(SandBag_Attack());
        }  
    }

    private void FixedUpdate() {   //타겟팅을 위한 픽스드업데이트
        Targeting();
    }

    IEnumerator SandBag_Attack() {
        isAttack = true;

        mat.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.magenta;
        attackArea.enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackArea.enabled = false;

        isAttack = false; 
    }

}
