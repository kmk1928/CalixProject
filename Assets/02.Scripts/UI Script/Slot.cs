using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; // 획득한 아이템
    public Image itemImage; // 아이템의 이미지
    public int itemCount; // 획득한 아이템의 개수

    [SerializeField]
    private SlotToolTip theSlotToolTip; // 슬롯 툴팁 호출

    void Start()
    {
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
    }

    public void ShowToolTip(Item _item)
    {
        theSlotToolTip.ShowToolTip(_item);
    }

    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        SetColor(1);
    }

    // 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            ShowToolTip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
}