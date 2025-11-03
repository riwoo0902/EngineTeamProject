using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int baseDamage = 20;
    public int baseDefense = 10;
    public int baseGold = 10;

    public int TotalAttack { get; private set; }
    public int TotalDefense { get; private set; }
    public int TotalGold { get; private set; }

    public List<ItemSO> inventory = new List<ItemSO>();

    //인벤토리 변경 시 호출할 액션
    public Action<List<ItemSO>> action;

    private void Start()
    {
        if (inventory.Count == 0)
        {
            action?.Invoke(inventory);
        }
    }

    public void AddItems(List<ItemSO> items)
    {
        foreach (var item in items)
        {
            AddItem(item); 
        }
    }

    // 단일 아이템 추가
    public void AddItem(ItemSO item)
    {
        if (item == null) return;

        inventory.Add(item);
        ApplyItemStats(item);

        Debug.Log($"[PlayerStats] {item.itemName} 추가됨, 공격력:{baseDamage}, 방어력:{baseDefense}, 골드:{baseGold}");
        action?.Invoke(inventory);
    }

    public void ApplyItemStats(ItemSO item)
    {
        foreach (var val in item.itemSetting)
        {
            switch (val.itemType)
            {
                case ItemType.Damage:
                    baseDamage += val.itemValue;
                    break;

                case ItemType.Defense:
                    baseDefense += val.itemValue;
                    break;

                case ItemType.Gold:
                    baseGold += val.itemValue;
                    break;
            }
        }
    }

    public void RemoveItemStats(ItemSO item)
    {
        foreach (var val in item.itemSetting)
        {
            switch (val.itemType)
            {
                case ItemType.Damage:
                    baseDamage -= val.itemValue;
                    break;

                case ItemType.Defense:
                    baseDefense -= val.itemValue;
                    break;

                case ItemType.Gold:
                    baseGold -= val.itemValue;
                    break;
            }
        }
    }
}