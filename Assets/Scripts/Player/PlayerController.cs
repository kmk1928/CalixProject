using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도 
    public float jumpForce = 7.0f; // 점프 힘
    public float dashSpeed = 10.0f; // 대시 속도
    public float dashDuration = 0.5f; // 대시 지속 시간
    public float maxJumpAngle = 30.0f; // 최대 점프 각도 (좌우로 움직일 수 있는 각도)


    private Rigidbody rb;
    private bool isGrounded = true; // 땅에 닿았는지 여부
    private int jumpCount = 0; // 점프 횟수
    private bool isDashing = false; // 대시 중인지 여부
    private float dashTimer = 0.0f; // 대시 타이머


    private Transform mainCameraTransform;

    Animator anim;//animation variable

    GameObject nearObject;
    Weapon equipWeapon;

    bool isDodge;

    //weapon variable
    public GameObject[] weapons;
    public bool[] hasWeapons;
    bool isSwap;
    bool isAttack;
    int equipWeaponIndex = -1;

    //attack
    bool isFireReady;
    float fireDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCameraTransform = Camera.main.transform;

        anim = GetComponentInChildren<Animator>();//animation
    }

    void Update()
    {
        // �̵� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = mainCameraTransform.forward;
        Vector3 cameraRight = mainCameraTransform.right;
        cameraForward.y = 0f; // y 축 회전 방향을 무시합니다.
        cameraRight.y = 0f;

        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        // 대시 중인 경우 대시 속도로 이동
        if (isDashing)
        {
            movement = transform.forward * dashSpeed;
            dashTimer += Time.deltaTime;

            // 대시 지속 시간이 지나면 대시 종료
            if (dashTimer >= dashDuration) {
                isDashing = false;
                dashTimer = 0.0f;
            }
        }
        else {
            // 일반 이동: 이동 방향을 이용하여 이동
            movement *= speed;

            if (!isGrounded) {
                float angle = Vector3.Angle(movement, transform.forward);
                if (angle > maxJumpAngle) {
                    // 만약 현재 움직이는 각도가 제한된 각도보다 크면, 이동 벡터를 수정하여 원하는 각도 내에서 움직일 수 있도록 합니다.
                    movement = Quaternion.Euler(0, maxJumpAngle, 0) * transform.forward * speed;
                }
            }
        }

        // 회전 처리: 이동 방향으로 캐릭터를 회전
        if (movement != Vector3.zero )
        {
            transform.forward = movement.normalized;
            anim.SetBool("isRun",true);//run animation
        }
        else if (movement == Vector3.zero)
        {
            anim.SetBool("isRun",false);//able animation
        }

        // Rigidbody�� �̵� ���� (y ���� ������ �ӵ��� �״�� ����)
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // ��� ó��
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && isGrounded)
        {
            //anim.SetTrigger("DashTrigger");//Dash animation
            isDashing = true;
        }

        // ���� ó��
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1 && !isDashing && !isSwap && !isDodge)
        {
            anim.SetTrigger("JumpTrigger");//Jump animation
            // ���� ���� �����Ͽ� ����
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            
        }
        //weapon
        Interraction();
        Swap();
        
        //attack
        Attack();

        Dodge();
    }

    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        FreezeRotation();
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���� ������ ���� ������ ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // ������ �������� ���� �Ұ����� ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "weapon")
            nearObject = other.gameObject;   
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "weapon")
            nearObject = null;
    }
    //weapon
    void Swap()
    {
        if(Input.GetKeyDown("1") && (!hasWeapons[0]||equipWeaponIndex == 0))
            return;
        if(Input.GetKeyDown("2") && (!hasWeapons[1]||equipWeaponIndex == 1))
            return;
        if(Input.GetKeyDown("3") && (!hasWeapons[2]||equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if(Input.GetKeyDown("1")) weaponIndex = 0;
        if(Input.GetKeyDown("2")) weaponIndex = 1;
        if(Input.GetKeyDown("3")) weaponIndex = 2;

        if(Input.GetKeyDown("1")||Input.GetKeyDown("2")||Input.GetKeyDown("3"))
        {
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");
            isSwap = true;
            speed = 0f;
            Invoke("SwapOut",0.6f);
        }
    }
    void SwapOut()
    {
        speed = 5.0f;
        isSwap = false;
    }

    void Interraction()
    {
        if (Input.GetKeyDown("e")&&nearObject != null)
        {
            Debug.Log("e누름");
            if(nearObject.tag == "weapon"){
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }
    //attack
    void Attack()
    {
        if(equipWeapon == null)
            return;
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(Input.GetMouseButtonDown(0) && isFireReady && !isDashing && !isSwap)
        {
            Debug.Log("!---Click Mouse(0)---!");
            equipWeapon.Use();
            anim.SetTrigger("doSwing");
            fireDelay = 0;
            isAttack = true;
            speed = 0;
            Invoke("AttackOut",0.5f);
        }
    }

    void AttackOut()
    {
        speed = 5.0f;
        isAttack = false;
    }

    //Dodge
    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && jumpCount < 1 && !isDashing && !isSwap && !isDodge)
        {
            speed *= 2;
            anim.SetTrigger("DodgeTrigger");//Jump animation
            // ���� ���� �����Ͽ� ����
            isDodge = true;

            Invoke("DodgeOut",0.5f);
        }
    }

    void DodgeOut()
    {
        speed *=0.5f;
        isDodge = false;
    }
}