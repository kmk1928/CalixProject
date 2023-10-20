using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoved : MonoBehaviour
{

    private float smoothTime = 0.1f;
    private float temp = 0.0f;      //������Ʈ�� ������ �� Ż��� float

    #region ���� �ݴ� �������� ������ �Լ�
    IEnumerator SmoothPushed(Vector3 currentPosition, Vector3 enemyPosition, float time, float shovedDistance) {      //ĳ���� z����ŭ �ڷ� �з���
        Vector3 velocity = Vector3.zero;
        float offset = 0.1f;
        Vector3 awayFromPlayerVector = (currentPosition - enemyPosition).normalized; //normalized�� ���⸸ ����
        awayFromPlayerVector.y = 0f;        //�������� �������� ���� y ����
        Vector3 targetPosition = transform.position + awayFromPlayerVector * shovedDistance;    //shovedDistance��ŭ �з���

        this.transform.position = currentPosition; //������ġ
        
        while (Vector3.Distance(this.transform.position, targetPosition) > offset ) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, time);
            temp += Time.deltaTime;
            if (temp > 0.3f) {
                //while���� ������ ���� �� Ż�� = �ڷ�ƾ ���� ����
                break;
            }
            yield return null;
            ////yield return new WaitForFixedUpdate();
        }
        
         temp = 0.0f;
        yield return null;
    }

    public void SmoothMove_Parry(Transform original, Transform enemy) {
        //SmoothPushed(������ġ, �������� ��ġ , �̵��ð�, �̵��Ÿ�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime - 0.1f,
                                    0.3f));
    }
    public void SmoothMove_normalAttack(Transform original, Transform enemy) {
        //SmoothPushed(������ġ, �������� ��ġ , �̵��ð�, �̵��Ÿ�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    1f));
    }
    public void SmoothMove_powerAttack(Transform original, Transform enemy) {
        //SmoothPushed(������ġ, �������� ��ġ , �̵��ð�, �̵��Ÿ�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    2f));
    }
    #endregion

    /* ���� z�������� �и�
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
    */

}
