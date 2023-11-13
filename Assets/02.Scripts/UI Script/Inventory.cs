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
    [SerializeField]
    private GameObject Inven_BackImg;
    [SerializeField]
    private GameObject go_Base;

    //슬롯
    private Slot[] slots;


    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !GameManager.isPause)
        {
            if (!InventoryActivated)
            {
                OpenInventory();
                InventoryActivated = true;
            }
            else
            {
                CloseInventory();
                InventoryActivated = false;
            }
        }
    }

    private void OpenInventory()
    {
        UIManager.isOpenUI = true;
        go_InventoryBase.SetActive(true);
        Inven_BackImg.SetActive(true);
        GameManager.isInventory = true;
        Time.timeScale = 0f;
    }

    private void CloseInventory()
    {
        UIManager.isOpenUI = false;
        go_InventoryBase.SetActive(false);
        Inven_BackImg.SetActive(false);
        go_Base.SetActive(false);
        GameManager.isInventory = false;
        Time.timeScale = 1f;
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        // 인벤토리가 가득 찼으면 획득할 수 없도록 처리
        if (IsInventoryFull())
        {
            Debug.LogWarning("인벤토리가 가득 찼습니다.");
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null || string.IsNullOrEmpty(slots[i].item.itemName))
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    // 인벤토리가 가득 찼는지 여부를 확인하는 함수
    public bool IsInventoryFull()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null || string.IsNullOrEmpty(slots[i].item.itemName))
            {
                return false;
            }
        }
        return true;
    }
}