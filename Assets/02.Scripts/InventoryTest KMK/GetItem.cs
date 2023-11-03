using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public ItemData item;

    public void SetItem(ItemData _item)
    {
        item.itemName = _item.itemName;
        item.itemIcon = _item.itemIcon;
        item.itemType = _item.itemType;

    }

    public ItemData UseItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
