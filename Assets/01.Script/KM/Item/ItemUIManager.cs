using System;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    public Action<ItemSO> AddItem;
    public Action<ItemSO> RemoveItem;

    public Transform ItemParent;
    public GameObject ItemPrefab;
    public void Start()
    {
        AddItem += Add;
        RemoveItem += Remove;
    }

    private void Add(ItemSO item)
    {
        Instantiate(ItemPrefab, ItemParent);
    }
    
    private void Remove(ItemSO item)
    {
        
    }
}
