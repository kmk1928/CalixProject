using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // �̵� �ӵ�
    public float jumpForce = 7.0f; // ���� ��
    public float dashSpeed = 30.0f; // ��� �ӵ�
    public float dashDuration = 0.5f; // ��� ���� �ð�
    public float maxJumpAngle = 30.0f; // �ִ� ���� ���� (�¿�� ������ �� �ִ� ����)

    private Rigidbody rb;
    private bool isGrounded = true; // ���� ��Ҵ��� ����
    private int jumpCount = 0; // ���� Ƚ��
    private bool isDashing = false; // ��� ������ ����
    private float dashTimer = 0.0f; // ��� Ÿ�̸�
    

    private Transform mainCameraTransform;

    Animator anim;//animation variable

    GameObject nearObject;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCameraTransform = Camera.main.transform;

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // �̵� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = mainCameraTransform.forward;
        Vector3 cameraRight = mainCameraTransform.right;
        cameraForward.y = 0f; // y �� ȸ�� ������ �����մϴ�.
        cameraRight.y = 0f;

        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        // ��� ���� ��� ��� �ӵ��� �̵�
        if (isDashing)
        {
            movement = transform.forward * dashSpeed;
            dashTimer += Time.deltaTime;

            // ��� ���� �ð��� ������ ��� ����
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0.0f;
            }
        }
        else
        {
            // �Ϲ� �̵�: �̵� ������ �̿��Ͽ� �̵�
            movement *= speed;
            
            if (!isGrounded)
            {
                float angle = Vector3.Angle(movement, transform.forward);
                if (angle > maxJumpAngle)
                {
                    // ���� ���� �����̴� ������ ���ѵ� �������� ũ��, �̵� ���͸� �����Ͽ� ���ϴ� ���� ������ ������ �� �ֵ��� �մϴ�.
                    movement = Quaternion.Euler(0, maxJumpAngle, 0) * transform.forward * speed;
                }
            }
        }

        // ȸ�� ó��: �̵� �������� ĳ���͸� ȸ��
        if (movement != Vector3.zero)
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
            anim.SetTrigger("DashTrigger");//Dash animation
            isDashing = true;
        }

        // ���� ó��
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1 && !isDashing)
        {
            anim.SetTrigger("JumpTrigger");//Jump animation
            // ���� ���� �����Ͽ� ����
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            
        }

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
        
        Debug.Log(nearObject.name);    
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "weapon")
            nearObject = null;
    }
}