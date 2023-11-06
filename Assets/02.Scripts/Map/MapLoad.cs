using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoad : MonoBehaviour
{
    public string sceneName; // 이동할 씬의 이름을 Inspector에서 설정할 변수

    // 클릭 시 이벤트를 처리하는 함수
    private void OnMouseDown()
    {
        ChangeScene();
    }

    // 씬을 변경하는 함수
    public void ChangeScene()
    {
        // sceneName 변수에 저장된 씬으로 이동
        SceneManager.LoadScene(sceneName);
    }
}

