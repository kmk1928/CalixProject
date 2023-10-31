using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text itemNameText;
    public Image itemIconImage;

    // UI 업데이트 함수
    public void UpdateUI(ItemData item)
    {
        if (item != null)
        {
            itemNameText.text = item.name;
            itemIconImage.sprite = item.icon;
        }
        else
        {
            itemNameText.text = "";
            itemIconImage.sprite = null;
        }
    }
}