using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNav : MonoBehaviour
{
    public GameObject yourUI; // 특정 UI 객체를 Inspector에서 할당
    public GameObject navUI;

    // 플레이어가 트리거 영역에 진입했을 때 호출됨
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와의 충돌 여부 확인
        {
            yourUI.SetActive(true); // UI를 활성화
            navUI.SetActive(true);
        }
    }

    // 플레이어가 트리거 영역에서 나갔을 때 호출됨
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와의 충돌 여부 확인
        {
            yourUI.SetActive(false); // UI를 비활성화
            navUI.SetActive(false);
        }
    }
}
