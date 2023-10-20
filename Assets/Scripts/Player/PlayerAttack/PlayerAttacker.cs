using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    ///public List<SOAttackPattern> attackPatterns;
    public SOAttackPattern[] attackPatterns;

    private float lastComboEnd;
    private float lastClickTime;
    private float lastAttackTime;
    private int comboCount; // 현재 콤보 횟수
    private int currentAttackIndex = 0;
    private bool canCombo = false; // 콤보 가능 여부
    int maxIndex;

    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        maxIndex = attackPatterns.Length - 1;
    }
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Attack();
        }
        ExitAttack();
    }

    private void Attack() { //현재시간 - 마지막 입력 시간이 쿨다운(보통 0.4f~0.2f)보다 클때 && 현재 인덱스가 최대 인덱스보다 작거나 같을 때  
        if (Time.time - lastComboEnd > attackPatterns[currentAttackIndex].cooldown && currentAttackIndex <= maxIndex) {
            // 공격 애니메이션 재생 및 데미지 적용 로직
            Debug.Log("1111");
            CancelInvoke("EndCombo");

            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                Debug.Log("attackgogo");
                anim.runtimeAnimatorController = attackPatterns[currentAttackIndex].animatorOV;
                anim.Play("NormalAttack", 0, 0);
                #region 파티클 효과 생성
                SOAttackPattern currentAttack = attackPatterns[currentAttackIndex];
                if (currentAttack.particleEffectPrefab != null) {
                    Instantiate(currentAttack.particleEffectPrefab, transform.position, Quaternion.identity);
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

    void ExitAttack() {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("NormalAttack") 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f  
            ){
            Debug.Log("-------------ExitAttack------------");
            Invoke("EndCombo", 0.2f); // 0.2초의 콤보이어가기 가능 시간
        }
    }
    void EndCombo() {       //콤보 종료
        Debug.Log("EndCombo------------");
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
    }

}
