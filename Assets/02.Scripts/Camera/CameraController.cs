using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    public float rotSensitive = 3f;//ī�޶� ȸ�� ����
    public float dis = 3.5f;//ī�޶�� �÷��̾������ �Ÿ�
    public float RotationMin = -10f;//ī�޶� ȸ������ �ּ�
    public float RotationMax = 80f;//ī�޶� ȸ������ �ִ�
    public float smoothTime = 0.12f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    //�� 5���� value�� �������� ���ⲯ �˾Ƽ� ����������
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public Image diamondUI; //���� ǥ���� UI

    // ī�޶� Ÿ����ȯ�� �ʿ��� ���
    private bool isRotating = false; //ȸ�������� �Ǵ�
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float rotationDuration = 0.2f; // 2 seconds for the rotation
    private float rotationTime = 0;

    Vector3 cameraPosition; // ī�޶� ��ġ ������Ʈ�� ����
    private Vector3 lastCameraDirection; // ������ ������ ������ ����


    void LateUpdate()//Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {

        bool isTargeting = target.GetComponentInParent<PlayerController>().isTargeting; // PlayerScript���� �ٶ󺸴��� �Ǵ� �޾ƿ���

        // �ü��� ������ ����� �ְ� �ü� ���� ���̸� �� ����� �ٶ󺸵��� �����մϴ�.
        if (isTargeting)
        {
            // Player ��ũ��Ʈ�� targetToLookAt ������ ���� -> �ٶ� ��� �޾ƿ���
            Transform targetToLookAt = target.GetComponentInParent<PlayerController>().enemyToLookAt;

            if (targetToLookAt != null)
            {
                // ī�޶� Ÿ�� �����ϰ�
                if (!isRotating)
                {
                    startRotation = transform.rotation;
                    endRotation = Quaternion.LookRotation(targetToLookAt.position - transform.position);
                    isRotating = true;
                    rotationTime = 0;
                    StartCoroutine(RotateCamera());
                }
                // = target.LookAt(�ٶ󺸴� ���);

                // ������ ����� UI ��ġ ������Ʈ
                Vector3 uiPosition = Camera.main.WorldToScreenPoint(targetToLookAt.position);
                uiPosition.y += 10f;
                diamondUI.transform.position = uiPosition;

                // UI�� Ȱ��ȭ�մϴ�.
                diamondUI.gameObject.SetActive(true);



                cameraPosition = target.position - transform.forward * dis;
                cameraPosition.y = target.position.y + 1.0f;
                transform.position = cameraPosition;

            }
        }
        else
        {

            Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;
            Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;
            Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
            targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
            this.transform.eulerAngles = targetRotation;


            cameraPosition = target.position - transform.forward * dis;
            transform.position = cameraPosition;



            diamondUI.gameObject.SetActive(false);

        }




    }
    IEnumerator RotateCamera()
    {
        while (rotationTime < rotationDuration)
        {
            rotationTime += Time.deltaTime;
            float t = rotationTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}