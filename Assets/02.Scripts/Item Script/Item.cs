using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    public int itemCode; // 아이템의 코드번호
    public string itemName; // 아이템의 이름
    public ItemType itemType; // 아이템의 유형
    public Sprite itemImage; // 아이템의 이미지
    public GameObject itemPrefab; // 아이템의 프리팹

    public int healthEnergy; // 체력 변경량
    public int attackEnergy; // 공격력 변경량
    public int defenseEnergy; // 방어력 변경량

    [TextArea]
    public string itemDesc; // 아이템의 설명

    public enum ItemType
    {   
        Red,
        Yellow,
        Blue
    };
}