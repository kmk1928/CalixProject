using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    /* 스크립트 설명 적의 피해입는 기능
     
     */
    CharCombat combat;
    Material mat;               //피격 시 색상변경 확인
    Animator anim; // 애니메이션 변수
    PlayerController playerController;
    GameObject nearObject;
    void Awake() {
        combat = GetComponent<CharCombat>();     
        mat = GetComponentInChildren<MeshRenderer>().material;    //피격 시 색상변경 확인
        anim = GetComponentInChildren<Animator>(); // 애니메이션
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "melee") {  //태그가 Melee일때 출력
            Debug.Log("Enemy Hit!!");  
            CharStats targetStatus = other.transform.root.GetComponent<CharStats>();
            if (targetStatus != null) {
                combat.Hitted(targetStatus);
            }
                              //애님 트리거  doDamage 발동
            StartCoroutine(OnDamage());
        }
    }
    public void Damaged() {             //애님 트리거  doDamage 발동
        anim.SetTrigger("doDamage");
        Debug.Log("     - - -      ");
    }

    IEnumerator OnDamage() 
    {
        mat.color = Color.red;      //피격 시 빨간색으로 변경 후 
        Damaged();
        yield return new WaitForSeconds(0.2f);  // 0.1초 후 아래 조건문에 의해 색 변경    
        mat.color = Color.white;

    }// Start is called before the first frame update
}
