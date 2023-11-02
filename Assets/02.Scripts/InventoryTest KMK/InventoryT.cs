using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryT : MonoBehaviour
{
    #region Singleton ½Ì±ÛÅæ
    public static InventoryT instance;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    private int slotCount;
    public int SlotCount
    {
        get => slotCount;
        set
        {
            slotCount = value;
        }
    }

    public delegate void OnChaneItem();
    public OnChaneItem onChaneItem;

    public List<ItemData> items = new List<ItemData>();
    private void Start()
    {
        SlotCount = 10;
    }

    public bool AddItem(ItemData _item)
    {
        if(items.Count<SlotCount)
        {
            items.Add(_item);
            if (onChaneItem != null)
            {
                onChaneItem.Invoke();
            }
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            GetItem getItem = other.GetComponent<GetItem>();
            if (AddItem(getItem.UseItem()))
                getItem.DestroyItem();
        }
    }
}
