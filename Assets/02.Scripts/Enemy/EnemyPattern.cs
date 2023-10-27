using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPattern : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool isLookPlayer = false;
    private float originNavSpeed;
    public float hrizon_move = 0;
    private float moveDirection = 0; //�¿� �̵��� �⺻ ��
    private float detectionRange = 10f; //����� �̵��ӵ�
    private float distanceToPlayer;

    public float detectionDistance = 20f; // �÷��̾� ���� ���� = ���ݽ��� ����

    private bool isSearchMode = true;
    private bool isBattleMode = false;
    public bool isInteracting = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // if(agent.velocity.sqrMagnitude >= 0.1f * 0.1f && agent.remainingDistance < 0.1f) {
        //    //�ȴ� �ִϸ��̼� ���� -- �ڿ������� ȸ���� ����
        //}
        if (agent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f)
        {
            //������Ʈ�� �̵�����
            Vector3 direction = agent.desiredVelocity;
            //ȸ������ (���ʹϾ� ����) ���� ���͸� ����
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            //�������� �Լ��� �̿��� �ε巯�� ȸ��   //�������� ���ڴ� �󸶳� ������ ȸ�� �� ������
            transform.rotation = Quaternion.Slerp(transform.rotation
                                                , targetAngle
                                                , Time.deltaTime * 5.0f);
        }

        if (isSearchMode)
        {
            if (player != null)
            {
                distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= detectionDistance)      //�÷��̾� �߰� ��
                {
                    agent.SetDestination(player.position);
                    // �÷��̾ ���� ���� ���� ���� ��
                    if (distanceToPlayer <= 4.0f)
                    {
                        isSearchMode = false;
                        isBattleMode = true;
                    }  //���� ��� ����

                }
            }
        }

        if (isBattleMode)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position); //���ݻ��·δ� �ι� �ҷ��� �������� �Ϲ� ����
            if (!isAttacking)
            {
                //���⿡ ���� �߰��ҰŸ� ����ġ�� ������ 1���� �ӽ÷� �ְ� Ȯ���ؼ� ����
                StartCoroutine(AttackPattern1());
            }

        }
        if (!isBattleMode)
        {  //�ʹ� �־����� �ٽ� �߰�
            isSearchMode = true;
            isBattleMode = false;
        }
    }

    IEnumerator AttackPattern1()
    {
        Debug.Log("Test1 Start");
        isAttacking = true;
        LockEnemyAnimRootTrue();

        animator.SetTrigger("Attack1");

        yield return new WaitForSeconds(1f);
        UnLockEnemyAnimRootfalse();
        yield return new WaitForSeconds(1f);

        // AttackPattern1�� �Ϸ�� ��, �������� horizontal_movement() �Ǵ� Backstep() ����
        int randomPattern = Random.Range(0, 2); // 0 �Ǵ� 1 ���� ����

        if (randomPattern == 0)
        {
            yield return StartCoroutine(horizontal_movement());
        }
        else
        {
            yield return StartCoroutine(Backstep());
        }
    }
    //2��°
    IEnumerator horizontal_movement()
    { //��� �̵�

        Debug.Log("Test2 Start");

        Vector3 lookForward = (transform.position + player.position).normalized;
        transform.LookAt(lookForward);

        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1; // �����ϰ� �¿� �̵� ���� ���� (-1: ����, 1: ������)
        float moveDistance = 4f; // �̵� �Ÿ�
        float moveSpeed = 2f; //  �̵� �ӵ�
        originNavSpeed = agent.speed;  //�̵��ӵ� ����
        agent.speed = moveSpeed;

        animator.SetFloat("horizon_move", moveDirection);
        // �̵� ����
        Vector3 initialPosition = transform.position;
        Vector3 localRight = transform.TransformDirection(Vector3.right); // ���� ���� x������ ���� ��ǥ
        Vector3 targetPosition = initialPosition + localRight * (moveDirection * moveDistance);
        agent.SetDestination(targetPosition);

        // �̵� �Ϸ���� ���
        while (agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        agent.speed = originNavSpeed; //�̵��ӵ� ����
        originNavSpeed = 0;
        animator.SetFloat("horizon_move", 0f);
        yield return new WaitForSeconds(2f);
        // �̵� ��
        isAttacking = false;
    }

    //3��°
    IEnumerator Backstep()
    { //����
        Debug.Log("Test3 Start");
        animator.SetTrigger("Backstep");
        float targetDistance = 2f;
        Vector3 moveDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + moveDirection * targetDistance;

        agent.SetDestination(targetPosition);
        agent.speed = 8f;

        // �̵� �Ϸ���� ���
        while (agent.remainingDistance > 0.5f) {
            yield return null;
        }
        agent.speed = 3.5f;


        yield return new WaitForSeconds(2f);


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

    private void LockEnemyAnimRootTrue()
    {
        originNavSpeed = agent.speed;
        agent.speed = 0;
        animator.applyRootMotion = true;
    }
    private void UnLockEnemyAnimRootfalse()
    {
        animator.applyRootMotion = false;
        agent.speed = originNavSpeed;
        originNavSpeed = 0;
    }
}