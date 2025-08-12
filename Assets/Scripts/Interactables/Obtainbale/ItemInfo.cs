using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Item",menuName = "ScriptableObject/Item")]
public class ItemInfo : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] string _itemName;
    [HideInInspector]public string ItemName { get => _itemName; }

    [SerializeField] string _itemDescription;
    [HideInInspector]public string ItemDescription {  get => _itemDescription; }

    [SerializeField] public Sprite ItemIcon;

    [Header("Stackable")]
    [SerializeField] bool _isStackable;
    [HideInInspector] public bool IsStackable { get => _isStackable; }
    [SerializeField] int _maxItemCount;
    [HideInInspector] public int MaxItemCount { get => _maxItemCount; }
    [Header("Useable")]
    bool _isUseable;
    [HideInInspector] public bool IsUseable { get =>  _isUseable; }
    [HideInInspector] public IUseable[] Useables { get; set; }

    public void Init(IUseable[] useableList)
    {
        Useables = useableList;
        if(Useables.Length > 0)
            _isUseable = true;
        else
            _isUseable = false;
    }
}
