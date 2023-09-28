using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    CharCombat combat;

    Rigidbody rigid;            //피격확인을 위함
    BoxCollider boxCollider;    //피격확인을 위함
    Material mat;               //피격 시 색상변경 확인

    void Awake() {
        combat = GetComponent<CharCombat>();
        rigid = GetComponent<Rigidbody>();              //피격확인을 위함
        boxCollider = GetComponent<BoxCollider>();      
        mat = GetComponentInChildren<MeshRenderer>().material;    //피격 시 색상변경 확인
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "melee") {  //태그가 Melee일때 출력
            Weapon weapon = other.GetComponent<Weapon>();
            CharStats targetStatus = other.transform.root.GetComponent<CharStats>();
            if (targetStatus != null) {
                Debug.Log("Enemy Hit!!");
                combat.Hitted(targetStatus);
            }
          
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage() 
    {
        mat.color = Color.red;                  //피격 시 빨간색으로 변경 후 
        yield return new WaitForSeconds(0.1f);  // 0.1초 후 아래 조건문에 의해 색 변경    
        mat.color = Color.white;

    }// Start is called before the first frame update
}
