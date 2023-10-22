using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    //public List<SOAttackPattern> attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_void;   //배열을 담을 빈 배열
    public SOAttackPattern[] attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_PA;
    private float lastComboEnd;         //마지막으로 콤보가 끝난 시간
    private float lastClickTime;        //마지막으로 누른 시간
    public int currentAttackIndex = 0; //배열의 현재인덱스
    public int maxIndex;           //현재 배열의 최대인덱스
    private int endCount = 0;       //EndCombo 1번 실행용 변수
    private float temp = 0;         //시간 계산용 증가시키는 변수

    private int minValueForCalculation = 0;
    private int maxValueForCalculation = 0;
    public int indexValueForCalculation = 0;

    public bool skill_1_Equipped = true;
    public bool skill_2_Equipped = false;

    private bool cooldownActive = false; // 쿨다운 중임을 나타내는 변수 추가


    Animator anim;
    Transform playerTransform;
    PlayerController playerController;
    MeleeAreaSetup meleeAreaSetup;

    private void Start() {
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
        meleeAreaSetup = GetComponent<MeleeAreaSetup>();
    }
    private void FixedUpdate() {
        if (skill_1_Equipped) {
            attackPatterns_void = attackPatterns_NA;
        }
        else if (skill_2_Equipped) {
            attackPatterns_void = attackPatterns_PA;
        }

        maxIndex = attackPatterns_void.Length - 1; //최대 인덱스 설정
    }
    private void Update() {

        if (Input.GetButtonDown("Fire1") && !cooldownActive) {
            Attack(attackPatterns_void);
        }
        ExitAttack(attackPatterns_void);

        Debug.Log("지금 인덱스" + indexValueForCalculation);
    }

    private void Attack(SOAttackPattern[] attackPatterns) { //현재시간 - 마지막 입력 시간이 쿨다운(보통 0.4f~0.2f)보다 클때 && 현재 인덱스가 최대 인덱스보다 작거나 같을 때  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            // 공격 애니메이션 재생 로직
            endCount = 0;
            //Debug.Log("-------------ㄴㅅㅁㄳtAttack------------");
            playerController.LockPlayerInput_ForAnimRootMotion();   //플레이어 이동제한

            if (!cooldownActive)
            { // 쿨다운 중이 아닐 때만 처리
                cooldownActive = true; // 쿨다운 시작
                StartCoroutine(PreviousIndex(attackPatterns));
            }
            //공격범위 활성    
            StartCoroutine(AttackAreaActive_Cour(attackPatterns));
            //공격중 회전 활성화
            StartCoroutine(playerController.AnimationingRotation());
            CancelInvoke("EndCombo");
            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                //Debug.Log("attackgogo");
                anim.runtimeAnimatorController = attackPatterns[currentAttackIndex].animatorOV;
                //애니메이션 이름 플레이
                anim.Play(attackPatterns[currentAttackIndex].AnimTag, 0, 0);
                //인덱스계산
                #region 파티클 효과 생성
                SOAttackPattern currentAttack = attackPatterns[currentAttackIndex];
                Vector3 position = playerTransform.position + currentAttack.particlePosition;
                Quaternion rotation = playerTransform.rotation * Quaternion.Euler(currentAttack.particleRotation);
                Vector3 scale = currentAttack.particleScale;
                if (currentAttack.particleEffectPrefab != null) {
                    StartCoroutine(SpawnParticleLifecycle(currentAttack.particleEffectPrefab, position, rotation, scale,
                                                            currentAttack.particleStartTime, currentAttack.particleEndTime));
                }
                #endregion
                currentAttackIndex++;
                lastClickTime = Time.time;

                if(currentAttackIndex > maxIndex) {
                    currentAttackIndex = 0;
                }
            }
        }
    }

    IEnumerator PreviousIndex(SOAttackPattern[] attackPatterns)
    {
        indexValueForCalculation = currentAttackIndex;
        yield return new WaitForSeconds(attackPatterns[indexValueForCalculation].cooldown + 0.2f);
        indexValueForCalculation++;
        if (indexValueForCalculation >= attackPatterns.Length)
        {
            indexValueForCalculation = 0;
        }
        cooldownActive = false; // 쿨다운 종료
    }

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float delay, float endTime) {
        yield return new WaitForSeconds(delay);
        GameObject particleInstance = Instantiate(prefab, position, rotation);
        Transform playerTransform = GameObject.Find("Player").transform;
        particleInstance.transform.parent = playerTransform;  //테스트
        particleInstance.transform.localScale = scale;
        // 지정된 endTime 시간 후에 파티클을 파괴하는 대기
        yield return new WaitForSeconds(endTime);
        // endTime 시간이 지난 후에 파티클 파괴
        Destroy(particleInstance);
    } 


    IEnumerator AttackAreaActive_Cour(SOAttackPattern[] attackPatterns) {   //공격범위 활성화
        while(temp < 0.2f) {
           temp += Time.deltaTime;
           // Debug.Log(temp);
            yield return null;  
        }
        meleeAreaSetup.OpenDamageCllider_Corutin();
        temp = 0;
        yield return null;
    }

    void ExitAttack(SOAttackPattern[] attackPatterns) {     //애니메이션 시간이 90%가 넘으면
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag(attackPatterns[currentAttackIndex].AnimTag) 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f){
            if(endCount == 0) {
                // Invoke("EndCombo", 0.5f);
                EndCombo();
            }
        }
    }
    void EndCombo() {       //콤보 종료
        Debug.Log("EndCombo------------");
        endCount = 1;
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
        playerController.UnlockPlayerInput_ForAnimRootMotion();
    }

    
    //void AttackAreaActive(SOAttackPattern[] attackPatterns) {       //공격범위 활성화 - meleeAreaSetup 사용한 버전
    //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime
    //            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime + 0.1f) {
    //        Debug.Log("-----AttackAreaAvtive!!!!----------!!!!!--");
    //        meleeAreaSetup.OpenDamageCllider_Corutin();
    //    }
    //}
}
