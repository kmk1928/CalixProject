using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    ///public List<SOAttackPattern> attackPatterns;
    public SOAttackPattern[] attackPatterns;

    private float lastComboEnd;
    private float lastClickTime;
    private float lastAttackTime;
    private int comboCount; // ���� �޺� Ƚ��
    private int currentAttackIndex = 0;
    private bool canCombo = false; // �޺� ���� ����
    private int comboCounter = 0;
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

    private void Attack() { ///Time.time - lastComboEnd > attackPatterns[currentAttackIndex].cooldown)
        if (Time.time - lastComboEnd > attackPatterns[currentAttackIndex].cooldown && currentAttackIndex <= maxIndex) {
            // ���� �ִϸ��̼� ��� �� ������ ���� ����
            CancelInvoke("EndCombo");

            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                anim.runtimeAnimatorController = attackPatterns[currentAttackIndex].animatorOV;
                anim.Play("NormalAttack", 0, 0);
                #region ��ƼŬ ȿ�� ����
                SOAttackPattern currentAttack = attackPatterns[currentAttackIndex];
                if (currentAttack.particleEffectPrefab != null) {
                    Instantiate(currentAttack.particleEffectPrefab, transform.position, Quaternion.identity);
                }
                #endregion
                currentAttackIndex++;
                lastClickTime = Time.time;

                if(currentAttackIndex > maxIndex) {
                    currentAttackIndex = 0;
                }
            }

            lastComboEnd = Time.time;
            canCombo = true;
            ///currentAttackIndex = 1;

        }
    }

    void ExitAttack() {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && 
            anim.GetCurrentAnimatorStateInfo(0).IsTag("normalAttack")){
            Invoke("EndCombo", 1f); // 1���� �޺��̾�� ���� �ð�
        }
    }
    void EndCombo() {
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
    }

}
