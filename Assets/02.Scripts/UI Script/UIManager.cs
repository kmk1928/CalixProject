using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; 

    public GameObject gameoverUI;   // ���� ������ Ȱ��ȭ �� UI ���� ������Ʈ
    public GameObject gameClearUI;
    public TMP_Text nanoText;          // ������ ���븦 ����� UI �ؽ�Ʈ
    public Slider player_HPBar;
    public TMP_Text hpText;

    public static bool isOpenUI = false; //UI�� ���������� ������ �����ϴ� bool����
    
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //�� �Űܵ� ���ı�
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
   

    public void UpdateNanoText(int newNano) {  //���� UI �ֽ�ȭ
        nanoText.text =  newNano.ToString();
    }
    public void UpdateHPBar(float PSimsi) {  //���� UI �ֽ�ȭ
        player_HPBar.value = Mathf.Lerp(player_HPBar.value, PSimsi, Time.deltaTime * 25);
    }

    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(true);     //���ӿ��� UI Ȱ��ȭ
    }

    public void SetActiveGameclearUI(bool active) {
        gameClearUI.SetActive(true);     //���ӿ��� UI Ȱ��ȭ
    }

    public void UpdateHpText(float curHp, float maxHp)
    {
        hpText.text = curHp.ToString() + " / " + maxHp.ToString();
    }

}
