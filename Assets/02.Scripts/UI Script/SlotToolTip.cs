using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotToolTip : MonoBehaviour
{


    //필요한 컴포넌트들
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private Image img_ItemIcon;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;

    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;


    public void ShowToolTip(Item _item)
    {
        go_Base.SetActive(true);

        img_ItemIcon.sprite = _item.itemImage;
        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}