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


    IEnumerator CurveMove(Vector3 startPos, Vector3 endPos, float height, float duration) {//Ŀ���̵�
        // �̵� ����� ���� ���
        float journeyLength = Vector3.Distance(startPos, endPos);
        // �̵� ���� �ð� ���
        float startTime = Time.time;
        // �̵��� �Ÿ� �ʱ�ȭ
        float distanceCovered = 0f;

        //// Raycast�� �̿��� �� ����
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, moveDirection, out hit, targetDistance, layerMaskForWalls)) {
        //    // ��ֹ�(��)�� �����Ǿ��ٸ�, �� �տ� �����ϵ��� ��ǥ ��ġ ����
        //    targetPosition = hit.point - moveDirection * 0.5f; // ������ ���� �Ÿ���ŭ �ڷ� �̵�
        //}

        while (distanceCovered < journeyLength) {
            // ��������� �̵� �ð��� ���
            float journeyTime = (Time.time - startTime) / duration;
            // ��� ���̸� ��� (�� �ڵ�� ��� �׸��� ���� ����)
            float heightOffset = height * 4.0f * journeyTime * (1 - journeyTime);
            // ���� ��ġ�� ���� ������ ����Ͽ� ���� (Vector3.Lerp �Լ��� ����Ͽ� ���� ��ġ�� �� ��ġ ���̸� �̵�)
            transform.position = Vector3.Lerp(startPos, endPos, journeyTime) + new Vector3(0f, heightOffset, 0f);
            // ������� �̵��� �Ÿ� ����
            distanceCovered = journeyLength * journeyTime;

            // ���� �����ӱ��� ���
            yield return null;
        }
        // �̵��� �Ϸ�Ǹ� ���� ������ �����ϰų� �ڷ�ƾ�� ����
    }  
}
