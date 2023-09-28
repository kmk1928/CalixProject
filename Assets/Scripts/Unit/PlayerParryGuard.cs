using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParryGuard : MonoBehaviour {

    CharCombat combat;

    [SerializeField]
    private float parryTimer = 0f;
    bool isBlocked = false;     //우클릭 가드 확인용
    bool isParried = false;     //패리 가능 상태 확인용
    bool isHitted = false;      //데미지를 연속으로 입는 것을 방지하기 위한 bool 트리거, OnDamage코루틴에 사용

    Material mat;

    [Header("Parry")]
    [Tooltip("플레이어 패링판정 범위")]
    public BoxCollider parryArea;
    [Tooltip("패링 성공 이펙트")]
    public ParticleSystem parryParticle;



    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<CharCombat>();
        mat = GetComponent<MeshRenderer>().material;
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
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnemyAttack" && !isHitted) {
            if (isParried) {
                Debug.Log("PARRY!!!");                          //패링 성공
                StartCoroutine(OnParried());
            }
            else if (isBlocked) {                                    //우클릭 가드로 인한 뎀감 실험
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
        isHitted = true;    //연속피격방지
        AttackedPushed(0.1f);
        parryParticle.Play();
        mat.color = Color.green;
        yield return new WaitForSeconds(0.2f);

        mat.color = Color.white;
        isHitted = false;    //연속피격방지
    }
    IEnumerator OnDamage() {              //가드 또는 피격 시 쓰는 데미지 코루틴
        isHitted = true;    //연속피격방지
        AttackedPushed(0.3f);
        /*if (isblocked) {                                    //우클릭 가드로 인한 뎀감 실험
            mat.color = color.yellow;
        }
        else {
            mat.color = color.red;              //피격 시 빨간색으로 변경 후 
        }*/
        yield return new WaitForSeconds(0.4f);
        isHitted = false;    //연속피격방지
    }

    void AttackedPushed(float zFlow) {      //캐릭터 z값만큼 뒤로 밀려남
        this.transform.position += new Vector3(0, 0, -zFlow);
    }
}
