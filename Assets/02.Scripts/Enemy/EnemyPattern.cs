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
    private float moveDirection = 0; //좌우 이동용 기본 값
    private float detectionRange = 10f; //후퇴용 이동속도
    private float distanceToPlayer;

    private bool isInSecondPhase = false;       // 반피일때 2페이즈 돌입
    private bool isSecondPhaseFirstPatten = true;

    private bool canEnemyRotate = true;

    public float detectionDistance = 20f; // 플레이어 감지 범위 = 공격시작 범위

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
        //    //걷는 애니메이션 중지 -- 자연스러운 회전을 위함
        //}
        if (agent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f)
        {
            //에이전트의 이동방향
            Vector3 direction = agent.desiredVelocity;
            //회전각도 (쿼터니언 산출) 위의 벡터를 변형
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            //선형보간 함수를 이용해 부드러운 회전   //마지막의 숫자는 얼마나 빠르게 회전 할 것인지
            transform.rotation = Quaternion.Slerp(transform.rotation
                                                , targetAngle
                                                , Time.deltaTime * 6.0f);
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (isSearchMode)
        {
            if (player != null)
            {
                if (distanceToPlayer <= detectionDistance)      //플레이어 발견 시
                {
                    animator.SetFloat("enemySpeed", 1f);
                    // 패턴이 실행 중이 아니라면 추적을 시작합니다.
                    if (!agent.isActiveAndEnabled)
                    {
                        agent.enabled = true;       //네비메시를 아예 꺼버림
                    }
                    agent.destination = player.position;
                    agent.SetDestination(player.position);
                    // 플레이어가 공격 범위 내에 있을 때
                    if (distanceToPlayer <= 2.0f)
                    {
                        isSearchMode = false;
                        isBattleMode = true;
                    }  //공격 모드 돌입
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
                    pattern = Random.Range(1, 4); // 예를 들어, 1에서 3 중에서 랜덤 선택
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
                        // 패턴 1: 공격 패턴 몸빵
                        StartCoroutine(AttackPattern1());
                        break;
                    case 2:
                        // 패턴 2: 공격패턴 3연타
                        StartCoroutine(AttackPattern1_2());
                        break;
                    case 3: //패턴 3: 순간이동 공격
                        StartCoroutine(TeleportAndAttack());
                        break;
                    case 4:
                        // 패턴 4: 연속공격 패턴
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

        // 플레이어 주변의 8개의 위치 생성
        Vector3[] teleportPositions = new Vector3[8];
        float moveDistance = 4.0f; // 순간이동시 플레이어와의 거리
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45.0f;
            Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * moveDistance;
            teleportPositions[i] = player.position + offset;
        }

        // 랜덤하게 하나의 위치 선택
        Vector3 randomTeleportPosition = teleportPositions[Random.Range(0, 8)];

       

        // 순간 이동
        transform.position = randomTeleportPosition;

        // 플레이어를 바라보도록 회전
        Vector3 lookDirection = (player.position - transform.position).normalized;
        transform.LookAt(transform.position + lookDirection);

        // 공격 액션 수행 (예를 들어, AttackPattern1 사용)
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
    //1-2 연속 공격
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
    //2번째
    IEnumerator Backstep()
    { //후퇴
        Debug.Log("Test3 Start");
        isAttacking = true;
        animator.SetTrigger("Backstep");
        float targetDistance = 3f;
        Vector3 moveDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + moveDirection * targetDistance;

        agent.destination = targetPosition;
        agent.speed = 8f;

        // 이동 완료까지 대기
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
    //3번째
    IEnumerator horizontal_movement()
    { //경계 이동

        Debug.Log("Test2 Start");

        Vector3 lookForward = (transform.position + player.position).normalized;
        transform.LookAt(lookForward);

        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1; // 랜덤하게 좌우 이동 방향 선택 (-1: 왼쪽, 1: 오른쪽)
        float moveDistance = 2f; // 이동 거리
        float moveSpeed = 1.5f; //  이동 속도
        agent.speed = moveSpeed;

        animator.SetFloat("horizon_move", moveDirection);
        // 이동 시작
        Vector3 initialPosition = transform.position;
        Vector3 localRight = transform.TransformDirection(Vector3.right); // 현재 로컬 x방향의 세계 좌표
        Vector3 targetPosition = initialPosition + localRight * (moveDirection * moveDistance);
        agent.destination = targetPosition;

        // 이동 완료까지 대기
        while (agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        agent.speed = originNavSpeed; //이동속도 복구
        animator.SetFloat("horizon_move", 0f);
        yield return new WaitForSeconds(3f);
        agent.destination = player.position;
        // 이동 끝
        isAttacking = false;
    }


    /*
    IEnumerator CurveMove() {//커브후퇴
                                                                                           // 이동 경로의 길이 계산
        float journeyLength = Vector3.Distance(startPos, endPos);
        // 이동 시작 시간 기록
        float startTime = Time.time;
        // 이동한 거리 초기화
        float distanceCovered = 0f;

        //// Raycast를 이용해 벽 감지
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, moveDirection, out hit, targetDistance, layerMaskForWalls)) {
        //    // 장애물(벽)이 감지되었다면, 벽 앞에 정지하도록 목표 위치 조정
        //    targetPosition = hit.point - moveDirection * 0.5f; // 벽에서 일정 거리만큼 뒤로 이동
        //}

        while (distanceCovered < journeyLength) {
            // 현재까지의 이동 시간을 계산
            float journeyTime = (Time.time - startTime) / duration;
            // 곡선의 높이를 계산 (이 코드는 곡선을 그리기 위한 수식)
            float heightOffset = height * 4.0f * journeyTime * (1 - journeyTime);
            // 현재 위치를 선형 보간을 사용하여 갱신 (Vector3.Lerp 함수를 사용하여 시작 위치와 끝 위치 사이를 이동)
            transform.position = Vector3.Lerp(startPos, endPos, journeyTime) + new Vector3(0f, heightOffset, 0f);
            // 현재까지 이동한 거리 갱신
            distanceCovered = journeyLength * journeyTime;

            // 다음 프레임까지 대기
            yield return null;
        }
        // 이동이 완료되면 다음 동작을 수행하거나 코루틴을 종료
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