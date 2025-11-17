//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerActionTester : MonoBehaviour
//{
//    [SerializeField] private PlayerStats playerStats;
//    [SerializeField] private List<ItemSO> testItems;  
//    [SerializeField] private ItemSO testItem;  

//    private void Start()
//    {
//        playerStats.action += OnInventoryChanged;
//        playerStats.AddItems(testItems);
//        playerStats.AddItem(testItem);
//    }

//    private void OnInventoryChanged(List<ItemSO> inventory)
//    {
//        Debug.Log($"인벤토리 변경 감지 현재 개수: {inventory.Count}");
//        foreach (var item in inventory)
//        {
//            Debug.Log($"{item.itemName}");
//        }
//    }

//    private void OnDestroy()
//    {
//        if (playerStats != null)
//            playerStats.action -= OnInventoryChanged;
//    }
//}