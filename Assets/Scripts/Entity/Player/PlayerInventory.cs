using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int? selectedSlot;
    InventorySlot[] slots;
    [SerializeField] PlayerInventoryInfo playerInventoryInfo;

    private void OnEnable()
    {
        selectedSlot = null;
        playerInventoryInfo.OnDisSelectItem();
    }

    void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>(true);
    }

    public void CollectItem(ItemObject item)
    {
        if(item == null) return;
        foreach (InventorySlot slot in slots)
        {
            if (slot.AddItem(item))
            {
                item.OnObtain();
                return;
            }
        }
    }

    public void OnSelectSlot(int slotnumber)
    {
        if (slots[slotnumber].ItemInfo == null)
        {
            selectedSlot = null;
            playerInventoryInfo.OnDisSelectItem();
        }
        else
        {
            selectedSlot = slotnumber;
            playerInventoryInfo.OnSelectItem(slots[slotnumber].ItemInfo);
        }
    }

    public void OnDrop()
    {
        if (selectedSlot == null || slots[(int)selectedSlot] == null) return;
        ItemObject dropitem = slots[(int)selectedSlot].itemObject;
        dropitem.parentPool.SpawnObject(PlayerManager.Instance.player.transform.position + PlayerManager.Instance.player.transform.forward);
        if (slots[(int)selectedSlot].RemoveItem())
        {
            selectedSlot = null;
            playerInventoryInfo.OnDisSelectItem();
        }
    }

    public void OnUse()
    {
        if (selectedSlot == null || slots[(int)selectedSlot] == null) return;
        if (!slots[(int)selectedSlot].ItemInfo.IsUseable)
            return;
        foreach (IUseable useable in slots[(int)selectedSlot].ItemInfo.Useables)
            useable.OnUse();
        if (slots[(int)selectedSlot].RemoveItem())
        {
            selectedSlot = null;
            playerInventoryInfo.OnDisSelectItem();
        }
    }
}
