using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public bool InventoryActivated = false;


    //필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;


    //슬롯
    private Slot[] slots;


    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (!InventoryActivated) {
                OpenInventory();
                InventoryActivated = true;
            }
            else {

                CloseInventory();
                InventoryActivated = false;
            }


        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null || string.IsNullOrEmpty(slots[i].item.itemName))
            {
                slots[i].AddItem(_item, _count);
                return;
            }    
        }
    }
}