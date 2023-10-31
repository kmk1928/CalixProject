using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   
    public ItemData[] inventory; // 아이템 스크립터블 오브젝트 배열

    // 아이템 추가 함수
    public void AddItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                // UI 업데이트 함수 호출
                return;
            }
        }
    }

    // 아이템 제거 함수
    public void RemoveItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
                // UI 업데이트 함수 호출
                return;
            }
        }
    }
}
