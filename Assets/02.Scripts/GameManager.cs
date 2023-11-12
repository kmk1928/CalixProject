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
    public static bool isInventory = false; // 인벤토리가 호출되면 true
    [SerializeField] private int playerNanoCount = 10; // 플레이어 나노 카운트

    public bool isBossBattle = false; //보스전투돌입

    // 게임 시작 시 각종 설정 및 초기화
    void Awake() {
        // GameManager 인스턴스가 없는 경우, 이 인스턴스를 유지하도록 함
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //씬 옮겨도 노파괴
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Start() 
    {  //나노 카운트 반영
        UIManager.instance.nanoText.text = "" + playerNanoCount;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        isPause = false;
        isInventory = false;
        isGameover = false;

        if (SceneManager.GetActiveScene().name == "01_StartScene") {
            // 모든 DontDestroyOnLoad 오브젝트 파괴
            GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
            foreach (GameObject obj in dontDestroyObjects) {
                Destroy(obj);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("06_BossStage");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("07_BounsStage1");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("07_BounsStage1");
        }

        if (isGameover && Input.GetKeyDown("k")) 
        { 
            GameRestart();
        }


        if( isPause || isInventory)
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

    #region 씬 관련 함수들 - 포탈, 사망, 메인메뉴
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("00_MainMenu 1");
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
        if (portalNum == 0)
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (portalNum == 1) // 스테이지1 -> 지도 -> 스테이지2
        {
            SceneManager.LoadScene("MapScene1");
        }
        else if (portalNum == 2) // 스테이지2 -> 지도 -> 스테이지3
        {
            // MapScene2_1, MapScene2_2, MapScene2_3 중에서 무작위로 선택
            string[] mapScene2 = { "MapScene2_1", "MapScene2_2", "MapScene2_3" };
            int randomIndex = Random.Range(0, mapScene2.Length);
            string randomMapScene2 = mapScene2[randomIndex];

            SceneManager.LoadScene(randomMapScene2);
        }
        else if (portalNum == 3)
        {
            // MapScene3_1, MapScene3_2, MapScene3_3 중에서 무작위로 선택
            string[] mapScene3 = { "MapScene3_1", "MapScene3_2", "MapScene3_3" };
            int randomIndex = Random.Range(0, mapScene3.Length);
            string randomMapScene3 = mapScene3[randomIndex];

            SceneManager.LoadScene(randomMapScene3);
        }
        else if (portalNum == 4)
        {
            SceneManager.LoadScene("BonusStage");
        }
        else if (portalNum == 5)
        {
            SceneManager.LoadScene("MapScene4");
        }
        else if (portalNum == 6)
        {
            SceneManager.LoadScene("07_BounsStage1");
        }
        else if (portalNum == 7)
        {
            SceneManager.LoadScene("06_BossStage 1");
        }
    }
    #endregion
}
