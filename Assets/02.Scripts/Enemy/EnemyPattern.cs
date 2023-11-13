using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPattern : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private BossEyeTrail bossEye;
    private CharStats charStats;
    public MeleeWeaponTrail meleeWeaponTrail;

    public bool isAttacking = false;
    public bool isCombo = false;
    private bool isLookPlayer = false;
    private float stoppingDistance = 1f;
    private float originNavSpeed = 3.5f;
    public float hrizon_move = 0;
    private float moveDirection = 0; //�¿� �̵��� �⺻ ��
    private float detectionRange = 10f; //����� �̵��ӵ�
    private float distanceToPlayer;

    private bool isInSecondPhase = false;       // �����϶� 2������ ����
    private bool isSecondPhaseFirstPatten = true;

    private bool canEnemyRotate = true;

    public float detectionDistance = 20f; // �÷��̾� ���� ���� = ���ݽ��� ����

    private bool isSearchMode = true;
    private bool isBattleMode = false;
    public bool isInteracting = false;

    public int debugingPatten = 1;

    public GameObject skill_Particle_blood;
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
        bossEye = GetComponentInChildren<BossEyeTrail>();
        charStats = GetComponent<CharStats>();
    }

    void Update()
    {
        isCombo = animator.GetBool("isCombo");
        if(charStats.curHealth < charStats.maxHealth / 2)
        {
            isInSecondPhase = true;
        }
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
                                                , Time.deltaTime * 6.0f);
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (isSearchMode)
        {
            if (player != null)
            {
                if (distanceToPlayer <= detectionDistance)      //�÷��̾� �߰� ��
                {
                    animator.SetFloat("enemySpeed", 1f);
                    // ������ ���� ���� �ƴ϶�� ������ �����մϴ�.
                    if (!agent.isActiveAndEnabled)
                    {
                        agent.enabled = true;       //�׺�޽ø� �ƿ� ������
                    }
                    agent.destination = player.position;
                    agent.SetDestination(player.position);
                    // �÷��̾ ���� ���� ���� ���� ��
                    if (distanceToPlayer <= 2.0f)
                    {
                        isSearchMode = false;
                        isBattleMode = true;
                    }  //���� ��� ����
                }
            }
        }

        if (isBattleMode)
        {
            if (!isAttacking)
            {
                agent.isStopped = true;
                canEnemyRotate = false;
                int pattern;
                if (!isInSecondPhase)
                {
                    pattern = Random.Range(1, 4); // ���� ���, 1���� 3 �߿��� ���� ����
                }
                else
                {
                    if (isSecondPhaseFirstPatten)
                    {
                        pattern = 4;
                        isSecondPhaseFirstPatten = false;
                    }
                    else
                    {
                        pattern = Random.Range(1, 6);
                    }
                }

                pattern = debugingPatten;
                //debugingPatten = pattern;
                switch (pattern)
                {
                    case 1:
                        // ���� 1: ���� ���� ����
                        StartCoroutine(AttackPattern1());
                        break;
                    case 2:
                        // ���� 2: �������� 3��Ÿ
                        StartCoroutine(AttackPattern1_2());
                        break;
                    case 3: //���� 3: �����̵� ����
                        StartCoroutine(TeleportAndAttack());
                        break;
                    case 4:
                        // ���� 4: ���Ӱ��� ����
                        LookAtPlayer();
                        StartCoroutine(AttackPattern_GS7());
                        break;
                    case 5:
                        StartCoroutine(AttackPattern_Blood_SwordAura());
                        break;
                }
            }

            if (distanceToPlayer > 1.1f)
            {

                isSearchMode = true;
                isBattleMode = false;
            }
        }
    }

    private void LookAtPlayer()
    {
        transform.LookAt(player.position);
    }

    private void Spawned_Particle_blood()
    {
        GameObject particleInstance = Instantiate(skill_Particle_blood, this.transform.position + new Vector3(0,0.6f,0), this.transform.rotation);
        Destroy(particleInstance, 1.5f);
    }

    public void MeleeTrail_Enemy()
    {
        meleeWeaponTrail._emitTime = 0.2f;
        meleeWeaponTrail.Emit = true;
    }
    IEnumerator TeleportAndAttack()
    {
        isAttacking = true;
        Debug.Log("Test Teleport");

        // �÷��̾� �ֺ��� 8���� ��ġ ����
        Vector3[] teleportPositions = new Vector3[8];
        float moveDistance = 4.0f; // �����̵��� �÷��̾���� �Ÿ�
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45.0f;
            Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * moveDistance;
            teleportPositions[i] = player.position + offset;
        }

        // �����ϰ� �ϳ��� ��ġ ����
        Vector3 randomTeleportPosition = teleportPositions[Random.Range(0, 8)];

       

        // ���� �̵�
        transform.position = randomTeleportPosition;

        // �÷��̾ �ٶ󺸵��� ȸ��
        Vector3 lookDirection = (player.position - transform.position).normalized;
        transform.LookAt(transform.position + lookDirection);

        // ���� �׼� ���� (���� ���, AttackPattern1 ���)
        yield return StartCoroutine(AttackPattern2());
    }

    IEnumerator AttackPattern1()
    {
        Debug.Log("Test1 Start");
        isAttacking = true;

        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(3f);

        //agent.SetDestination(player.position);
        isAttacking = false;

    }
    //1-2 ���� ����
    IEnumerator AttackPattern1_2()
    {
        isAttacking = true;
        isCombo = true;
        animator.SetTrigger("Attack1_all");
        Debug.Log("Test Combo");
        yield return new WaitForSeconds(4f);
        isAttacking = false;
        isCombo = false;
    }
    IEnumerator AttackPattern_GS7() {
        isAttacking = true;
        isCombo = true;
        charStats.curHardness += 60;
        animator.SetTrigger("Attack_GS7");
        Debug.Log("Test GSGSGS7777");
        yield return new WaitForSeconds(8f);
        isAttacking = false;
        isCombo = false;
    }
    IEnumerator AttackPattern_Blood_SwordAura()
    {
        isAttacking = true;
        isCombo = true;
        animator.SetTrigger("Blood_SwordAura");
        Debug.Log("Test particle Attack");
        yield return new WaitForSeconds(4f);
        isAttacking = false;
        isCombo = false;

    }

    public void ResetAnim_Search()
    {
        isAttacking = false;
        isCombo = false;
    }

















    IEnumerator AttackPattern2()
    {
        Debug.Log("TellAttack Start");
        isAttacking = true;

        animator.SetTrigger("Attack2");
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;

    }
    //2��°
    IEnumerator Backstep()
    { //����
        Debug.Log("Test3 Start");
        isAttacking = true;
        animator.SetTrigger("Backstep");
        float targetDistance = 3f;
        Vector3 moveDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + moveDirection * targetDistance;

        agent.destination = targetPosition;
        agent.speed = 8f;

        // �̵� �Ϸ���� ���
        while (agent.remainingDistance > 0.2f)
        {
            yield return null;
        }
        agent.speed = originNavSpeed;
        yield return new WaitForSeconds(2f);
        agent.destination = player.position;
        // yield return StartCoroutine(horizontal_movement());
        isAttacking = false;
    }
    //3��°
    IEnumerator horizontal_movement()
    { //��� �̵�

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
        while (agent.remainingDistance > 0.5f)
        {
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

    private void LockEnemyAnimRootTrue()
    {
        animator.applyRootMotion = true;
    }
    private void UnLockEnemyAnimRootfalse()
    {
        animator.applyRootMotion = false;
    }
    //public void EnemyCanRotate() {
    //    canEnemyRotate = true;
    //}
    //public void EnemyNotRotate() {
    //    canEnemyRotate = false;
    //}

    void StopAgent()
    {
        canEnemyRotate = false;
        agent.isStopped = true;

    }
    void GoAgent()
    {
        canEnemyRotate = true;
        agent.isStopped = false;
    }
    void ComboEnd()
    {
        isCombo = false;
    }

    void BossEye_Enable_RootObj() {
        bossEye.EyeParticleEnable();
    }
    void BossEye_Disable_RootObj() {
        bossEye.EyeParticleDisable();
    }
}