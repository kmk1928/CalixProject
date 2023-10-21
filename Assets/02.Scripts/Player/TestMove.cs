using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour {
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    private float rotSensitive = 3f;//카메라 회전 감도
    private float dis = 3.5f;//카메라와 플레이어사이의 거리
    private float RotationMin = -10f;//카메라 회전각도 최소
    private float RotationMax = 80f;//카메라 회전각도 최대
    private float smoothTime = 0.12f;//카메라가 회전하는데 걸리는 시간
    //위 5개의 value는 개발자의 취향껏 알아서 설정해주자
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public Image diamondUI; //락온 표시할 UI

    // 카메라 타겟전환시 필요한 요소
    private bool isRotating = false;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float rotationDuration = 0.2f; // 2 seconds for the rotation
    private float rotationTime = 0;

    Vector3 cameraPosition; // 카메라 위치 업데이트용 변수

    private Vector3 lastCameraPosition; // 마지막 위치를 저장할 변수
    private bool wasTargeting = false; // 이전 프레임에서 시점 고정이었는지 여부를 저장할 변수

    void LateUpdate()//Player가 움직이고 그 후 카메라가 따라가야 하므로 LateUpdate
    {

        bool isTargeting = target.GetComponentInParent<PlayerController>().isTargeting; // PlayerScript에 시선 고정 상태 변수를 추가해야 합니다.
        // 현재 프레임의 시점 고정 여부를 저장

        // 시선이 고정된 대상이 있고 시선 고정 중이면 그 대상을 바라보도록 설정합니다.
        if (isTargeting) {
            // Player 스크립트의 targetToLookAt 변수를 참조합니다.
            Transform targetToLookAt = target.GetComponentInParent<PlayerController>().enemyToLookAt;

            if (targetToLookAt != null) {
                // 카메라가 타겟 고정하게
                if (!isRotating) { // 
                    startRotation = transform.rotation;
                    endRotation = Quaternion.LookRotation(targetToLookAt.position - transform.position);
                    isRotating = true;
                    rotationTime = 0;
                    StartCoroutine(RotateCamera());
                }
            }

            // 카메라의 위치를 조절합니다.
            cameraPosition = target.position - transform.forward * dis;
            cameraPosition.y = target.position.y + 1.0f; // yourDesiredHeight에 원하는 높이 값을 대입합니다.

            transform.position = cameraPosition;

            // 마름모 모양의 UI 위치 업데이트
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(targetToLookAt.position);
            uiPosition.y += 10f;
            diamondUI.transform.position = uiPosition;

            // UI를 활성화합니다.
            diamondUI.gameObject.SetActive(true);

            // 시점 고정 중일 때, 현재 위치를 마지막 위치로 저장
            lastCameraPosition = transform.position;

        }
        else {
            //위에 록온이 되었을 때 wasTargeting을 true로 했음
            if (wasTargeting) {
                // 시점 고정이 해제될 때, 마지막 위치를 시작 위치로 설정
                transform.position = lastCameraPosition;
                wasTargeting = false;       //한 프레임만 실행
            }
            else if (!wasTargeting) { //그냥 else로 해도 되지만 구분을 위해 else if로 함
                isRotating = false; // if문에서 카메라 회전 여부 판단
                #region 기본 카메라 시점 회전 = 카메라가 움직이게 해주는 것
                Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//마우스 좌우움직임을 입력받아서 카메라의 Y축을 회전시킨다
                Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//마우스 상하움직임을 입력받아서 카메라의 X축을 회전시킨다
                                                                        //Xaxis는 마우스를 아래로 했을때(음수값이 입력 받아질때) 값이 더해져야 카메라가 아래로 회전한다 

                Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
                //X축회전이 한계치를 넘지않게 제한해준다.

                targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
                this.transform.eulerAngles = targetRotation;
                //SmoothDamp를 통해 부드러운 카메라 회전
                #endregion


                cameraPosition = target.position - transform.forward * dis;
                transform.position = cameraPosition;
                //카메라의 위치는 플레이어보다 설정한 값만큼 떨어져있게 계속 변경된다.

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