using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // 순찰 지점 배열
    public Transform player; // 플레이어 오브젝트 참조
    private NavMeshAgent navMeshAgent;
    private Vector3 originalPosition;
    private bool isPatrolling = true;
    public float detectionDistance = 10f; // 플레이어 감지 거리

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        SetDestinationToNextPatrolPoint();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                // 플레이어가 일정 거리 이내에 있으면 추적
                navMeshAgent.SetDestination(player.position);
                isPatrolling = false;
            }
            else if (!isPatrolling)
            {
                // 플레이어가 일정 거리 밖에 있고 추적 중이면 순찰 지점으로 돌아가기
                navMeshAgent.SetDestination(originalPosition);
                if (navMeshAgent.remainingDistance < 0.5f)
                {
                    isPatrolling = true;
                }
            }
        }
        else if (!isPatrolling)
        {
            // 플레이어가 없고 추적 중이면 순찰 지점으로 돌아가기
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
}