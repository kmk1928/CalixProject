using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParryGuard : MonoBehaviour {

    CharCombat combat;

    [SerializeField]
    private float parryTimer = 0f;
    private bool isBlocked = false;     //우클릭 가드 확인용
    private bool isParried = false;     //패리 가능 상태 확인용
    private bool isHitted = false;      //데미지를 연속으로 입는 것을 방지하기 위한 bool 트리거, OnDamage코루틴에 사용
    private bool isHittedMotioning = false;     //맞는 모션중 패링 방지
    private float parryRecovery_Time = 0.3f;        //이동가능까지 걸리는 시간
    private float hitRecovery_Time = 0.5f;
    private float powerHitRecovery_Time = 1.0f;

    public float smoothTime = 0.5f;
    Transform original;
    SmoothMoved smoothMoved;
    GameObject nearObject;

    [Header("Parry")]
    [Tooltip("플레이어 패링판정 범위")]
    public BoxCollider parryArea;
    [Tooltip("패링 성공 이펙트")]
    public ParticleSystem parryParticle;

    [Header("power Hit")]
    [Tooltip("강한 피격 이펙트")]
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
        if (Input.GetMouseButtonDown(1) && !isHittedMotioning) {                  //우클릭 키 다운 시 패링
            isParried = true;                   //패리중을 true로 변경
            isBlocked = true;                   //가드중을 true로 변경
            anim.SetBool("isGuard", true);//able animation
            playerController.speed /= 2;
        }
       //if (Input.GetMouseButton(1)) {                       //우클릭 꾹 누를 시 가드
       //     
       // }
        if (isParried && !isHittedMotioning) {                        //패리중이 true일때 패리 영역을활성화하고 패리시간이 지나면 다시 꺼지는 코드
            parryArea.enabled = true;
            parryTimer += Time.deltaTime;
            if (parryTimer > 0.6f) {
                parryTimer = 0;
                isParried = false;
                parryArea.enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(1)) {         //우클릭 해제 시 가드해제
            isBlocked = false;
            anim.SetBool("isGuard", false);//able animation
            playerController.speed = playerController.defaultSpeed;
            Debug.Log("?-? Non blocked");

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!isHitted && (other.tag == "EnemyAttack" || other.tag == "EnemyPowerAttack") && !playerController.isDodge && !playerController.isDead) {
            Debug.Log("1"); //추적추적추적추적
            nearObject = other.gameObject;
            playerController.LockPlayerInput();  //피격중 이동 제한
            TestAnimationEndPlayerVelocityZero();
            if (isParried) {
                Debug.Log("PARRY!!!");                          //패링 성공
                OnParried();
            }
            else if (isBlocked) {                                    //우클릭 가드로 인한 뎀감 실험
                CharStats targetStatus = other.GetComponentInParent<CharStats>();
                if(targetStatus != null) {
                    combat.Guard(targetStatus);
                }
                Debug.Log("Guard!");
                OnDamage();
            }
            else {
                CharStats targetStatus = other.GetComponentInParent<CharStats>();
                    combat.PlayerHitted(targetStatus);
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
        isHitted = true;                        //연속피격방지
        isParried = false;     
        parryTimer = 0f;    //패리가능시간 초기화
       /// Debug.Log("2"); //추적추적추적추적
        original = this.transform;      //부드럽게 밀려남
        smoothMoved.SmoothMove_Parry(original, nearObject.transform);

        parryParticle.Play();           //패리 이펙트
        Invoke("HittedOut", 0.2f);              //연속피격방지   
        parryArea.enabled = false;
        playerController.Invoke("UnlockPlayerInput_ForAnimRootMotion", parryRecovery_Time);
    }
    private void OnDamage() {              //가드 또는 피격 시 쓰는 데미지 코루틴
        isHitted = true;                        //연속피격방지
        isHittedMotioning = true;           //피격 직후 60프레임 내 패링 가능 방지

        //Debug.Log("3"); //추적추적추적추적
        original = this.transform;
        smoothMoved.SmoothMove_normalAttack(original, nearObject.transform);

        Invoke("HittedMotioningOut", 0.1f);
        Invoke("HittedOut", 0.2f);
        playerController.Invoke("UnlockPlayerInput_ForAnimRootMotion", hitRecovery_Time);
        TestAnimationEndPlayerVelocityZero();
    }
    private void OnPowerDamage() {              //강한 공격 피격 시 쓰는 데미지 코루틴
        isHitted = true;                        //연속피격방지
        isHittedMotioning = true;           //피격 직후 60프레임 내 패링 가능 방지
        powerHittedParticle.Play();
       // Debug.Log("4"); //추적추적추적추적
        original = this.transform;
        smoothMoved.SmoothMove_powerAttack(original, nearObject.transform);

        Invoke("HittedMotioningOut", 0.1f);
        Invoke("HittedOut", 0.2f);
        playerController.Invoke("UnlockPlayerInput_ForAnimRootMotion", powerHitRecovery_Time);
    }
    private void HittedOut() {
        isHitted = false;    //연속피격방지
        //Debug.Log("5"); //추적추적추적추적
    }

    private void HittedMotioningOut() {
        isHittedMotioning = false;    // 피격 직후 60프레임 내 패링 가능 방지
        //Debug.Log("6"); //추적추적추적추적
    }

    private void TestAnimationEndPlayerVelocityZero() {
        playerController.rb.velocity = Vector3.zero; 
    }
}
