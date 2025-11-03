using System.Collections.Generic;
using UnityEngine;

public class PlayerActionTester : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats; // PlayerStats 연결
    [SerializeField] private ItemSO testItem;          // 테스트할 ItemSO 연결

    private void Start()
    {
        playerStats.action += OnInventoryChanged;
        playerStats.AddItem(testItem);
    }

    private void OnInventoryChanged(List<ItemSO> inventory)
    {
        Debug.Log($"인벤토리 변경 감지 현재 개수: {inventory.Count}");
        foreach (var item in inventory)
        {
            Debug.Log($"{item.itemName}");
        }
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.action -= OnInventoryChanged;
    }
}