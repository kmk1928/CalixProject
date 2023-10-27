using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPattern : MonoBehaviour {
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool isLookPlayer = false;
    private float stoppingDistance = 1f;
    private float originNavSpeed = 3.5f;
    public float hrizon_move = 0;
    private float moveDirection = 0; //�¿� �̵��� �⺻ ��
    private float detectionRange = 10f; //����� �̵��ӵ�
    private float distanceToPlayer;

    private bool canEnemyRotate = true;

    private int backStepCount = 0;
    public float detectionDistance = 20f; // �÷��̾� ���� ���� = ���ݽ��� ����

    private bool isSearchMode = true;
    private bool isBattleMode = false;
    public bool isInteracting = false;

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }
    void Update() {
        // if(agent.velocity.sqrMagnitude >= 0.1f * 0.1f && agent.remainingDistance < 0.1f) {
        //    //�ȴ� �ִϸ��̼� ���� -- �ڿ������� ȸ���� ����
        //}
        if (agent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f) {
            //������Ʈ�� �̵�����
            Vector3 direction = agent.desiredVelocity;
            //ȸ������ (���ʹϾ� ����) ���� ���͸� ����
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            //�������� �Լ��� �̿��� �ε巯�� ȸ��   //�������� ���ڴ� �󸶳� ������ ȸ�� �� ������
            transform.rotation = Quaternion.Slerp(transform.rotation
                                                , targetAngle
                                                , Time.deltaTime * 6.0f);
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (isSearchMode) {
            if (player != null) {
                if (distanceToPlayer <= detectionDistance)      //�÷��̾� �߰� ��
                {
                    animator.SetFloat("enemySpeed", 1f);
                    agent.SetDestination(player.position);
                    // �÷��̾ ���� ���� ���� ���� ��
                    if (distanceToPlayer <= 4.0f) {
                        isSearchMode = false;
                        isBattleMode = true;
                    }  //���� ��� ����

                }
            }
        }

        //if (distanceToPlayer <= stoppingDistance) {
        //    agent.isStopped = true;
        //}
        //else {
        //    agent.isStopped = false;
        //    agent.SetDestination( player.position);
        //}

        if (isBattleMode) {
            if (!isAttacking) {
                agent.isStopped = true;
                canEnemyRotate = false;
                animator.SetFloat("enemySpeed", 0f);
                int pattern = 1; //Random.Range(1, 4); // ���� ���, 1���� 3 �߿��� ���� ����
                //int backCount = Random.Range(0, 16);
                //if (backStepCount > backCount) {
                //    pattern = 3;
                //    StartCoroutine(Backstep());
                //}
                switch (pattern) {
                    case 1:
                        // ���� 1: ���� ����
                        BackStepCountPlus();
                        StartCoroutine(AttackPattern1());
                        break;
                    case 2:
                        // ���� 2: �̵� ����
                        //StartCoroutine(horizontal_movement());
                        //int ranana = Random.Range(1, 3);
                        break;
                    case 3:
                        // ���� 3: ���� ����

                        break;
                }
            }

            if(distanceToPlayer > 4.1f) {

                isSearchMode = true;
                isBattleMode = false;
            }
        }
    }

    IEnumerator AttackPattern1() {
        Debug.Log("Test1 Start");
        isAttacking = true;
       // LockEnemyAnimRootTrue();

        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.8f);
       // UnLockEnemyAnimRootfalse();
        yield return new WaitForSeconds(1f);

        //agent.SetDestination(player.position);
        isAttacking = false;

    }
    //2��°
    IEnumerator Backstep() { //����
        Debug.Log("Test3 Start");
        isAttacking = true;
        animator.SetTrigger("Backstep");
        float targetDistance = 3f;
        Vector3 moveDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + moveDirection * targetDistance;

        agent.destination = targetPosition; 
        agent.speed = 8f;

        // �̵� �Ϸ���� ���
        while (agent.remainingDistance > 0.2f) {
            yield return null;
        }
        agent.speed = originNavSpeed;
        yield return new WaitForSeconds(2f);
        agent.destination = player.position;
        // yield return StartCoroutine(horizontal_movement());
        isAttacking = false;
    }
    //3��°
    IEnumerator horizontal_movement() { //��� �̵�

        Debug.Log("Test2 Start");

        Vector3 lookForward = (transform.position + player.position).normalized;
        transform.LookAt(lookForward);

        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1; // �����ϰ� �¿� �̵� ���� ���� (-1: ����, 1: ������)
        float moveDistance = 2f; // �̵� �Ÿ�
        float moveSpeed = 1.5f; //  �̵� �ӵ�
        agent.speed = moveSpeed;

        animator.SetFloat("horizon_move", moveDirection);
        // �̵� ����
        Vector3 initialPosition = transform.position;
        Vector3 localRight = transform.TransformDirection(Vector3.right); // ���� ���� x������ ���� ��ǥ
        Vector3 targetPosition = initialPosition + localRight * (moveDirection * moveDistance);
        agent.destination = targetPosition;

        // �̵� �Ϸ���� ���
        while (agent.remainingDistance > 0.5f) {
            yield return null;
        }

        agent.speed = originNavSpeed; //�̵��ӵ� ����
        animator.SetFloat("horizon_move", 0f);
        yield return new WaitForSeconds(3f);
        agent.destination = player.position;
        // �̵� ��
        isAttacking = false;
    }


    /*
    IEnumerator CurveMove() {//Ŀ������
                                                                                           // �̵� ����� ���� ���
        float journeyLength = Vector3.Distance(startPos, endPos);
        // �̵� ���� �ð� ���
        float startTime = Time.time;
        // �̵��� �Ÿ� �ʱ�ȭ
        float distanceCovered = 0f;

        //// Raycast�� �̿��� �� ����
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, moveDirection, out hit, targetDistance, layerMaskForWalls)) {
        //    // ��ֹ�(��)�� �����Ǿ��ٸ�, �� �տ� �����ϵ��� ��ǥ ��ġ ����
        //    targetPosition = hit.point - moveDirection * 0.5f; // ������ ���� �Ÿ���ŭ �ڷ� �̵�
        //}

        while (distanceCovered < journeyLength) {
            // ��������� �̵� �ð��� ���
            float journeyTime = (Time.time - startTime) / duration;
            // ��� ���̸� ��� (�� �ڵ�� ��� �׸��� ���� ����)
            float heightOffset = height * 4.0f * journeyTime * (1 - journeyTime);
            // ���� ��ġ�� ���� ������ ����Ͽ� ���� (Vector3.Lerp �Լ��� ����Ͽ� ���� ��ġ�� �� ��ġ ���̸� �̵�)
            transform.position = Vector3.Lerp(startPos, endPos, journeyTime) + new Vector3(0f, heightOffset, 0f);
            // ������� �̵��� �Ÿ� ����
            distanceCovered = journeyLength * journeyTime;

            // ���� �����ӱ��� ���
            yield return null;
        }
        // �̵��� �Ϸ�Ǹ� ���� ������ �����ϰų� �ڷ�ƾ�� ����
    }

    */

    private void LockEnemyAnimRootTrue() {
        animator.applyRootMotion = true;
    }
    private void UnLockEnemyAnimRootfalse() {
        animator.applyRootMotion = false;
    }
    public void EnemyCanRotate() {
        canEnemyRotate = true;
    }
    public void EnemyNotRotate() {
        canEnemyRotate = false;
    }
    private void BackStepCountPlus() {
        backStepCount += 1;
    }
    void StopAgent() {
        canEnemyRotate = false;
        agent.isStopped = true;
    }
    void GoAgent() {
        canEnemyRotate = true;
        agent.isStopped = false;
    }
}