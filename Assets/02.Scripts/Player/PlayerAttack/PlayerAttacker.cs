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

    private void Attack(SOAttackPattern[] attackPatterns) { //����ð� - ������ �Է� �ð��� ��ٿ�(���� 0.4f~0.2f)���� Ŭ�� && ���� �ε����� �ִ� �ε������� �۰ų� ���� ��  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            // ���� �ִϸ��̼� ��� ����
            endCount = 0;
            //Debug.Log("-------------RotateAttack-----------");
            //playerController.StartStopRotation();
            Debug.Log("-------------��������tAttack------------");
            maxIndex = attackPatterns.Length - 1;
            playerController.LockPlayerInput_ForAnimRootMotion();   //�÷��̾� �̵�����
            //���ݹ��� Ȱ��    
            StartCoroutine(AttackAreaActive_Cour(attackPatterns));
            playerController.StartAnimRotation();
            StartCoroutine(playerController.EndAnimRotation());

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
                    StartCoroutine(SpawnParticleLifecycle(currentAttack.particleEffectPrefab, position, rotation, scale,
                                                            currentAttack.particleStartTime, currentAttack.particleEndTime));
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

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float delay, float endTime) {
        yield return new WaitForSeconds(delay);
        //Transform playerTransform = GameObject.Find("Player").transform; // �÷��̾� ������Ʈ�� ã�ų� �����մϴ�. �׽�Ʈ

        GameObject particleInstance = Instantiate(prefab, position, rotation);
        particleInstance.transform.localScale = scale;
        //particleInstance.transform.parent = playerTransform; // �׽�Ʈ

        // ������ endTime �ð� �Ŀ� ��ƼŬ�� �ı��ϴ� ���
        yield return new WaitForSeconds(endTime);
        // endTime �ð��� ���� �Ŀ� ��ƼŬ �ı�
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
            //Invoke("EndCombo", 0.05f); // 0.2���� �޺��̾�� ���� �ð�
            if(endCount == 0) {
                // Invoke("EndCombo", 0.5f);
                EndCombo();
            }
        }
    }
    void EndCombo() {       //�޺� ����
        Debug.Log("EndCombo------------");
        endCount = 1;
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
        playerController.UnlockPlayerInput_ForAnimRootMotion();
    }






    //void AttackAreaActive(SOAttackPattern[] attackPatterns) {       //���ݹ��� Ȱ��ȭ - meleeAreaSetup ����� ����
    //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime
    //            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime + 0.1f) {
    //        Debug.Log("-----AttackAreaAvtive!!!!----------!!!!!--");
    //        meleeAreaSetup.OpenDamageCllider_Corutin();
    //    }
    //}
}
