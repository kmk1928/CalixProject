using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour {
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    private float rotSensitive = 3f;//ī�޶� ȸ�� ����
    private float dis = 3.5f;//ī�޶�� �÷��̾������ �Ÿ�
    private float RotationMin = -10f;//ī�޶� ȸ������ �ּ�
    private float RotationMax = 80f;//ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    //�� 5���� value�� �������� ���ⲯ �˾Ƽ� ����������
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public Image diamondUI; //���� ǥ���� UI

    // ī�޶� Ÿ����ȯ�� �ʿ��� ���
    private bool isRotating = false;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float rotationDuration = 0.2f; // 2 seconds for the rotation
    private float rotationTime = 0;

    Vector3 cameraPosition; // ī�޶� ��ġ ������Ʈ�� ����

    private Vector3 lastCameraPosition; // ������ ��ġ�� ������ ����
    private bool wasTargeting = false; // ���� �����ӿ��� ���� �����̾����� ���θ� ������ ����

    void LateUpdate()//Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {

        bool isTargeting = target.GetComponentInParent<PlayerController>().isTargeting; // PlayerScript�� �ü� ���� ���� ������ �߰��ؾ� �մϴ�.
        // ���� �������� ���� ���� ���θ� ����

        // �ü��� ������ ����� �ְ� �ü� ���� ���̸� �� ����� �ٶ󺸵��� �����մϴ�.
        if (isTargeting) {
            // Player ��ũ��Ʈ�� targetToLookAt ������ �����մϴ�.
            Transform targetToLookAt = target.GetComponentInParent<PlayerController>().enemyToLookAt;

            if (targetToLookAt != null) {
                // ī�޶� Ÿ�� �����ϰ�
                if (!isRotating) { // 
                    startRotation = transform.rotation;
                    endRotation = Quaternion.LookRotation(targetToLookAt.position - transform.position);
                    isRotating = true;
                    rotationTime = 0;
                    StartCoroutine(RotateCamera());
                }
            }

            // ī�޶��� ��ġ�� �����մϴ�.
            cameraPosition = target.position - transform.forward * dis;
            cameraPosition.y = target.position.y + 1.0f; // yourDesiredHeight�� ���ϴ� ���� ���� �����մϴ�.

            transform.position = cameraPosition;

            // ������ ����� UI ��ġ ������Ʈ
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(targetToLookAt.position);
            uiPosition.y += 10f;
            diamondUI.transform.position = uiPosition;

            // UI�� Ȱ��ȭ�մϴ�.
            diamondUI.gameObject.SetActive(true);

            // ���� ���� ���� ��, ���� ��ġ�� ������ ��ġ�� ����
            lastCameraPosition = transform.position;

        }
        else {
            //���� �Ͽ��� �Ǿ��� �� wasTargeting�� true�� ����
            if (wasTargeting) {
                // ���� ������ ������ ��, ������ ��ġ�� ���� ��ġ�� ����
                transform.position = lastCameraPosition;
                wasTargeting = false;       //�� �����Ӹ� ����
            }
            else if (!wasTargeting) { //�׳� else�� �ص� ������ ������ ���� else if�� ��
                isRotating = false; // if������ ī�޶� ȸ�� ���� �Ǵ�
                #region �⺻ ī�޶� ���� ȸ�� = ī�޶� �����̰� ���ִ� ��
                Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
                Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
                                                                        //Xaxis�� ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

                Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
                //X��ȸ���� �Ѱ�ġ�� �����ʰ� �������ش�.

                targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
                this.transform.eulerAngles = targetRotation;
                //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��
                #endregion


                cameraPosition = target.position - transform.forward * dis;
                transform.position = cameraPosition;
                //ī�޶��� ��ġ�� �÷��̾�� ������ ����ŭ �������ְ� ��� ����ȴ�.

                diamondUI.gameObject.SetActive(false);
            }

        }


    }
    IEnumerator RotateCamera() {
        while (rotationTime < rotationDuration) {
            rotationTime += Time.deltaTime;
            float t = rotationTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}