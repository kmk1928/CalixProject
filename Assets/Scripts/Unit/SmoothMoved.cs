using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoved : MonoBehaviour
{

    public float smoothTime = 0.2f;
    /* ����
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

    #region -z�������� �и�
    IEnumerator SmoothPushed(Vector3 currentPosition, Vector3 enemyPosition, float time, float shovedDistance) {      //ĳ���� z����ŭ �ڷ� �з���
        Vector3 velocity = Vector3.zero;
        float offset = 0.1f;
        Vector3 awayFromPlayerVector = (currentPosition - enemyPosition).normalized; //normalized�� ���⸸ ����
        awayFromPlayerVector.y = 0f;        //�������� �������� ���� y ����
        Vector3 targetPosition = transform.position + awayFromPlayerVector * shovedDistance;    //shovedDistance��ŭ �з���

        this.transform.position = currentPosition; //������ġ
        while (Vector3.Distance(this.transform.position, targetPosition) > offset) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, time);
            yield return null;
        }

        yield return null;
    }

    public void SmoothMove_Parry(Transform original, Transform enemy) {
        //SmoothPushed(������ġ, ��ǥ��ġ(������ġ - 0.3f�� Z��) , �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    0.3f));
    }
    public void SmoothMove_normalAttack(Transform original, Transform enemy) {
        //SmoothPushed(������ġ, ��ǥ��ġ(������ġ - 0.3f�� Z��) , �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    1f));
    }
    public void SmoothMove_powerAttack(Transform original, Transform enemy) {
        //SmoothPushed(������ġ, ��ǥ��ġ(������ġ - 0.3f�� Z��) , �̵��ð�)�� ����
        StartCoroutine(SmoothPushed(original.position,
                                    enemy.position,
                                    smoothTime,
                                    2f));
    }
    #endregion

    #region �Ʒ��� gpt�� ¥�� z������ �ƴ� ���� �ݴ� �������� ������ �Լ���
    IEnumerator MoveAwayFromPlayer(Transform player, Transform enemy, float time) {
        Vector3 playerPosition = player.position;
        Vector3 enemyPosition = enemy.position;

        // �÷��̾�� ���� �����ϴ� ���� ���
        Vector3 awayFromPlayerVector = (enemyPosition - playerPosition).normalized;

        // ��ǥ ��ġ ���� (���� ��ġ���� ������ ��ġ)
        Vector3 targetPosition = transform.position + awayFromPlayerVector * 2f;

        Vector3 velocity = Vector3.zero;
        this.transform.position = playerPosition; // ���� ��ġ

        while (Vector3.Distance(this.transform.position, targetPosition) > 0.1f) { //���� ��ġ�� ��ǥ ��ġ ���� �Ÿ��� 0.1���� ū�� Ȯ���մϴ�.
            // �ε巴�� �̵�
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
