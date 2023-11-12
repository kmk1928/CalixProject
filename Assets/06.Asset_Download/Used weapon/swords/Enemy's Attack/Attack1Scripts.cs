using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Scripts : MonoBehaviour
{
    public Transform player; // Player 오브젝트의 Transform 컴포넌트를 연결해야 합니다.

    private bool isHit = false; // 충돌 여부를 나타내는 변수
    private Rigidbody rb; // Cube의 Rigidbody 컴포넌트

    void Start()
    {
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Cube가 충돌하지 않았을 때만 회전 코드를 실행합니다.
        if (!isHit)
        {
            // Cube의 위치에서 Player의 위치를 향하도록 방향 벡터를 구합니다.
            Vector3 directionToPlayer = player.position - transform.position;

            // 방향 벡터를 회전 각도로 변환합니다.
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);

            // Cube의 회전을 적용합니다.
            transform.rotation = rotationToPlayer;
        }
    }

    // 충돌 발생 시 호출되는 함수
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Player" 또는 "Ground"일 때
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            // Rigidbody의 속도를 0으로 설정하여 즉시 움직임을 멈춥니다.
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 충돌 상태를 true로 설정
            isHit = true;

            // 1초 후에 사라지도록 코루틴을 시작합니다.
            StartCoroutine(DisappearAfterDelay(1f));
        }
    }

    // 일정 시간 후에 사라지도록 하는 코루틴 함수
    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 1초 후에 Cube를 제거합니다.
        Destroy(gameObject);
    }
}