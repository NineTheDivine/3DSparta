using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameTxt;
    [SerializeField] TextMeshProUGUI itemDescriptionTxt;
    [SerializeField] Button useButton;
    [SerializeField] Button dropButton;

    public void OnSelectItem(ItemInfo itemInfo)
    {
        this.gameObject.SetActive(true);
        itemNameTxt.text = itemInfo.ItemName;
        itemDescriptionTxt.text = itemInfo.ItemDescription;
        if (itemInfo.IsUseable)
            useButton.enabled = true;
        else
            useButton.enabled = false;
    }

    public void OnDisSelectItem()
    {
        this.gameObject.SetActive(false);
    }
}
