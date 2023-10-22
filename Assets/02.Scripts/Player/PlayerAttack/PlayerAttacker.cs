using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    //public List<SOAttackPattern> attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_void;   //�迭�� ���� �� �迭
    public SOAttackPattern[] attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_PA;
    private float lastComboEnd;         //���������� �޺��� ���� �ð�
    private float lastClickTime;        //���������� ���� �ð�
    public int currentAttackIndex = 0; //�迭�� �����ε���
    public int maxIndex;           //���� �迭�� �ִ��ε���
    private int endCount = 0;       //EndCombo 1�� ����� ����
    private float temp = 0;         //�ð� ���� ������Ű�� ����

    private int minValueForCalculation = 0;
    private int maxValueForCalculation = 0;
    public int indexValueForCalculation = 0;

    public bool skill_1_Equipped = true;
    public bool skill_2_Equipped = false;

    private bool cooldownActive = false; // ��ٿ� ������ ��Ÿ���� ���� �߰�


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

        maxIndex = attackPatterns_void.Length - 1; //�ִ� �ε��� ����
    }
    private void Update() {

        if (Input.GetButtonDown("Fire1") && !cooldownActive) {
            Attack(attackPatterns_void);
        }
        ExitAttack(attackPatterns_void);

        Debug.Log("���� �ε���" + indexValueForCalculation);
    }

    private void Attack(SOAttackPattern[] attackPatterns) { //����ð� - ������ �Է� �ð��� ��ٿ�(���� 0.4f~0.2f)���� Ŭ�� && ���� �ε����� �ִ� �ε������� �۰ų� ���� ��  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            // ���� �ִϸ��̼� ��� ����
            endCount = 0;
            //Debug.Log("-------------��������tAttack------------");
            playerController.LockPlayerInput_ForAnimRootMotion();   //�÷��̾� �̵�����

            if (!cooldownActive)
            { // ��ٿ� ���� �ƴ� ���� ó��
                cooldownActive = true; // ��ٿ� ����
                StartCoroutine(PreviousIndex(attackPatterns));
            }
            //���ݹ��� Ȱ��    
            StartCoroutine(AttackAreaActive_Cour(attackPatterns));
            //������ ȸ�� Ȱ��ȭ
            StartCoroutine(playerController.AnimationingRotation());
            CancelInvoke("EndCombo");
            if(Time.time - lastClickTime >= attackPatterns[currentAttackIndex].cooldown) {
                //Debug.Log("attackgogo");
                anim.runtimeAnimatorController = attackPatterns[currentAttackIndex].animatorOV;
                //�ִϸ��̼� �̸� �÷���
                anim.Play(attackPatterns[currentAttackIndex].AnimTag, 0, 0);
                //�ε������
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
        cooldownActive = false; // ��ٿ� ����
    }

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float delay, float endTime) {
        yield return new WaitForSeconds(delay);
        GameObject particleInstance = Instantiate(prefab, position, rotation);
        Transform playerTransform = GameObject.Find("Player").transform;
        particleInstance.transform.parent = playerTransform;  //�׽�Ʈ
        particleInstance.transform.localScale = scale;
        // ������ endTime �ð� �Ŀ� ��ƼŬ�� �ı��ϴ� ���
        yield return new WaitForSeconds(endTime);
        // endTime �ð��� ���� �Ŀ� ��ƼŬ �ı�
        Destroy(particleInstance);
    } 


    IEnumerator AttackAreaActive_Cour(SOAttackPattern[] attackPatterns) {   //���ݹ��� Ȱ��ȭ
        while(temp < 0.2f) {
           temp += Time.deltaTime;
           // Debug.Log(temp);
            yield return null;  
        }
        meleeAreaSetup.OpenDamageCllider_Corutin();
        temp = 0;
        yield return null;
    }

    void ExitAttack(SOAttackPattern[] attackPatterns) {     //�ִϸ��̼� �ð��� 90%�� ������
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag(attackPatterns[currentAttackIndex].AnimTag) 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f){
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
