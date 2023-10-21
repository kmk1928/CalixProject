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
    private int comboCount; // ���� �޺� Ƚ��
    private int currentAttackIndex = 0;
    private bool canCombo = false; // �޺� ���� ����
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

    private void Attack(SOAttackPattern[] attackPatterns) { //����ð� - ������ �Է� �ð��� ��ٿ�(���� 0.4f~0.2f)���� Ŭ�� && ���� �ε����� �ִ� �ε������� �۰ų� ���� ��  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            // ���� �ִϸ��̼� ��� ����
            maxIndex = attackPatterns.Length - 1;
            playerController.LockPlayerInput_ForAnimRootMotion();   //�÷��̾� �̵�����
            CancelInvoke("EndCombo");
            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                Debug.Log("attackgogo");
                anim.runtimeAnimatorController = attackPatterns[currentAttackIndex].animatorOV;
                anim.Play("NormalAttack", 0, 0);
                #region ��ƼŬ ȿ�� ����
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
                    Debug.Log("------------�ʱ�ȭ!");
                    playerController.UnlockPlayerInput_ForAnimRootMotion();
                }
            }
        }
    }

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float delay, float endTime) {
        yield return new WaitForSeconds(delay);
        GameObject particleInstance = Instantiate(prefab, position, rotation);
        particleInstance.transform.localScale = scale;

        // ������ endTime �ð� �Ŀ� ��ƼŬ�� �ı��ϴ� ���
        yield return new WaitForSeconds(endTime);
        // endTime �ð��� ���� �Ŀ� ��ƼŬ �ı�
        Destroy(particleInstance);
    }

    void ExitAttack() {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("NormalAttack") 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f  
            ){
            Debug.Log("-------------ExitAttack------------");
            Invoke("EndCombo", 0.01f); // 0.2���� �޺��̾�� ���� �ð�
        }
    }
    void EndCombo() {       //�޺� ����
        Debug.Log("EndCombo------------");
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
        playerController.UnlockPlayerInput_ForAnimRootMotion();
    }

}
