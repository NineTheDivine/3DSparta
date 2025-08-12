using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour, IInteractable
{
    [SerializeField] ItemPool pool;
    [SerializeField] Vector3 outPos;
    [SerializeField] SpriteRenderer vendingItemIcon;
    private void Start()
    {
        if (pool == null)
            throw new System.Exception("Vending Machine doesn't have base Pool");

        Sprite icon = pool.GetItemIcon();
        vendingItemIcon.sprite = icon;
        vendingItemIcon.transform.localScale = new Vector3(100.0f / (icon.rect.width * transform.localScale.x), 100.0f / (icon.rect.height * transform.localScale.y), 1.0f);
    }

    public void OnInteract()
    {
        pool.SpawnObject(this.transform.position + outPos);
    }

}
