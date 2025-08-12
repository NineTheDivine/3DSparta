using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPool : MonoBehaviour
{
    [SerializeField] ItemObject itemObjectPrefab;
    public int itemCount;
    public int activateNumber = 0;

    public void Start()
    {
        for (int i = 0; i < itemCount; i++)
        {
            ItemObject item = Instantiate(itemObjectPrefab, transform);
            item.parentPool = this;
            item.gameObject.SetActive(false);
        }
    }


    public ItemObject SpawnObject(Vector3 spawnPos)
    {
        activateNumber++;
        if (activateNumber <= itemCount)
        {
            ItemObject item = transform.GetChild(0).GetComponent<ItemObject>();
            item.gameObject.SetActive(true);
            item.transform.SetParent(null);
            item.transform.position = spawnPos;
            return item;
        }
        else
        {
            ItemObject item = Instantiate(itemObjectPrefab, null);
            item.transform.position = spawnPos;
            return item;
        }
    }

    public void DestroyObject(ItemObject item)
    {
        if (activateNumber <= 0)
            throw new System.Exception("Negative number of object needs deleted");
        activateNumber--;
        if (activateNumber <= itemCount)
        {
            item.transform.SetParent(this.transform);
            item.gameObject.SetActive(false);
        }
        else
        {
            Destroy(item.gameObject);
        }
    }
}
