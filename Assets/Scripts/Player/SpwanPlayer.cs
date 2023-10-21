using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPlayer : MonoBehaviour
{
    public GameObject playerPrefab; // 플레이어 프리팹을 Inspector에서 설정

    void Awake() {
        // 플레이어 프리팹을 복제하여 씬에 생성
        GameObject playerClone = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // 복제한 플레이어를 원래 빈 게임 오브젝트로 대체
        Destroy(gameObject); // 빈 게임 오브젝트 삭제
    }
}
