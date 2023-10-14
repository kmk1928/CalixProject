using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoved : MonoBehaviour
{

    public float smoothTime = 0.2f;
    /* 원본
        IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //캐릭터 z값만큼 뒤로 밀려남
        Vector3 velocity = Vector3.zero;
        this.transform.position = current;
        float offset = 0.1f;
        while (target.z + offset <= this.transform.position.z) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, target, ref velocity, time);
            yield return null;
        }

        yield return null;
    }
     */

    #region -z방향으로 밀림
    IEnumerator SmoothPushed(Vector3 currentPosition, Vector3 enemyPosition, float time, float shovedDistance) {      //캐릭터 z값만큼 뒤로 밀려남
        Vector3 velocity = Vector3.zero;
        float offset = 0.1f;
        Vector3 awayFromPlayerVector = (currentPosition - enemyPosition).normalized; //normalized로 방향만 남김
        awayFromPlayerVector.y = 0f;        //수평적인 움직임을 위해 y 제거
        Vector3 targetPosition = transform.position + awayFromPlayerVector * shovedDistance;    //shovedDistance만큼 밀려남

        this.transform.position = currentPosition; //시작위치
        while (Vector3.Distance(this.transform.position, targetPosition) > offset) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, time);
            yield return null;
        }

        yield return null;
    }

    public void SmoothMove_Parry(Transform original, Transform enemy) {
        //SmoothPushed(현재위치, 목표위치(현재위치 - 0.3f의 Z값) , 이동시간)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    0.3f));
    }
    public void SmoothMove_normalAttack(Transform original, Transform enemy) {
        //SmoothPushed(현재위치, 목표위치(현재위치 - 0.3f의 Z값) , 이동시간)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    1f));
    }
    public void SmoothMove_powerAttack(Transform original, Transform enemy) {
        //SmoothPushed(현재위치, 목표위치(현재위치 - 0.3f의 Z값) , 이동시간)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    2f));
    }
    #endregion

    #region 아래는 gpt가 짜준 z방향이 아닌 적의 반대 방향으로 날리는 함수다
    IEnumerator MoveAwayFromPlayer(Transform player, Transform enemy, float time) {
        Vector3 playerPosition = player.position;
        Vector3 enemyPosition = enemy.position;

        // 플레이어와 적을 연결하는 벡터 계산
        Vector3 awayFromPlayerVector = (enemyPosition - playerPosition).normalized;

        // 목표 위치 설정 (현재 위치에서 떨어진 위치)
        Vector3 targetPosition = transform.position + awayFromPlayerVector * 2f;

        Vector3 velocity = Vector3.zero;
        this.transform.position = playerPosition; // 시작 위치

        while (Vector3.Distance(this.transform.position, targetPosition) > 0.1f) { //현재 위치와 목표 위치 간의 거리가 0.1보다 큰지 확인합니다.
            // 부드럽게 이동
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, time);
            yield return null;
        }

        yield return null;
    }

    public void MoveAwayFromPlayer(Transform player, Transform enemy) {
        StartCoroutine(MoveAwayFromPlayer(player, enemy, smoothTime));
    }
    #endregion
}
