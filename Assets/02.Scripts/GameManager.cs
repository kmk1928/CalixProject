using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool canPlayerMove = true; // 플레이어의 움직임 제어
    public static bool canPlayerRotate = true; // 카메라의 움직임 제어
    public static bool isGameover; // 플레이어 사망 여부 
    public static bool isPause = false; // 메뉴가 호출되면 true
    [SerializeField] private int playerNanoCount = 10; // 플레이어 나노 카운트

    //[Header("디버그용 인스펙터 표시용 변수들")]
    //public bool debug_canPlayerMove = true; // 플레이어의 움직임 제어
    //public bool debug_canPlayerRotate = true; // 카메라의 움직임 제어
    public bool debug_isGameover = false; // 플레이어 사망 여부 
    //public bool debug_isPause = false; // 메뉴가 호출되면 true


    // 게임 시작 시 각종 설정 및 초기화
    void Awake() {
        // GameManager 인스턴스가 없는 경우, 이 인스턴스를 유지하도록 함
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //씬 옮겨도 노파괴
            Debug.LogWarning("게임 매니저가 생성되었습니다!!!");
        }
        else {
            Debug.LogWarning("이미 다른 게임 매니저가 존재합니다. 이전 매니저를 파괴합니다.");
            Destroy(gameObject);
        }
    }
    private void Start() 
    {  //나노 카운트 반영
        UIManager.instance.nanoText.text = "" + playerNanoCount;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        isPause = false;
        isGameover = false;
    }

    void Update()
    {

        if(isGameover && Input.GetKeyDown("k")) 
        { 
            GameRestart();
        }


        if( isPause )
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canPlayerMove = false;
        }
        else
        {   
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            canPlayerMove = true;
        }
        //debug_canPlayerMove = canPlayerMove;
        //debug_canPlayerRotate = canPlayerRotate;
        debug_isGameover = isGameover;
        //debug_isPause = isPause;
    }

    public void AddNano(int newNano) {
        if (!isGameover) {
            playerNanoCount += newNano;
            UIManager.instance.UpdateNanoText(playerNanoCount);
        }
    }
    public void OnPlayerDead() {    
        isGameover = true;      //플레이어 사망
        UIManager.instance.SetActiveGameoverUI(true);
    }

    public void GameRestart() {
        SceneManager.LoadScene("01_StartScene");
    }

    public void SceneLoad_Battle() {
        ///SceneManager.LoadScene("SampleScene");
        ///

        GameManager.isGameover = false;
        SceneManager.LoadScene("03_EnemyTestFeild");
    }

    public void SceneLoad_normalMap(int portalNum) {
        ///SceneManager.LoadScene("SampleScene");
        ///
        if(portalNum == 0) {
            SceneManager.LoadScene("04_Normal Stage");
        }
        
    }

}
