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
                }
                else if(other.tag == "EnemyPowerAttack") {
                    anim.SetTrigger("doDamage_Power");
                }
                OnDamage();
            }

        }
    }

    private void  OnParried() {
        isHitted = true;    //�����ǰݹ���
        isParried = false;          
        parryArea.enabled = false;      
        parryTimer = 0f;    //�и����ɽð� �ʱ�ȭ

        Debug.Log("�и� ��------");
        original = this.transform;
        //SmoothPushed(������ġ, ��ǥ��ġ, �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 0.3f),
                                    smoothTime));
        parryParticle.Play();
        isHitted = false;    //�����ǰݹ���
    }
    private void OnDamage() {              //���� �Ǵ� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;    //�����ǰݹ���
        StartCoroutine(SmoothPushed(original.position,
                            original.position - new Vector3(0, 0, 1),
                            smoothTime));
        isHitted = false;    //�����ǰݹ���
    }

    IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //ĳ���� z����ŭ �ڷ� �з���
        Vector3 velocity = Vector3.zero;
        Debug.Log("--------������ ��");
        this.transform.position = current;
        float offset = 0.1f;
        while (target.z + offset <= this.transform.position.z) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, target, ref velocity, time);
            yield return null;
        }

        yield return null;
    }
    /*
    IEnumerator OnParried() {
        isHitted = true;    //�����ǰݹ���
        //AttackedPushed(0.1f);   //�з���
        parryParticle.Play();
        yield return new WaitForSeconds(0.2f);
        isHitted = false;    //�����ǰݹ���
    }
    IEnumerator OnDamage() {              //���� �Ǵ� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;    //�����ǰݹ���
                            // AttackedPushed(0.2f);   //�з���
        yield return new WaitForSeconds(0.4f);
        isHitted = false;    //�����ǰݹ���
    }
   void AttackedPushed(float zFlow) {      //ĳ���� z����ŭ �ڷ� �з���
        this.transform.position += new Vector3(0, 0, -zFlow);
    }
 */

}
