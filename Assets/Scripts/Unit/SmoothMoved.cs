using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoved : MonoBehaviour
{

    public float smoothTime = 0.2f;
    // Start is called before the first frame update

    IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //ĳ���� z����ŭ �ڷ� �з���
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
        //SmoothPushed(������ġ, ��ǥ��ġ(������ġ - 0.16f�� Z��) , �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 0.16f),
                                    smoothTime));
    }
    public void SmoothMove_normalAttack(Transform original) {
        //SmoothPushed(������ġ, ��ǥ��ġ(������ġ - 0.3f�� Z��) , �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 0.3f),
                                    smoothTime));
    }
    public void SmoothMove_powerAttack(Transform original) {
        //SmoothPushed(������ġ, ��ǥ��ġ, �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    original.position - new Vector3(0, 0, 1.6f),
                                    smoothTime + 0.3f));
    }
}
