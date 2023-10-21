using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoved : MonoBehaviour
{

    private float smoothTime = 0.1f;
    private float temp = 0.0f;      //오브젝트에 막혔을 때 탈출용 float

    #region 적의 반대 방향으로 날리는 함수
    IEnumerator SmoothPushed(Vector3 currentPosition, Vector3 enemyPosition, float time, float shovedDistance) {      //캐릭터 z값만큼 뒤로 밀려남
        Vector3 velocity = Vector3.zero;
        float offset = 0.1f;
        Vector3 awayFromPlayerVector = (currentPosition - enemyPosition).normalized; //normalized로 방향만 남김
        awayFromPlayerVector.y = 0f;        //수평적인 움직임을 위해 y 제거
        Vector3 targetPosition = transform.position + awayFromPlayerVector * shovedDistance;    //shovedDistance만큼 밀려남

        this.transform.position = currentPosition; //시작위치
        
        while (Vector3.Distance(this.transform.position, targetPosition) > offset ) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, time);
            temp += Time.deltaTime;
            if (temp > 0.3f) {
                //while문이 끝나지 않을 때 탈출 = 코루틴 강제 종료
                break;
            }
            yield return null;
            ////yield return new WaitForFixedUpdate();
        }
        
         temp = 0.0f;
        yield return null;
    }

    public void SmoothMove_Parry(Transform original, Transform enemy) {
        //SmoothPushed(현재위치, 공격자의 위치 , 이동시간, 이동거리)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime - 0.1f,
                                    0.3f));
    }
    public void SmoothMove_normalAttack(Transform original, Transform enemy) {
        //SmoothPushed(현재위치, 공격자의 위치 , 이동시간, 이동거리)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    1f));
    }
    public void SmoothMove_powerAttack(Transform original, Transform enemy) {
        //SmoothPushed(현재위치, 공격자의 위치 , 이동시간, 이동거리)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    2f));
    }
    #endregion

    /* 원본 z방향으로 밀림
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

}
