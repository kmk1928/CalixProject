using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    InventoryT inven;

    public GameObject inventoryUI; // 인벤토리 UI 게임 오브젝트
    bool activeInventory = false;
    public SlotT[] slots;
    public Transform slotHolder;

    void Start()
    {
        inven = InventoryT.instance;
        slots = slotHolder.GetComponentsInChildren<SlotT>();
        inven.onChaneItem += RedrawSlotUI;
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

    public void Slot()
    {
        inven.SlotCount = inven.SlotCount;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
