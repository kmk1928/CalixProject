using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // 순찰 지점 배열
    public Transform player; // 플레이어 게임 오브젝트
    private NavMeshAgent navMeshAgent;
    private Vector3 originalPosition;
    private bool isPatrolling = true;
    public float detectionDistance = 10f; // 플레이어 감지 범위

    Animator anim; // 애니메이션 변수

    // 공격
    bool isAttack = false;
    float attackDelay = 1.3f; // 공격 딜레이 (초)

    private float originalSpeed;

    public float enemyAttackRange = 1.4f;    //OnDrawGizmos() 범위 표시용 float(거리)

    bool isInteracting = false; //애니메이션 실행중인지 확인하는 bool - attack에 사용

    CharStats myStats;
    CharCombat combat;
    private int comboAttackCount = 0;
    private int deathCount = 0;

    public BoxCollider attackArea;
 
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyAttackRange);
    }

    private void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        SetDestinationToNextPatrolPoint();

        anim = GetComponentInChildren<Animator>(); // 애니메이션
        originalSpeed = navMeshAgent.speed; // AI 이동속도 저장

        combat = GetComponent<CharCombat>();
        myStats = GetComponent<CharStats>();
    }

    private void Update()
    {
            if (GameManager.isGameover) {
                //플레이어 사망 시 인식 거리를 줄여 원래 위치로 돌아가게 함
                detectionDistance = 0.1f;
            }

            anim.SetFloat("enemySpeed", navMeshAgent.speed);
            isInteracting = anim.GetBool("isInteracting");
            isAttack = anim.GetBool("isAttack");
            if (player != null) {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (!isInteracting) {
                    if (distanceToPlayer <= detectionDistance)      //플레이어 발견 시
                    {

                        // 플레이어 따라가기
                        navMeshAgent.SetDestination(player.position);
                        isPatrolling = false;

                        // 플레이어가 감지 범위 내에 있을 때 공격
                        if (distanceToPlayer <= 2.0f && !isAttack) {
                            anim.SetBool("isInteracting", true); // 애니메이션 대기용 애니메이터 bool파라미터
                            navMeshAgent.speed = 0f;                // AI의 이동 속도를 0으로 설정
                            //int randomAttack = Random.Range(0, 2);
                            Debug.Log("---Find Player---");
                            Attack2();

                            /*
                                if (randomAttack == 0) {
                                Attack();
                            }
                            else {
                                Attack2();
                            }
                                */
                        }

                    }
                    else if (!isPatrolling) {
                        // 플레이어가 감지 범위 밖으로 나갔을 때 원래 위치로 돌아가기
                        navMeshAgent.SetDestination(originalPosition);
                        if (navMeshAgent.remainingDistance < 0.2f) {
                            isPatrolling = true;            //원래 위치로 돌아온 뒤 속도0으로 변경
                                                            //navMeshAgent.speed = 0;
                        }
                    }
                }

            }
            else if (!isPatrolling) {
                // 플레이어가 없을 때 원래 위치로 돌아가기
                navMeshAgent.SetDestination(originalPosition);
                if (navMeshAgent.remainingDistance < 0.5f) {
                    isPatrolling = true;
                }
            }
        
        
    }

    private void SetDestinationToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        navMeshAgent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
    }

    // 공격 메서드
    void Attack()
    {
        Debug.Log("ATTACK"); // 디버그 로그 출력 (대문자 'Debug')
        StartCoroutine(AttackOut("doSwing"));
    }

    void Attack2() {
        Debug.Log("ATTACK2");
        StartCoroutine(AttackOut("doSwing2"));
    }

    // 공격 종료 메서드
    IEnumerator AttackOut(string attackName) { 
        isAttack = true;
        anim.SetTrigger(attackName);            //공격에 맞는 트리거 발동
        while (isAttack) {
            yield return new WaitForSeconds(1f);  //공격 종료 후 idle의 지속 시간
        }
        isAttack = false;
        // AI의 이동 속도를 원래 값으로 복원, 저장해둔 속도로 플레이어 추격
        navMeshAgent.speed = originalSpeed;
    }
    IEnumerator ComboAttackOut(string attackName) {
        isAttack = true;
        navMeshAgent.speed = 0f;                // AI의 이동 속도를 0으로 설정
        while (comboAttackCount < 3) {
            anim.SetTrigger(attackName);            //공격에 맞는 트리거 발동
            yield return new WaitForSeconds(0.5f);
            comboAttackCount++;
        }
        // AI의 이동 속도를 원래 값으로 복원
        navMeshAgent.speed = originalSpeed;
        isAttack = false;
    }


    /*
       void Attack()
     {
         // AI의 이동 속도를 0으로 설정
         navMeshAgent.speed = 0f;
         anim.SetTrigger("doSwing");
         isAttack = true;
         Debug.Log("ATTACK"); // 디버그 로그 출력 (대문자 'Debug')
         Invoke("AttackOut", attackDelay);
     }

     // 공격 종료 메서드
     void AttackOut()
     {

         // AI의 이동 속도를 원래 값으로 복원
         navMeshAgent.speed = originalSpeed;

         isAttack = false;

     }

     void Attack2()
     {
         navMeshAgent.speed = 0f;
         anim.SetTrigger("doSwing2");
         isAttack = true;
         Debug.Log("ATTACK2");
         Invoke("AttackOut", attackDelay + 2f);
     }
      */

}
