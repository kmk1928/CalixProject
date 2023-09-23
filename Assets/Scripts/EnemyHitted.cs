using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitted : MonoBehaviour
{
    public int maxHP = 100;           //최대체력
    public int curHP = 100;           //현재체력

    Rigidbody rigid;            //피격확인을 위함
    BoxCollider boxCollider;    //피격확인을 위함
    Material mat;               //피격 시 색상변경 확인

    private void Awake() {
        rigid = GetComponent<Rigidbody>();              //피격확인을 위함
        boxCollider = GetComponent<BoxCollider>();      //피격확인을 위함\
        mat = GetComponentInChildren<MeshRenderer>().material;    //피격 시 색상변경 확인
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Melee") {  //태그가 Melee일때 출력
            curHP -= 10;
            Debug.Log("Enemy Hit!! curHP = " + curHP);
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage() {
        mat.color = Color.red;                  //피격 시 빨간색으로 변경 후 
        yield return new WaitForSeconds(0.1f);  // 0.1초 후 아래 조건문에 의해 색 변경 

        if (curHP > 0) {
            mat.color = Color.white;        //체력이 남아있으면 흰색으로
        }
        else {
            mat.color = Color.gray;
            // Destroy(gameObject, 4);      //체력이 없으면 회색 + 4초 후 제거
        }
    }
}
