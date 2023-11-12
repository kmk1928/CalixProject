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
}