using System;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    public Action<ItemSO> AddItem;
    public Action<ItemSO> RemoveItem;

    public Transform ItemParent;
    public GameObject ItemPrefab;

    public ItemSO TestItem;
    public void Start()
    {
        AddItem += Add;
        RemoveItem += Remove;
    }

    private void Add(ItemSO item)
    {
        Instantiate(ItemPrefab, ItemParent).GetComponent<ItemSetting>().MyitemSO = item;
    }

    [ContextMenu("Test Add Items")]
    private void TestAdd()
    {
        AddItem?.Invoke(TestItem);
    }
    
    private void Remove(ItemSO item)
    {
        
    }
}
