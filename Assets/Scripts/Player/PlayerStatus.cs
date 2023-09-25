using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int maxHP = 100;
    public int curHP;
    public int defense = 5;
    float parryTimer = 0f;

    public Text timeText;       //Ȯ�ο� �ؽ�Ʈ

    bool isBlocked = false;     //��Ŭ�� ���� Ȯ�ο�
    bool isParried = false;     //�и� ���� ���� Ȯ�ο�
    bool isHitted = false;      //�������� �������� �Դ� ���� �����ϱ� ���� bool Ʈ����, OnDamage�ڷ�ƾ�� ���

    Animator anim;//animation variable

    Material mat;

    public ParticleSystem parryParticle;

    public BoxCollider parryArea;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        curHP = maxHP;
        anim = GetComponentInChildren<Animator>();//animation
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {                  //��Ŭ�� Ű �ٿ� �� �и�
            isParried = true;                   //�и����� true�� ����
            anim.SetTrigger("GuardTrigger");
        }
        if (Input.GetMouseButtonDown(1)){                       //��Ŭ�� �� ���� �� ����
            isBlocked = true;                   //�������� true�� ����
            anim.SetTrigger("GuardTrigger");
        }
        if (isParried) {                        //�и����� true�϶� �и� ������Ȱ��ȭ�ϰ� �и��ð��� ������ �ٽ� ������ �ڵ�
            parryArea.enabled = true;
            parryTimer += Time.deltaTime;
            if(parryTimer > 0.3f) {
                parryTimer = 0;
                isParried = false;
                parryArea.enabled = false;
            }
        }
        if(Input.GetMouseButtonUp(1)) {         //��Ŭ�� ���� �� ��������
            isBlocked = false;
            Debug.Log("?-? Non blocked");

        }
        
        timeText.text = parryTimer.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "EnemyAttack" && !isHitted) {
            if (isParried) {
                Debug.Log("PARRY!!!");                          //�и� ����
                StartCoroutine(OnParried());
            }
            else if (isBlocked) {                                    //��Ŭ�� ����� ���� ���� ����
                curHP -= (10 - defense);

                Debug.Log("Player Hit!! curHP = " + curHP);     //����Ǵ��ǰ�
                StartCoroutine(OnAttacked());
            }
            else {
                curHP -= 10;                    //�������� �������� �ʰ� �ӽ� ������ ���

                Debug.Log("Player Hit!! curHP = " + curHP);     //����Ǵ��ǰ�
                StartCoroutine(OnAttacked());
            }

        }
    }

    IEnumerator OnParried() {
        isHitted = true;    //�����ǰݹ���
        parryParticle.Play();
        mat.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        mat.color = Color.white;
        isHitted = false;    //�����ǰݹ���
    }
    IEnumerator OnAttacked() {              //���� �Ǵ� �ǰ� �� ���� ������ �ڷ�ƾ
        isHitted = true;    //�����ǰݹ���

        if (isBlocked) {                                    //��Ŭ�� ����� ���� ���� ����
            mat.color = Color.yellow;
        }
        else {
            mat.color = Color.red;              //�ǰ� �� ���������� ���� �� 
        }
        yield return new WaitForSeconds(0.1f);  // 0.1�� �� �Ʒ� ���ǹ��� ���� �� ���� 


        if (curHP > 0) {
            mat.color = Color.white;        //ü���� ���������� �������
        }
        else {
            mat.color = Color.gray;
            // Destroy(gameObject, 4);      //ü���� ������ ȸ�� + 4�� �� ����
        }
        yield return new WaitForSeconds(0.4f);
        isHitted = false;    //�����ǰݹ���
    }
}
