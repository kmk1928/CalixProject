using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShop : MonoBehaviour
{
    [SerializeField] public GameObject go_Shop;
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
                    go_Shop.SetActive(true);
                }
                else
                {
                    GameManager.isPause = false;
                    go_Shop.SetActive(false);
                }
            }
        }
    }
}