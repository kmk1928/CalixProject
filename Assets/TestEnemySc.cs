using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySc : MonoBehaviour
{
    public Transform player;

    private void Start() {
        Vector3 start = transform.position;
        Vector3 end = player.position;
        StartCoroutine(CurveMove(start, end, 4f, 1f));

    }


    IEnumerator CurveMove(Vector3 startPos, Vector3 endPos, float height, float duration) {//커브이동
        // 이동 경로의 길이 계산
        float journeyLength = Vector3.Distance(startPos, endPos);
        // 이동 시작 시간 기록
        float startTime = Time.time;
        // 이동한 거리 초기화
        float distanceCovered = 0f;

        //// Raycast를 이용해 벽 감지
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, moveDirection, out hit, targetDistance, layerMaskForWalls)) {
        //    // 장애물(벽)이 감지되었다면, 벽 앞에 정지하도록 목표 위치 조정
        //    targetPosition = hit.point - moveDirection * 0.5f; // 벽에서 일정 거리만큼 뒤로 이동
        //}

        while (distanceCovered < journeyLength) {
            // 현재까지의 이동 시간을 계산
            float journeyTime = (Time.time - startTime) / duration;
            // 곡선의 높이를 계산 (이 코드는 곡선을 그리기 위한 수식)
            float heightOffset = height * 4.0f * journeyTime * (1 - journeyTime);
            // 현재 위치를 선형 보간을 사용하여 갱신 (Vector3.Lerp 함수를 사용하여 시작 위치와 끝 위치 사이를 이동)
            transform.position = Vector3.Lerp(startPos, endPos, journeyTime) + new Vector3(0f, heightOffset, 0f);
            // 현재까지 이동한 거리 갱신
            distanceCovered = journeyLength * journeyTime;

            // 다음 프레임까지 대기
            yield return null;
        }
        // 이동이 완료되면 다음 동작을 수행하거나 코루틴을 종료
    }  
}
