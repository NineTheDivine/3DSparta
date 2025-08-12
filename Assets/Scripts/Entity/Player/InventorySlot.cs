using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemObject itemObject;
    ItemInfo _itemInfo;
    [HideInInspector] public ItemInfo ItemInfo { get { return _itemInfo; } }
    [SerializeField] TextMeshProUGUI QuantityTxt;
    [SerializeField] Image itemimage;
    public int quantity;
    public bool isEmpty = true;

    private void OnEnable()
    {
        UpdateValues();
    }

    public bool AddItem(ItemObject item)
    {
        //ADD Item if slot is empty
        if (isEmpty)
        {
            itemObject = item.itemPrefab;
            _itemInfo = item.itemInfo;
            isEmpty = false;
            quantity++;
            UpdateValues();
            return true;
        }
        else
        {
            //If Current Item is not matched, false
            if (_itemInfo.ItemName != item.itemInfo.ItemName)
                return false;
            //if not stackable or quantity is maxquantity
            if (!_itemInfo.IsStackable || _itemInfo.MaxItemCount == quantity)
                return false;
            quantity++;
            UpdateValues();
            return true;
        }
    }

    public bool RemoveItem()
    {
        if (quantity <= 0)
            throw new System.Exception("Negative Quantity");
        quantity--;
        
        if (quantity == 0)
        {
            itemObject = null;
            _itemInfo = null;
            isEmpty = true;
        }
        UpdateValues();
        return isEmpty;
    }

    void UpdateValues()
    {
        if (isEmpty)
        {
            GetComponent<Button>().enabled = false;
            QuantityTxt.text = "";
            itemimage.sprite = null;
        }
        else if (!ItemInfo.IsStackable)
        {
            GetComponent<Button>().enabled = true;
            QuantityTxt.text = "";
            itemimage.sprite = ItemInfo.ItemIcon;
        }
        else
        {
            GetComponent<Button>().enabled = true;
            QuantityTxt.text = quantity.ToString();
            itemimage.sprite = ItemInfo.ItemIcon;
        }
    }
}
