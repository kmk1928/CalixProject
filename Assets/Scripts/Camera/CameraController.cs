using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    public float rotSensitive = 3f;//카메라 회전 감도
    public float dis = 3.5f;//카메라와 플레이어사이의 거리
    public float RotationMin = -10f;//카메라 회전각도 최소
    public float RotationMax = 80f;//카메라 회전각도 최대
    public float smoothTime = 0.12f;//카메라가 회전하는데 걸리는 시간
    //위 5개의 value는 개발자의 취향껏 알아서 설정해주자
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public Image diamondUI; //락온 표시할 UI

    // 카메라 타겟전환시 필요한 요소
    private bool isRotating = false; //회전중인지 판단
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float rotationDuration = 0.2f; // 2 seconds for the rotation
    private float rotationTime = 0;

    Vector3 cameraPosition; // 카메라 위치 업데이트용 변수
    private Vector3 lastCameraDirection; // 마지막 방향을 저장할 변수


    void LateUpdate()//Player가 움직이고 그 후 카메라가 따라가야 하므로 LateUpdate
    {

        bool isTargeting = target.GetComponentInParent<PlayerController>().isTargeting; // PlayerScript에서 바라보는지 판단 받아오기

        // 시선이 고정된 대상이 있고 시선 고정 중이면 그 대상을 바라보도록 설정합니다.
        if (isTargeting)
        {
            // Player 스크립트의 targetToLookAt 변수를 참조 -> 바라본 대상 받아오기
            Transform targetToLookAt = target.GetComponentInParent<PlayerController>().enemyToLookAt;

            if (targetToLookAt != null)
            {
                // 카메라가 타겟 고정하게
                if (!isRotating)
                {
                    startRotation = transform.rotation;
                    endRotation = Quaternion.LookRotation(targetToLookAt.position - transform.position);
                    isRotating = true;
                    rotationTime = 0;
                    StartCoroutine(RotateCamera());
                }
                // = target.LookAt(바라보는 대상);

                // 마름모 모양의 UI 위치 업데이트
                Vector3 uiPosition = Camera.main.WorldToScreenPoint(targetToLookAt.position);
                uiPosition.y += 10f;
                diamondUI.transform.position = uiPosition;

                // UI를 활성화합니다.
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