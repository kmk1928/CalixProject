using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParryGuard : MonoBehaviour {

    CharCombat combat;

    [SerializeField]
    private float parryTimer = 0f;
    bool isBlocked = false;     //��Ŭ�� ���� Ȯ�ο�
    bool isParried = false;     //�и� ���� ���� Ȯ�ο�
    bool isHitted = false;      //�������� �������� �Դ� ���� �����ϱ� ���� bool Ʈ����, OnDamage�ڷ�ƾ�� ���

    public float smoothTime = 0.5f;
    Transform original;
    SmoothMoved smoothMoved;
    GameObject nearObject;

    [Header("Parry")]
    [Tooltip("�÷��̾� �и����� ����")]
    public BoxCollider parryArea;
    [Tooltip("�и� ���� ����Ʈ")]
    public ParticleSystem parryParticle;

    Animator anim;//animation variable

    PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<CharCombat>();
        anim = GetComponentInChildren<Animator>();//animation
        playerController = GetComponent<PlayerController>();
        smoothMoved = GetComponent<SmoothMoved>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {                  //��Ŭ�� Ű �ٿ� �� �и�
            isParried = true;                   //�и����� true�� ����
            anim.SetBool("isGuard", true);//able animation
            playerController.speed /= 2;
        }
        if (Input.GetMouseButton(1)) {                       //��Ŭ�� �� ���� �� ����
            isBlocked = true;                   //�������� true�� ����
        }
        if (isParried) {                        //�и����� true�϶� �и� ������Ȱ��ȭ�ϰ� �и��ð��� ������ �ٽ� ������ �ڵ�
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
            playerController.speed *= 2;
            Debug.Log("?-? Non blocked");

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnemyAttack" || other.tag == "EnemyPowerAttack" && !isHitted) {
            nearObject = other.gameObject;
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
        parryArea.enabled = false;      
        parryTimer = 0f;    //�и����ɽð� �ʱ�ȭ
        Debug.Log("�и� ��------");

        original = this.transform;      //�ε巴�� �з���
        smoothMoved.SmoothMove_Parry(original, nearObject.transform);

        parryParticle.Play();           //�и� ����Ʈ
        Invoke("HittedOut", 0.2f);              //�����ǰݹ���
    }
    private void OnDamage() {              //���� �Ǵ� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;                        //�����ǰݹ���
        original = this.transform;
        smoothMoved.SmoothMove_normalAttack(original, nearObject.transform);

        Invoke("HittedOut", 0.2f);              //�����ǰݹ���
    }
    private void OnPowerDamage() {              //���� ���� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;                        //�����ǰݹ���

        original = this.transform;
        smoothMoved.SmoothMove_powerAttack(original, nearObject.transform);

        Invoke("HittedOut", 0.2f);              //�����ǰݹ���
    }
    private void HittedOut() {
        isHitted = false;    //�����ǰݹ���
    }

}
