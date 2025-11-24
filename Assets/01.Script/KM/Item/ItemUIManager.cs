using System;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    public Action<ItemSO> AddItem; //add
    public Action<ItemSO> RemoveItem; //remove

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
        Debug.Log("Add");
        Instantiate(ItemPrefab, ItemParent).GetComponent<ItemSetting>().MyitemSO = item;
    }

    [ContextMenu("Test Add Items")]
    public void TestAdd()
    {
        AddItem?.Invoke(TestItem);
    }
    
    private void Remove(ItemSO item)
    {
        
    }
}
