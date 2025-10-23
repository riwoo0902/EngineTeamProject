using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gold,
    Defense,
    Damage,
    Heal
}


[Serializable]
public class ItemValues
{
    [Header("Item Setting")]
    public ItemType itemType;
    public int itemValue;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/KM/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    public List<ItemValues> itemSetting = new List<ItemValues>();
    [Space(5)]
    [Header("Item UI")]
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
}
