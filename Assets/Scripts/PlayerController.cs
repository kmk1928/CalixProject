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

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 이동 처리
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
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0.0f;
            }
        }
        else
        {
            // 일반 이동: 이동 방향을 이용하여 이동
            movement *= speed;

            if (!isGrounded)
            {
                float angle = Vector3.Angle(movement, transform.forward);
                if (angle > maxJumpAngle)
                {
                    // 만약 현재 움직이는 각도가 제한된 각도보다 크면, 이동 벡터를 수정하여 원하는 각도 내에서 움직일 수 있도록 합니다.
                    movement = Quaternion.Euler(0, maxJumpAngle, 0) * transform.forward * speed;
                }
            }
        }

        // 회전 처리: 이동 방향으로 캐릭터를 회전
        if (movement != Vector3.zero)
        {
            transform.forward = movement.normalized;

        }

        // Rigidbody에 이동 적용 (y 축은 현재의 속도를 그대로 유지)
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // 대시 처리
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && isGrounded)
        {
            isDashing = true;
        }

        // 점프 처리
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1 && !isDashing)
        {
            // 점프 힘을 적용하여 점프
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }
 
    void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿으면 점프 가능한 상태로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // 땅에서 떨어지면 점프 불가능한 상태로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}