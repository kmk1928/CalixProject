using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShop : MonoBehaviour
{
    [SerializeField] public GameObject go_Shop;
    [SerializeField] private GameObject Shop_BackImg;
    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (playerController != null && playerController.nearObject != null && playerController.nearObject.CompareTag("NPC") && Input.GetKeyDown(KeyCode.E))
        {
            if (go_Shop != null)
            {
                if (!GameManager.isPause)
                {
                    GameManager.isPause = true;
                    UIManager.isOpenUI = true;
                    go_Shop.SetActive(true);
                    Shop_BackImg.SetActive(true);
                    Time.timeScale = 0.00001f;
                }
                else
                {
                    GameManager.isPause = false;
                    UIManager.isOpenUI = false;
                    go_Shop.SetActive(false);
                    Shop_BackImg.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
        }
    }

        // 체력 회복 함수
    public void Healing()
    {
        if (GameManager.instance.playerNanoCount >= 10)
        {
            PlayerStats playerStats = GameManager.instance.playerStats;
            playerStats.curHealth = playerStats.maxHealth;
            GameManager.instance.AddNano(-10);
        }
        else
        {
            Debug.Log("나노가 부족합니다.");
        }
    }


    // 공격력 상승 함수
    public void YellowStatUp()
    {
        if (GameManager.instance.playerNanoCount >= 50)
        {
            PlayerStats playerStats = GameManager.instance.playerStats;
            playerStats.attackDamage += 10;
            GameManager.instance.AddNano(-50);
        }
        else
        {
            // 골드가 부족하면 처리
            Debug.Log("나노가 부족합니다.");
        }
    }


    // 방어력 상승 함수
    public void BlueStatUp()
    {
        if (GameManager.instance.playerNanoCount >= 30)
        {
            PlayerStats playerStats = GameManager.instance.playerStats;
            playerStats.defense += 5;
            GameManager.instance.AddNano(-30);
        }
        else
        {
            // 골드가 부족하면 처리
            Debug.Log("나노가 부족합니다.");
        }
    }
}