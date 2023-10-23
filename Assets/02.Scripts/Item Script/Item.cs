using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    public string itemName; // 아이템의 이름
    public ItemType itemtype; // 아이템의 유형
    public Sprite itemImage; // 아이템의 이미지
    public GameObject itemPrefab; // 아이템의 프리팹

    [TextArea]
    public string itemDesc; // 아이템의 설명


    public string weaponType; // 무기 유형

    public int value;
   
    public enum ItemType
    {   
        Magic,
        Reward,
        Used,
        Weapon
    };
}