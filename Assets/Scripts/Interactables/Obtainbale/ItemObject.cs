using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] public ItemPool parentPool;
    [SerializeField] public ItemInfo itemInfo;
    [SerializeField] public ItemObject itemPrefab;

    private void Awake()
    {
        if (itemInfo == null)
            throw new MissingComponentException("item info is missing");
        itemInfo.Init(this.gameObject.GetComponents<IUseable>());
    }



    public void OnObtain()
    {
        parentPool.DestroyObject(itemPrefab);
    }

    public void OnInteract()
    {
        PlayerManager.Instance.player.inventory.CollectItem(this);
    }
}
