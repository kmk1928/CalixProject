using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    ///public List<SOAttackPattern> attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_PA;
    private float lastComboEnd;
    private float lastClickTime;
    private float lastAttackTime;
    private int comboCount; // 현재 콤보 횟수
    private int currentAttackIndex = 0;
    private bool canCombo = false; // 콤보 가능 여부
    private int maxIndex;
    private int endCount = 0;
    private float temp = 0;

    public bool skill_1_Equipped = true;
    public bool skill_2_Equipped = false;



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
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (skill_1_Equipped) {
                Attack(attackPatterns_NA);
            }
            else if (skill_2_Equipped) {
                Attack(attackPatterns_PA);
            }
        }
        ExitAttack();

    }

    private void Attack(SOAttackPattern[] attackPatterns) { //현재시간 - 마지막 입력 시간이 쿨다운(보통 0.4f~0.2f)보다 클때 && 현재 인덱스가 최대 인덱스보다 작거나 같을 때  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            // 공격 애니메이션 재생 로직
            endCount = 0;
            //Debug.Log("-------------RotateAttack-----------");
            //playerController.StartStopRotation();
            Debug.Log("-------------ㄴㅅㅁㄳtAttack------------");
            maxIndex = attackPatterns.Length - 1;
            playerController.LockPlayerInput_ForAnimRootMotion();   //플레이어 이동제한
            //공격범위 활성    
            StartCoroutine(AttackAreaActive_Cour(attackPatterns));
            playerController.StartAnimRotation();
            StartCoroutine(playerController.EndAnimRotation());

            CancelInvoke("EndCombo");
            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                Debug.Log("attackgogo");
                anim.runtimeAnimatorController = attackPatterns[currentAttackIndex].animatorOV;
                anim.Play("NormalAttack", 0, 0);
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
                    Debug.Log("------------초기화!");
                }
            }
        }
    }

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float delay, float endTime) {
        yield return new WaitForSeconds(delay);
        //Transform playerTransform = GameObject.Find("Player").transform; // 플레이어 오브젝트를 찾거나 지정합니다. 테스트

        GameObject particleInstance = Instantiate(prefab, position, rotation);
        particleInstance.transform.localScale = scale;
        //particleInstance.transform.parent = playerTransform; // 테스트

        // 지정된 endTime 시간 후에 파티클을 파괴하는 대기
        yield return new WaitForSeconds(endTime);
        // endTime 시간이 지난 후에 파티클 파괴
        Destroy(particleInstance);
    } 


    IEnumerator AttackAreaActive_Cour(SOAttackPattern[] attackPatterns) {
        while(temp < 0.2f) {
           temp += Time.deltaTime;
           // Debug.Log(temp);
            yield return null;  
        }
        meleeAreaSetup.OpenDamageCllider_Corutin();
        temp = 0;
        yield return null;
    }

    void ExitAttack() {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("NormalAttack") 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f  
            ){
            //Debug.Log("-------------ExitAttack------------");
            //Invoke("EndCombo", 0.05f); // 0.2초의 콤보이어가기 가능 시간
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
