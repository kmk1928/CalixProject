using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotToolTip : MonoBehaviour
{
    // 필요한 컴포넌트들
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private Image img_ItemIcon;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;

    [SerializeField]
    private TextMeshProUGUI txt_ItemGrade;

    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    public void ShowToolTip(Item _item)
    {
        go_Base.SetActive(true);

        img_ItemIcon.sprite = _item.itemImage;

        ItemNameColor(_item);

        ItemGradeColor(_item);

        txt_ItemDesc.text = _item.itemDesc;
    }



    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }



    private void ItemNameColor(Item _item)
    {
        if (_item.itemType == Item.ItemType.Red)
        {
            txt_ItemName.text = $"<color=red>{_item.itemName}</color>";
        }
        else if (_item.itemType == Item.ItemType.Blue)
        {
            txt_ItemName.text = $"<color=blue>{_item.itemName}</color>";
        }
        else if (_item.itemType == Item.ItemType.Yellow)
        {
            txt_ItemName.text = $"<color=yellow>{_item.itemName}</color>";
        }
        else
            txt_ItemName.text = _item.itemName;
    }

    private void ItemGradeColor(Item _item)
    {
        Color gradeColor;

        // ItemGrade에 따라 색상 설정
        switch (_item.itemGrade)
        {
            case Item.ItemGrade.Rare:
                gradeColor = new Color(0, 0.5f, 1);
                break;
            case Item.ItemGrade.Epic:
                gradeColor = new Color(0.5f, 0, 1);
                break;
            case Item.ItemGrade.Unique:
                gradeColor = new Color(1, 0.7f, 0);
                break;
            case Item.ItemGrade.Legendary:
                gradeColor = new Color(0, 0.7f, 0);
                break;
            default:
                gradeColor = Color.white;
                break;
        }

        txt_ItemGrade.color = gradeColor;
        txt_ItemGrade.text = _item.itemGrade.ToString();
    }
}