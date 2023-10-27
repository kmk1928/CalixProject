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
    private float moveDirection = 0; //좌우 이동용 기본 값
    private float detectionRange = 10f; //후퇴용 이동속도
    private float distanceToPlayer;

    public float detectionDistance = 20f; // 플레이어 감지 범위 = 공격시작 범위

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
                                                , Time.deltaTime * 5.0f);
        }

        if (isSearchMode)
        {
            if (player != null)
            {
                distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= detectionDistance)      //플레이어 발견 시
                {
                    agent.SetDestination(player.position);
                    // 플레이어가 공격 범위 내에 있을 때
                    if (distanceToPlayer <= 4.0f)
                    {
                        isSearchMode = false;
                        isBattleMode = true;
                    }  //공격 모드 돌입

                }
            }
        }

        if (isBattleMode)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position); //지금상태로는 두번 불러서 손해지만 일반 냅둠
            if (!isAttacking)
            {
                //여기에 패턴 추가할거면 스위치문 넣으셈 1개만 임시로 넣고 확인해서 빼놈
                StartCoroutine(AttackPattern1());
            }

        }
        if (!isBattleMode)
        {  //너무 멀어지면 다시 추격
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

        // AttackPattern1이 완료된 후, 랜덤으로 horizontal_movement() 또는 Backstep() 실행
        int randomPattern = Random.Range(0, 2); // 0 또는 1 랜덤 선택

        if (randomPattern == 0)
        {
            yield return StartCoroutine(horizontal_movement());
        }
        else
        {
            yield return StartCoroutine(Backstep());
        }
    }
    //2번째
    IEnumerator horizontal_movement()
    { //경계 이동

        Debug.Log("Test2 Start");

        Vector3 lookForward = (transform.position + player.position).normalized;
        transform.LookAt(lookForward);

        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1; // 랜덤하게 좌우 이동 방향 선택 (-1: 왼쪽, 1: 오른쪽)
        float moveDistance = 4f; // 이동 거리
        float moveSpeed = 2f; //  이동 속도
        originNavSpeed = agent.speed;  //이동속도 변경
        agent.speed = moveSpeed;

        animator.SetFloat("horizon_move", moveDirection);
        // 이동 시작
        Vector3 initialPosition = transform.position;
        Vector3 localRight = transform.TransformDirection(Vector3.right); // 현재 로컬 x방향의 세계 좌표
        Vector3 targetPosition = initialPosition + localRight * (moveDirection * moveDistance);
        agent.SetDestination(targetPosition);

        // 이동 완료까지 대기
        while (agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        agent.speed = originNavSpeed; //이동속도 복구
        originNavSpeed = 0;
        animator.SetFloat("horizon_move", 0f);
        yield return new WaitForSeconds(2f);
        // 이동 끝
        isAttacking = false;
    }

    //3번째
    IEnumerator Backstep()
    { //후퇴
        Debug.Log("Test3 Start");
        animator.SetTrigger("Backstep");
        float targetDistance = 2f;
        Vector3 moveDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + moveDirection * targetDistance;

        agent.SetDestination(targetPosition);
        agent.speed = 8f;

        // 이동 완료까지 대기
        while (agent.remainingDistance > 0.5f) {
            yield return null;
        }
        agent.speed = 3.5f;


        yield return new WaitForSeconds(2f);


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