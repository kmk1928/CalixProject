using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoad : MonoBehaviour
{
    public string selectScene; // 확정 씬이동
    public string[] random; // 랜덤 씬이동용

    // 클릭 시 이벤트를 처리하는 함수
    private void OnMouseDown()
    {
        ChangeScene();
        ChangeRandomScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(selectScene);
    }

    // 씬을 변경하는 함수
    public void ChangeRandomScene()
    {

        // 배열에서 무작위로 씬을 선택합니다.
        int randomIndex = Random.Range(0, random.Length);
        string randomScene = random[randomIndex];

        // 선택된 씬을 로드합니다.
        SceneManager.LoadScene(randomScene);
    }
}

