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

    private void Attack() { //����ð� - ������ �Է� �ð��� ��ٿ�(���� 0.4f~0.2f)���� Ŭ�� && ���� �ε����� �ִ� �ε������� �۰ų� ���� ��  
        if (Time.time - lastComboEnd > attackPatterns[currentAttackIndex].cooldown && currentAttackIndex <= maxIndex) {
            // ���� �ִϸ��̼� ��� �� ������ ���� ����
            Debug.Log("1111");
            CancelInvoke("EndCombo");

            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                Debug.Log("attackgogo");
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
                    Debug.Log("------------�ʱ�ȭ!");
                }
            }
        }
    }

    void ExitAttack() {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("NormalAttack") 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f  
            ){
            Debug.Log("-------------ExitAttack------------");
            Invoke("EndCombo", 0.2f); // 0.2���� �޺��̾�� ���� �ð�
        }
    }
    void EndCombo() {       //�޺� ����
        Debug.Log("EndCombo------------");
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
    }

}
