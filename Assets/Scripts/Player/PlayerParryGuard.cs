using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParryGuard : MonoBehaviour {

    CharCombat combat;

    [SerializeField]
    private float parryTimer = 0f;
    private bool isBlocked = false;     //��Ŭ�� ���� Ȯ�ο�
    private bool isParried = false;     //�и� ���� ���� Ȯ�ο�
    private bool isHitted = false;      //�������� �������� �Դ� ���� �����ϱ� ���� bool Ʈ����, OnDamage�ڷ�ƾ�� ���
    private bool isHittedMotioning = false;
    public bool isLockPlayerMoved = false;

    public float smoothTime = 0.5f;
    Transform original;
    SmoothMoved smoothMoved;
    GameObject nearObject;

    [Header("Parry")]
    [Tooltip("�÷��̾� �и����� ����")]
    public BoxCollider parryArea;
    [Tooltip("�и� ���� ����Ʈ")]
    public ParticleSystem parryParticle;

    [Header("power Hit")]
    [Tooltip("���� �ǰ� ����Ʈ")]
    public ParticleSystem powerHittedParticle;
    
    Animator anim;  //animation variable

    PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<CharCombat>();
        anim = GetComponentInChildren<Animator>();//animation
        playerController = GetComponent<PlayerController>();
        smoothMoved = GetComponent<SmoothMoved>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(1) && !isHittedMotioning) {                  //��Ŭ�� Ű �ٿ� �� �и�
            isParried = true;                   //�и����� true�� ����
            isBlocked = true;                   //�������� true�� ����
            anim.SetBool("isGuard", true);//able animation
            playerController.speed /= 2;
        }
       //if (Input.GetMouseButton(1)) {                       //��Ŭ�� �� ���� �� ����
       //     
       // }
        if (isParried && !isHittedMotioning) {                        //�и����� true�϶� �и� ������Ȱ��ȭ�ϰ� �и��ð��� ������ �ٽ� ������ �ڵ�
            parryArea.enabled = true;
            parryTimer += Time.deltaTime;
            if (parryTimer > 0.3f) {
                parryTimer = 0;
                isParried = false;
                parryArea.enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(1)) {         //��Ŭ�� ���� �� ��������
            isBlocked = false;
            anim.SetBool("isGuard", false);//able animation
            playerController.speed = playerController.defaultSpeed;
            Debug.Log("?-? Non blocked");

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!isHitted && other.tag == "EnemyAttack" || other.tag == "EnemyPowerAttack") {
            Debug.Log("1"); //����������������
            nearObject = other.gameObject;
            isLockPlayerMoved = true;           //�ǰ��� �̵� ����
            if (isParried) {
                Debug.Log("PARRY!!!");                          //�и� ����
                OnParried();
            }
            else if (isBlocked) {                                    //��Ŭ�� ����� ���� ���� ����
                CharStats targetStatus = other.GetComponentInParent<CharStats>();
                if(targetStatus != null) {
                    combat.Guard(targetStatus);
                }
                Debug.Log("Guard!");
                OnDamage();
            }
            else {
                CharStats targetStatus = other.GetComponentInParent<CharStats>();
                    combat.Hitted(targetStatus);
                Debug.Log("Damaged");
                if(other.tag == "EnemyAttack") {
                    anim.SetTrigger("doDamage");
                    OnDamage();
                    
                }
                else if(other.tag == "EnemyPowerAttack") {
                    anim.SetTrigger("doDamage_Power");
                    OnPowerDamage();
                    
                }
                
            }

        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag == "EnemyAttack" && other.tag == "EnemyPowerAttack")
            nearObject = null;
    }
    private void  OnParried() {
        isHitted = true;                        //�����ǰݹ���
        isParried = false;     
        parryTimer = 0f;    //�и����ɽð� �ʱ�ȭ
        Debug.Log("�и� ��------");
        Debug.Log("2"); //����������������
        original = this.transform;      //�ε巴�� �з���
        smoothMoved.SmoothMove_Parry(original, nearObject.transform);

        parryParticle.Play();           //�и� ����Ʈ
        Invoke("HittedOut", 0.2f);              //�����ǰݹ���
        parryArea.enabled = false;
        Invoke("LockPlayerInput", 0.3f);
    }
    private void OnDamage() {              //���� �Ǵ� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;                        //�����ǰݹ���
        isHittedMotioning = true;           //�ǰ� ���� 60������ �� �и� ���� ����

        Debug.Log("3"); //����������������
        original = this.transform;
        smoothMoved.SmoothMove_normalAttack(original, nearObject.transform);

        Invoke("HittedMotioningOut", 0.1f);
        Invoke("HittedOut", 0.2f);
        Invoke("LockPlayerInput", 0.5f);
    }
    private void OnPowerDamage() {              //���� ���� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;                        //�����ǰݹ���
        isHittedMotioning = true;           //�ǰ� ���� 60������ �� �и� ���� ����
        powerHittedParticle.Play();
        Debug.Log("4"); //����������������
        original = this.transform;
        smoothMoved.SmoothMove_powerAttack(original, nearObject.transform);

        Invoke("HittedMotioningOut", 0.1f);
        Invoke("HittedOut", 0.2f);
        Invoke("LockPlayerInput", 1f);      //�÷��̾� �̵� �Ұ� ����
    }
    private void HittedOut() {
        isHitted = false;    //�����ǰݹ���
        Debug.Log("5"); //����������������
    }

    private void HittedMotioningOut() {
        isHittedMotioning = false;    // �ǰ� ���� 60������ �� �и� ���� ����
        Debug.Log("6"); //����������������
    }

    private void LockPlayerInput() {
        isLockPlayerMoved = false;    //�ǰ� �� �̵� ����
        Debug.Log("7"); //����������������
    }

    IEnumerator LockPlayerMoveInput() {
        //playerController.speed = playerController.defaultSpeed;
        while (isHittedMotioning) {
            playerController.rb.velocity = Vector3.zero;
            yield return null;
        }
        yield return null;
    }
}
