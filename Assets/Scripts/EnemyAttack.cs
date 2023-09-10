using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public Transform player; // Player 오브젝트의 Transform 컴포넌트를 연결해야 합니다.
    public GameObject cubePrefab; // Cube 오브젝트의 프리팹을 연결해야 합니다.
    public float attackDelay = 1.0f; // 공격 딜레이 (1초로 설정)

    private float timer = 0.0f;

    private void Update()
    {
        // 타이머를 업데이트하여 공격 딜레이를 카운트합니다.
        timer += Time.deltaTime;

        // 공격 딜레이가 지났을 때 Cube를 날립니다.
        if (timer >= attackDelay)
        {
            timer = 0.0f; // 타이머를 초기화합니다.

            // Player 오브젝트의 위치를 가져옵니다.
            Vector3 playerPosition = player.position;

            // Cube 오브젝트를 인스턴스화합니다.
            GameObject cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);

            // 1초 뒤에 Cube를 움직이도록 설정합니다.
            StartCoroutine(MoveCubeAfterDelay(cube, playerPosition));
        }
    }

    // Cube를 1초 뒤에 움직이는 함수
    private IEnumerator MoveCubeAfterDelay(GameObject cube, Vector3 playerPosition)
    {
        yield return new WaitForSeconds(1.0f); // 1초 대기

        // Cube를 Player 방향으로 날립니다.
        Vector3 direction = (playerPosition - cube.transform.position).normalized;
        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
        cubeRigidbody.velocity = direction * 30.0f; // 10은 날아가는 속도입니다. 필요에 따라 조절할 수 있습니다.
    }

   
}
