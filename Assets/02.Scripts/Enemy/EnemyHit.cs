using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHit : MonoBehaviour
{
    // 스크립트 설명 적의 피해입는 기능
    CharStats enemyStats;
    CharCombat combat;
    Material mat;               //피격 시 색상변경 확인
    Animator anim; // 애니메이션 변수
    PlayerController playerController;
    GameObject nearObject;
    BoxCollider enemyCollider;
    NavMeshAgent agent;

    private float noHitTime = 0.2f;
    private bool isHitted = false;
    private bool deathAnim = false;
    void Awake() {
        combat = GetComponent<CharCombat>();     
        mat = GetComponentInChildren<MeshRenderer>().material;    //피격 시 색상변경 확인
        anim = GetComponentInChildren<Animator>(); // 애니메이션
        enemyStats = GetComponent<CharStats>();
        enemyCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (enemyStats.isDead) {
            if (!deathAnim) {
                deathAnim = true;
                if(agent != null)
                {
                    agent.isStopped = true;
                }
                int count = Random.Range(1, 4);
                switch (count)
                {
                    case 1:
                        anim.SetTrigger("enemyDeathTrg");
                        break;
                    case 2:
                        anim.SetTrigger("enemyDeathTrg2");
                        break;
                    case 3:
                        anim.SetTrigger("enemyDeathTrg3");
                        break;
                }
                enemyCollider.enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("melee") && !isHitted && !enemyStats.isDead) {  //태그가 Melee일때 출력
            Debug.Log("Enemy Hit!!");  
            PlayerStats targetStatus = other.transform.root.GetComponent<PlayerStats>();
            if (targetStatus != null) {
                combat.EnemyHitted(targetStatus);
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
        isHitted = true;
        mat.color = Color.red;      //피격 시 빨간색으로 변경 후 
        Damaged();
        yield return new WaitForSeconds(noHitTime);  // 0.1초 후 아래 조건문에 의해 색 변경
        isHitted = false;
        mat.color = Color.white;

    }

}
