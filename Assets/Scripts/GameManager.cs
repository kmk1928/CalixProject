using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameover = false; // ���� ���� ����
    private int playerNanoCount = 10; // ���� ����

    // ���� ���۰� ���ÿ� �̱����� ����
    void Awake() {
        // �̱��� ���� instance�� ����ִ°�?
        if (instance == null) {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Debug.LogWarning("���ӸŴ��� �Ҵ�!!!");
        }
        else {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �� �� �̻��� ���ӸŴ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }
    private void Start() {  //���� �� �������� ����� ������Ʈ
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
        isGameover = true;      //���ӿ���
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
