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

    Material mat;

    [Header("Parry")]
    [Tooltip("�÷��̾� �и����� ����")]
    public BoxCollider parryArea;
    [Tooltip("�и� ���� ����Ʈ")]
    public ParticleSystem parryParticle;



    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<CharCombat>();
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {                  //��Ŭ�� Ű �ٿ� �� �и�
            isParried = true;                   //�и����� true�� ����
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
            Debug.Log("?-? Non blocked");

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnemyAttack" && !isHitted) {
            if (isParried) {
                Debug.Log("PARRY!!!");                          //�и� ����
                StartCoroutine(OnParried());
            }
            else if (isBlocked) {                                    //��Ŭ�� ����� ���� ���� ����
                CharStats targetStatus = other.GetComponentInParent<CharStats>();
                if(targetStatus != null) {
                    combat.Guard(targetStatus);
                }
                Debug.Log("Guard!");
                StartCoroutine(OnDamage());
            }
            else {
                CharStats targetStatus = other.GetComponentInParent<CharStats>();
                    combat.Hitted(targetStatus);
                Debug.Log("Damaged");

                StartCoroutine(OnDamage());
            }

        }
    }

    IEnumerator OnParried() {
        isHitted = true;    //�����ǰݹ���
        AttackedPushed(0.1f);
        parryParticle.Play();
        mat.color = Color.green;
        yield return new WaitForSeconds(0.2f);

        mat.color = Color.white;
        isHitted = false;    //�����ǰݹ���
    }
    IEnumerator OnDamage() {              //���� �Ǵ� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;    //�����ǰݹ���
        AttackedPushed(0.3f);
        /*if (isblocked) {                                    //��Ŭ�� ����� ���� ���� ����
            mat.color = color.yellow;
        }
        else {
            mat.color = color.red;              //�ǰ� �� ���������� ���� �� 
        }*/
        yield return new WaitForSeconds(0.4f);
        isHitted = false;    //�����ǰݹ���
    }

    void AttackedPushed(float zFlow) {      //ĳ���� z����ŭ �ڷ� �з���
        this.transform.position += new Vector3(0, 0, -zFlow);
    }
}
