using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    private int playerNanoCount = 10; // 보유 나노

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null) {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Debug.LogWarning("게임매니저 할당!!!");
        }
        else {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 게임매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }
    private void Start() {  //시작 시 보유중인 나노로 업데이트
        UIManager.instance.nanoText.text = ""+playerNanoCount;
    }

    void Update()
    {
        if(isGameover && Input.GetKeyDown("k")) {
            GameRestart();
        }
    }

    public void AddNano(int newNano) {
        if (!isGameover) {
            playerNanoCount += newNano;
            UIManager.instance.UpdateNanoText(playerNanoCount);
        }
    }
    public void OnPlayerDead() {    
        isGameover = true;      //게임오버
        UIManager.instance.SetActiveGameoverUI(true);
    }

    public void GameRestart() {
        SceneManager.LoadScene("01_StartScene");
    }

    public void SceneLoad() {
        ///SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("03_EnemyTestFeild");
    }
}
