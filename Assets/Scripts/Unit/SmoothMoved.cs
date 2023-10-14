using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoved : MonoBehaviour
{

    public float smoothTime = 0.2f;
    // Start is called before the first frame update

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

    public void SmoothMove_Parry(Transform original) {
        //SmoothPushed(현재위치, 목표위치(현재위치 - 0.16f의 Z값) , 이동시간)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 0.16f),
                                    smoothTime));
    }
    public void SmoothMove_normalAttack(Transform original) {
        //SmoothPushed(현재위치, 목표위치(현재위치 - 0.3f의 Z값) , 이동시간)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 0.3f),
                                    smoothTime));
    }
    public void SmoothMove_powerAttack(Transform original) {
        //SmoothPushed(현재위치, 목표위치, 이동시간)을 받음
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 1.6f),
                                    smoothTime + 0.3f));
    }
}
