using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // ���� ���� �迭
    public Transform player; // �÷��̾� ������Ʈ ����
    private NavMeshAgent navMeshAgent;
    private Vector3 originalPosition;
    private bool isPatrolling = true;
    public float detectionDistance = 10f; // �÷��̾� ���� �Ÿ�

    Animator anim;//animation variable

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        SetDestinationToNextPatrolPoint();

        anim = GetComponentInChildren<Animator>();//animation
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                // �÷��̾ ���� �Ÿ� �̳��� ������ ����
                navMeshAgent.SetDestination(player.position);
                isPatrolling = false;
                anim.SetBool("isRun",true);//run animation
            }
            else if (!isPatrolling)
            {
                // �÷��̾ ���� �Ÿ� �ۿ� �ְ� ���� ���̸� ���� �������� ���ư���
                navMeshAgent.SetDestination(originalPosition);
                anim.SetBool("isRun",false);//able animation
                if (navMeshAgent.remainingDistance < 0.5f)
                {
                    isPatrolling = true;
                }
            }
        }
        else if (!isPatrolling)
        {
            // �÷��̾ ���� ���� ���̸� ���� �������� ���ư���
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