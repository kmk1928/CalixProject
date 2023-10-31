using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryONOFF : MonoBehaviour
{
    public GameObject inventoryUI; // �κ��丮 UI ���� ������Ʈ
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
