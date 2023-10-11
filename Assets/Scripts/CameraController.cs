using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    private float rotSensitive = 3f;//ī�޶� ȸ�� ����
    private float dis = 3f;//ī�޶�� �÷��̾������ �Ÿ�
    private float RotationMin = -10f;//ī�޶� ȸ������ �ּ�
    private float RotationMax = 80f;//ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    //�� 5���� value�� �������� ���ⲯ �˾Ƽ� ����������
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public Image diamondUI; //���� ǥ���� UI


    void LateUpdate()//Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {
        bool isTargeting = target.GetComponentInParent<PlayerController>().isTargeting; // PlayerScript�� �ü� ���� ���� ������ �߰��ؾ� �մϴ�.

        // �ü��� ������ ����� �ְ� �ü� ���� ���̸� �� ����� �ٶ󺸵��� �����մϴ�.
        if (isTargeting)
        {
            // Player ��ũ��Ʈ�� targetToLookAt ������ �����մϴ�.
            Transform targetToLookAt = target.GetComponentInParent<PlayerController>().enemyToLookAt;

            if (targetToLookAt != null)
            {
                // ī�޶��� ��ġ�� �����մϴ�.
                Vector3 cameraPosition = target.position - transform.forward * 4f;
                cameraPosition.y = target.position.y + 1.5f; // yourDesiredHeight�� ���ϴ� ���� ���� �����մϴ�.

                transform.position = cameraPosition;

                transform.LookAt(targetToLookAt); // ī�޶� Ÿ�� �����ϰ�

                // ������ ����� UI ��ġ ������Ʈ
                Vector3 uiPosition = Camera.main.WorldToScreenPoint(targetToLookAt.position);
                uiPosition.y += 10f;
                diamondUI.transform.position = uiPosition;

                // UI�� Ȱ��ȭ�մϴ�.
                diamondUI.gameObject.SetActive(true);
            }
        }
        else
        {
            Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
            Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
                                                                    //Xaxis�� ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

            Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
            //X��ȸ���� �Ѱ�ġ�� �����ʰ� �������ش�.

            targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
            this.transform.eulerAngles = targetRotation;
            //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��

            transform.position = target.position - transform.forward * dis;
            //ī�޶��� ��ġ�� �÷��̾�� ������ ����ŭ �������ְ� ��� ����ȴ�.

            diamondUI.gameObject.SetActive(false);
        }
    }
   
}