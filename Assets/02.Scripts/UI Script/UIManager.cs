using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; 

    public GameObject gameoverUI;   // ���� ������ Ȱ��ȭ �� UI ���� ������Ʈ
    public TMP_Text nanoText;          // ������ ���븦 ����� UI �ؽ�Ʈ

    public Slider playerHPBar;

    void Awake() {
        // �̱��� ���� instance�� ����ִ°�?
        if (instance == null) {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
            Debug.LogWarning("UI �Ŵ��� �Ҵ�!!!");
        }
        else {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �� �� �̻��� UI�Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }
    private void Start() {

    }

    public void UpdateNanoText(int newNano) {  //���� UI �ֽ�ȭ
        nanoText.text = "Nano " + newNano;
    }

    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(true);     //���ӿ��� UI Ȱ��ȭ
    }
}
