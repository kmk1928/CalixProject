using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryONOFF : MonoBehaviour
{
    public GameObject inventoryUI; // 인벤토리 UI 게임 오브젝트
    bool activeInventory = false;

    void Start()
    {
       
            inventoryUI.SetActive(activeInventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryUI.SetActive(activeInventory);
        }
    }
}
