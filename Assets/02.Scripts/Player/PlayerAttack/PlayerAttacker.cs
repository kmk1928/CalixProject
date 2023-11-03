using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    //public List<SOAttackPattern> attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_void_normalAk;   //배열을 담을 빈 배열
    public SOAttackPattern[] attackPatterns_void_Skill_1;   //배열을 담을 빈 배열
    public SOAttackPattern[] attackPatterns_void_Skill_2;   //배열을 담을 빈 배열
    public SOAttackPattern[] attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_PA;
    public SOAttackPattern[] rapidAssault;
    public SOAttackPattern[] oneSlash;
    public SOAttackPattern[] flymech;
    private float lastComboEnd;         //마지막으로 콤보가 끝난 시간
    private float lastClickTime;        //마지막으로 누른 시간
    public int currentAttackIndex = 0; //배열의 현재인덱스
    public int maxIndex;           //현재 배열의 최대인덱스
    private int endCount = 0;       //EndCombo 1번 실행용 변수
    
    public int indexValueForCalculation = 0;  //playerStats에 넘겨주는 인덱스

    public bool NA_Equipped = true;
    public bool PA_Equipped = false;

    public bool Skill_1_Equipped = true;
    public bool Skill_2_Equipped = true;

    private bool cooldownActive = false; // 쿨다운 중임을 나타내는 변수 추가

    //private bool isAttacking = false;
    public bool isNAing = false;
    public bool isSkill_1ing = false;
    public bool isSkill_2ing = false;

    private bool colliderCoroutineIsRunning = false;

    Animator anim;
    Transform playerTransform;
    PlayerController playerController;
    MeleeAreaSetup meleeAreaSetup;
    MeleeWeaponTrail meleeWeaponTrail;
    HitParticleActiveCollider hPACollider;
    private void Start() {
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
        meleeAreaSetup = GetComponent<MeleeAreaSetup>();
        meleeWeaponTrail = GetComponentInChildren<MeleeWeaponTrail>();
        hPACollider = GetComponentInChildren<HitParticleActiveCollider>();
    }
    private void FixedUpdate() {
        if (NA_Equipped) {
            attackPatterns_void_normalAk = attackPatterns_NA;
        }
        else if (PA_Equipped) {
            attackPatterns_void_normalAk = attackPatterns_PA;
        }

        if (Skill_1_Equipped) {
            attackPatterns_void_Skill_1 = rapidAssault;
        }
        else if(!Skill_1_Equipped)
        {
            attackPatterns_void_Skill_1 = flymech;
        }
        if (Skill_2_Equipped) {
            attackPatterns_void_Skill_2 = oneSlash;
        }
    }
    private void Update() {

        if (Input.GetButtonDown("Fire1") && !cooldownActive && !isSkill_1ing && !isSkill_2ing && !PlayerFlag.isInteracting && playerController.isGrounded) {
            Attack(attackPatterns_void_normalAk);
            isNAing = true;
        }
        if (isNAing) {
            ExitAttack(attackPatterns_void_normalAk);
        }
        if (Input.GetKeyDown(KeyCode.F) && !cooldownActive && !isNAing && !isSkill_2ing && !PlayerFlag.isInteracting && playerController.isGrounded) {
            Attack(attackPatterns_void_Skill_1);
            isSkill_1ing = true;
        }
        if (isSkill_1ing) {
            ExitAttack(attackPatterns_void_Skill_1);
        }
        if (Input.GetKeyDown(KeyCode.R) && !cooldownActive && !isNAing && !isSkill_1ing &&!PlayerFlag.isInteracting && playerController.isGrounded) {
            Attack(attackPatterns_void_Skill_2);
            isSkill_2ing = true;
        }
        if (isSkill_2ing) {
            ExitAttack(attackPatterns_void_Skill_2);
        }


        //Debug.Log("지금 인덱스" + indexValueForCalculation);
    }

    private void Attack(SOAttackPattern[] attackPatterns) { //현재시간 - 마지막 입력 시간이 쿨다운(보통 0.4f~0.2f)보다 클때 && 현재 인덱스가 최대 인덱스보다 작거나 같을 때  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            PlayerFlag.isAttacking = true;     //플래그 설정
            maxIndex = attackPatterns.Length - 1; //최대 인덱스 설정
            // 공격 애니메이션 재생 로직
            endCount = 0;
            //Debug.Log("-------------ㄴㅅㅁㄳtAttack------------");
            playerController.LockPlayerInput_ForAnimRootMotion();   //플레이어 이동제한
            if(maxIndex > 0) {
                if (!cooldownActive) { // 쿨다운 중이 아닐 때만 처리
                    cooldownActive = true; // 쿨다운 시작
                    StartCoroutine(PreviousIndex(attackPatterns));
                }
            }

            //공격범위 활성    
            if (!colliderCoroutineIsRunning) {
                StartCoroutine(AttackAreaActive_Cour(attackPatterns));
            }
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
                if (currentAttack.particleEffectPrefab != null) {
                    StartCoroutine(SpawnParticleLifecycle(currentAttack.particleEffectPrefab,
                                                            currentAttack.particleStartTime,
                                                            currentAttack.particleEndTime,
                                                            currentAttack));
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

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, float delay, float endTime, SOAttackPattern currentAttack) {
        yield return new WaitForSeconds(delay);
        //Transform playerTransform = GameObject.Find("Player").transform;
        Vector3 position = playerTransform.position + currentAttack.particlePosition;
        Quaternion rotation = playerTransform.rotation * Quaternion.Euler(currentAttack.particleRotation);
        Vector3 scale = currentAttack.particleScale;
        GameObject particleInstance = Instantiate(prefab, position, rotation);
        //particleInstance.transform.parent = playerTransform;  //플레이어를 부모로 지정
        particleInstance.transform.localScale = scale;
        // 지정된 endTime 시간 후에 파티클을 파괴하는 대기
        yield return new WaitForSeconds(endTime);
        // endTime 시간이 지난 후에 파티클 파괴
        Destroy(particleInstance);
    } 


    IEnumerator AttackAreaActive_Cour(SOAttackPattern[] attackPatterns) {   //공격범위 활성화
        colliderCoroutineIsRunning = true;
        yield return new WaitForSeconds(attackPatterns[currentAttackIndex].attackCollider_ActiveTime);
        meleeAreaSetup.OpenDamageCollider_Corutin();
        hPACollider.ColliderEnable();
        colliderCoroutineIsRunning = false;
        yield return null;
    }

    public void MeleeTrail() {
        meleeWeaponTrail._emitTime = 0.2f;
        meleeWeaponTrail.Emit = true;
    }

    void ExitAttack(SOAttackPattern[] attackPatterns) {     //애니메이션 시간이 90%가 넘으면

        if((anim.GetCurrentAnimatorStateInfo(0).IsTag(attackPatterns[currentAttackIndex].AnimTag) 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f) || anim.GetBool("isDamaged")==true) {
            if(endCount == 0) {
                // Invoke("EndCombo", 0.5f);
                EndCombo();
            }
        }
    }
    void EndCombo() {       //콤보 종료
        Debug.Log("EndCombo------------");
        isNAing = false;
        isSkill_1ing = false;
        isSkill_2ing = false;
        endCount = 1;
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
        playerController.UnlockPlayerInput_ForAnimRootMotion();
        PlayerFlag.isAttacking = false;     //플래그 설정
    }

    public void WorldTimeSlowDown()
    {
        StartCoroutine(WorldTimeSlowDown_C());
    }
    public void WorldTimeReset()
    {
        Time.timeScale = 1f; 
    }
    IEnumerator WorldTimeSlowDown_C()
    {
        Time.timeScale = 0.1f;// 월드 시간을 0.1로 느리게 설정
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 1f;
    }

    //void AttackAreaActive(SOAttackPattern[] attackPatterns) {       //공격범위 활성화 - meleeAreaSetup 사용한 버전
    //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime
    //            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime + 0.1f) {
    //        Debug.Log("-----AttackAreaAvtive!!!!----------!!!!!--");
    //        meleeAreaSetup.OpenDamageCllider_Corutin();
    //    }
    //}
}
