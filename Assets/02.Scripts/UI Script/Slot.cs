using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originPos;

    public Item item; // 획득한 아이템
    public Image itemImage; // 아이템의 이미지
    public int itemCount; // 획득한 아이템의 개수


    public PlayerStats playerStats;


    [SerializeField]
    private SlotToolTip theSlotToolTip; // 슬롯 툴팁 호출

    void Start()
    {
        originPos = transform.position;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right) //버튼 우클릭을 했을때
        {
            if(item != null) ///슬롯이 비었는지 판단
            {

                    if (item.itemType == Item.ItemType.Red)
                        playerStats.redCount -= 1;
                    else if (item.itemType == Item.ItemType.Yellow)
                        playerStats.yellowCount -= 1;
                    else
                        playerStats.blueCount -= 1;

                    
                    
                    Debug.Log(item.itemName + " 을 파괴했습니다.");
                    playerStats.RemoveItemModifiers(item);
                    SetSlotCount(-1);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
       if(item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
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