using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; 

    public GameObject gameoverUI;   // 게임 오버시 활성화 할 UI 게임 오브젝트
    public TMP_Text nanoText;          // 보유한 나노를 출력할 UI 텍스트

    public Slider playerHPBar;

    void Awake() {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null) {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
            Debug.LogWarning("UI 매니저 할당!!!");
        }
        else {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 UI매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }
    private void Start() {

    }

    public void UpdateNanoText(int newNano) {  //나노 UI 최신화
        nanoText.text = "Nano " + newNano;
    }

    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(true);     //게임오버 UI 활성화
    }
}
