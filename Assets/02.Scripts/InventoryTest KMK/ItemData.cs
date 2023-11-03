using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Red,
    Yellow,
    Green
}

[System.Serializable]
public class ItemData
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;

    public bool Use()
    {
        return false;
    }
}
