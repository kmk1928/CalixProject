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

    public float gizmosDrawRange = 5.0f;    //OnDrawGizmos() 범위 표시용 float(거리)
    public float enemyAttackRange = 1.4f;    //OnDrawGizmos() 범위 표시용 float(거리)

    CharCombat combat;

    void OnDrawGizmos() {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, gizmosDrawRange);
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
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                // 플레이어 따라가기
                navMeshAgent.SetDestination(player.position);
                isPatrolling = false;
                anim.SetBool("isRun", true); // 달리기 애니메이션

                // 플레이어가 감지 범위 내에 있을 때 공격
                if (distanceToPlayer <= 2.0f && !isAttack)
                {
                    int randomAttack = Random.Range(0, 2);

                    if (randomAttack == 0)
                    {
                        Attack();
                    }
                    else
                    {
                        Attack2();
                    }
                }
            }
            else if (!isPatrolling)
            {
                // 플레이어가 감지 범위 밖으로 나갔을 때 원래 위치로 돌아가기
                navMeshAgent.SetDestination(originalPosition);
                anim.SetBool("isRun", false); // 대기 애니메이션
                if (navMeshAgent.remainingDistance < 0.2f)
                {
                    isPatrolling = true;
                }
            }
        }
        else if (!isPatrolling)
        {
            // 플레이어가 없을 때 원래 위치로 돌아가기
            navMeshAgent.SetDestination(originalPosition);
            if (navMeshAgent.remainingDistance < 0.5f)
            {
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
        Invoke("AttackOut", attackDelay + 1f);
    }


}
