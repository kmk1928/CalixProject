using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour {
    //public List<SOAttackPattern> attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_void_normalAk;   //�迭�� ���� �� �迭
    public SOAttackPattern[] attackPatterns_void_Skill_1;   //�迭�� ���� �� �迭
    public SOAttackPattern[] attackPatterns_void_Skill_2;   //�迭�� ���� �� �迭
    public SOAttackPattern[] attackPatterns_NA;
    public SOAttackPattern[] attackPatterns_PA;
    public SOAttackPattern[] rapidAssault;
    public SOAttackPattern[] oneSlash;
    public SOAttackPattern[] flymech;
    private float lastComboEnd;         //���������� �޺��� ���� �ð�
    private float lastClickTime;        //���������� ���� �ð�
    public int currentAttackIndex = 0; //�迭�� �����ε���
    public int maxIndex;           //���� �迭�� �ִ��ε���
    private int endCount = 0;       //EndCombo 1�� ����� ����
    
    public int indexValueForCalculation = 0;  //playerStats�� �Ѱ��ִ� �ε���

    public bool NA_Equipped = true;
    public bool PA_Equipped = false;

    public bool Skill_1_Equipped = true;
    public bool Skill_2_Equipped = true;

    private bool cooldownActive = false; // ��ٿ� ������ ��Ÿ���� ���� �߰�

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


        //Debug.Log("���� �ε���" + indexValueForCalculation);
    }

    private void Attack(SOAttackPattern[] attackPatterns) { //����ð� - ������ �Է� �ð��� ��ٿ�(���� 0.4f~0.2f)���� Ŭ�� && ���� �ε����� �ִ� �ε������� �۰ų� ���� ��  
        if (Time.time - lastComboEnd > 0.1f && currentAttackIndex <= maxIndex) {
            PlayerFlag.isAttacking = true;     //�÷��� ����
            maxIndex = attackPatterns.Length - 1; //�ִ� �ε��� ����
            // ���� �ִϸ��̼� ��� ����
            endCount = 0;
            //Debug.Log("-------------��������tAttack------------");
            playerController.LockPlayerInput_ForAnimRootMotion();   //�÷��̾� �̵�����
            if(maxIndex > 0) {
                if (!cooldownActive) { // ��ٿ� ���� �ƴ� ���� ó��
                    cooldownActive = true; // ��ٿ� ����
                    StartCoroutine(PreviousIndex(attackPatterns));
                }
            }

            //���ݹ��� Ȱ��    
            if (!colliderCoroutineIsRunning) {
                StartCoroutine(AttackAreaActive_Cour(attackPatterns));
            }
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
        cooldownActive = false; // ��ٿ� ����
    }

    private IEnumerator SpawnParticleLifecycle(GameObject prefab, float delay, float endTime, SOAttackPattern currentAttack) {
        yield return new WaitForSeconds(delay);
        //Transform playerTransform = GameObject.Find("Player").transform;
        Vector3 position = playerTransform.position + currentAttack.particlePosition;
        Quaternion rotation = playerTransform.rotation * Quaternion.Euler(currentAttack.particleRotation);
        Vector3 scale = currentAttack.particleScale;
        GameObject particleInstance = Instantiate(prefab, position, rotation);
        //particleInstance.transform.parent = playerTransform;  //�÷��̾ �θ�� ����
        particleInstance.transform.localScale = scale;
        // ������ endTime �ð� �Ŀ� ��ƼŬ�� �ı��ϴ� ���
        yield return new WaitForSeconds(endTime);
        // endTime �ð��� ���� �Ŀ� ��ƼŬ �ı�
        Destroy(particleInstance);
    } 


    IEnumerator AttackAreaActive_Cour(SOAttackPattern[] attackPatterns) {   //���ݹ��� Ȱ��ȭ
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

    void ExitAttack(SOAttackPattern[] attackPatterns) {     //�ִϸ��̼� �ð��� 90%�� ������

        if((anim.GetCurrentAnimatorStateInfo(0).IsTag(attackPatterns[currentAttackIndex].AnimTag) 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f) || anim.GetBool("isDamaged")==true) {
            if(endCount == 0) {
                // Invoke("EndCombo", 0.5f);
                EndCombo();
            }
        }
    }
    void EndCombo() {       //�޺� ����
        Debug.Log("EndCombo------------");
        isNAing = false;
        isSkill_1ing = false;
        isSkill_2ing = false;
        endCount = 1;
        currentAttackIndex = 0;
        lastComboEnd = Time.time;
        playerController.UnlockPlayerInput_ForAnimRootMotion();
        PlayerFlag.isAttacking = false;     //�÷��� ����
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
        Time.timeScale = 0.1f;// ���� �ð��� 0.1�� ������ ����
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 1f;
    }

    //void AttackAreaActive(SOAttackPattern[] attackPatterns) {       //���ݹ��� Ȱ��ȭ - meleeAreaSetup ����� ����
    //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime
    //            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > attackPatterns[currentAttackIndex].attackCollider_ActiveTime + 0.1f) {
    //        Debug.Log("-----AttackAreaAvtive!!!!----------!!!!!--");
    //        meleeAreaSetup.OpenDamageCllider_Corutin();
    //    }
    //}
}
