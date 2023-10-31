using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
    public string description = "Item description here"; //아이템 설명 및 정보 입력

    public virtual void Use()
    {
        // 아이템 사용 로직을 구현 (각 아이템 유형에 따라 다름)
    }
}