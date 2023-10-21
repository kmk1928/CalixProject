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

    public bool skill1Equipped = true;

    Animator anim;
    Transform playerTransform;
    PlayerController playerController;

    private void Start() {
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (skill1Equipped) {
                Attack(attackPatterns_NA);
            }
        }
        ExitAttack();
    }

    private void Attack(SOAttackPattern[] attackPatterns) { //현재시간 - 마지막 입력 시간이 쿨다운(보통 0.4f~0.2f)보다 클때 && 현재 인덱스가 최대 인덱스보다 작거나 같을 때  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            // 공격 애니메이션 재생 로직
            maxIndex = attackPatterns.Length - 1;
            playerController.LockPlayerInput_ForAnimRootMotion();   //플레이어 이동제한
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
                    //GameObject particleInstance = Instantiate(currentAttack.particleEffectPrefab, position, rotation);//.transform.localScale = scale;
                    //StartCoroutine(DestroyParticleAfterTime(particleInstance, currentAttack.particleEndTime)); 
                    StartCoroutine(SpawnParticleLifecycle(currentAttack.particleEffectPrefab, position, rotation, scale,
                                                            currentAttack.particleStartTime, currentAttack.particleEndTime));
                }
                #endregion
                currentAttackIndex++;
                lastClickTime = Time.time;

                if(currentAttackIndex > maxIndex) {
                    currentAttackIndex = 0;
                    Debug.Log("------------초기화!");
                    playerController.UnlockPlayerInput_ForAnimRootMotion();
                }
            }
        }
    }

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float delay, float endTime) {
        yield return new WaitForSeconds(delay);
        GameObject particleInstance = Instantiate(prefab, position, rotation);
        particleInstance.transform.localScale = scale;

        // 지정된 endTime 시간 후에 파티클을 파괴하는 대기
        yield return new WaitForSeconds(endTime);
        // endTime 시간이 지난 후에 파티클 파괴
        Destroy(particleInstance);
    }

    void ExitAttack() {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("NormalAttack") 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f  
            ){
            Debug.Log("-------------ExitAttack------------");
            Invoke("EndCombo", 0.01f); // 0.2초의 콤보이어가기 가능 시간
        }
    }
    void EndCombo() {       //콤보 종료
        Debug.Log("EndCombo------------");
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
        playerController.UnlockPlayerInput_ForAnimRootMotion();
    }

}
