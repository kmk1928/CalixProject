using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
    public int maxHP = 100;
    public int curHP;
    public int defense = 5;
    float parryTimer = 0f;

    bool isBlocked = false;     //우클릭 가드 확인용
    bool isParried = false;     //패리 가능 상태 확인용
    bool isHitted = false;      //데미지를 연속으로 입는 것을 방지하기 위한 bool 트리거, OnDamage코루틴에 사용

    Material mat;

    [Header("Parry")]
    [Tooltip("플레이어 패링판정 범위")]
    public BoxCollider parryArea;
    [Tooltip("패링 성공 이펙트")]
    public ParticleSystem parryParticle;
    [Tooltip("패링시간 확인용 텍스트")]
    public Text timeText;       //확인용 텍스트



    // Start is called before the first frame update
    void Start() {
        mat = GetComponent<MeshRenderer>().material;
        curHP = maxHP;
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {                  //우클릭 키 다운 시 패링
            isParried = true;                   //패리중을 true로 변경
        }
        if (Input.GetMouseButton(1)) {                       //우클릭 꾹 누를 시 가드
            isBlocked = true;                   //가드중을 true로 변경
        }
        if (isParried) {                        //패리중이 true일때 패리 영역을활성화하고 패리시간이 지나면 다시 꺼지는 코드
            parryArea.enabled = true;
            parryTimer += Time.deltaTime;
            if (parryTimer > 0.3f) {
                parryTimer = 0;
                isParried = false;
                parryArea.enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(1)) {         //우클릭 해제 시 가드해제
            isBlocked = false;
            Debug.Log("?-? Non blocked");

        }

        timeText.text = parryTimer.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnemyAttack" && !isHitted) {
            if (isParried) {
                Debug.Log("PARRY!!!");                          //패링 성공
                StartCoroutine(OnParried());
            }
            else if (isBlocked) {                                    //우클릭 가드로 인한 뎀감 실험
                curHP -= (10 - defense);

                Debug.Log("Player Hit!! curHP = " + curHP);     //가드또는피격
                StartCoroutine(OnAttacked());
            }
            else {
                curHP -= 10;                    //데미지를 가져오진 않고 임시 데미지 사용

                Debug.Log("Player Hit!! curHP = " + curHP);     //가드또는피격
                StartCoroutine(OnAttacked());
            }

        }
    }

    IEnumerator OnParried() {
        isHitted = true;    //연속피격방지
        AttackedPushed(0.1f);
        parryParticle.Play();
        mat.color = Color.green;
        yield return new WaitForSeconds(0.2f);

        mat.color = Color.white;
        isHitted = false;    //연속피격방지
    }
    IEnumerator OnAttacked() {              //가드 또는 피격 시 쓰는 데미지 코루틴
        isHitted = true;    //연속피격방지
        AttackedPushed(0.3f);
        if (isBlocked) {                                    //우클릭 가드로 인한 뎀감 실험
            mat.color = Color.yellow;
        }
        else {
            mat.color = Color.red;              //피격 시 빨간색으로 변경 후 
        }
        yield return new WaitForSeconds(0.1f);  // 0.1초 후 아래 조건문에 의해 색 변경 


        if (curHP > 0) {
            mat.color = Color.white;        //체력이 남아있으면 흰색으로
        }
        else {
            mat.color = Color.gray;
            // Destroy(gameObject, 4);      //체력이 없으면 회색 + 4초 후 제거
        }
        yield return new WaitForSeconds(0.4f);
        isHitted = false;    //연속피격방지
    }

    void AttackedPushed(float zFlow) {      //캐릭터 z값만큼 뒤로 밀려남
        this.transform.position += new Vector3(0, 0, -zFlow);
    }
}
