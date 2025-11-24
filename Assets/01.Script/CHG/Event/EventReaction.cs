using System.Collections.Generic;
using _01.Script.Lrw.Inventory;
using Lrw_PinBall;
using UnityEngine;

public class EventReaction : MonoBehaviour
{
    public static EventReaction Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RetouchMaxHealth(int n)
    {
        if (n > 0)
        {
            PlayerManager.Instance.AddMaxHealth(n);
            PlayerManager.Instance.AddCurrentHealth(n);
        }
        else
            PlayerManager.Instance.SpendMaxHealth(n);
    }

    public void RetouchCurrentHealth(int n)
    {
        if (n > 0)
            PlayerManager.Instance.AddCurrentHealth(n);
        else
            PlayerManager.Instance.SpendCurrentHealth(n);
    }

    public void RetouchItem(List<ItemSO> items, int value)
    {
        if (items.Count != 0)
        {
            PlayerManager.Instance.AddItemValue(items);

        }
        else
        {
            int removeCount = PlayerManager.Instance.HaveItem.Count > value ? value : PlayerManager.Instance.HaveItem.Count;

            List<ItemSO> removeItems = new List<ItemSO>();
            for (int i = 0; i < removeCount; i++)
            {
                int r = Random.Range(0, PlayerManager.Instance.HaveItem.Count);
                removeItems.Add(PlayerManager.Instance.HaveItem[r]);
            }

            PlayerManager.Instance.RemoveItemValue(removeItems);
        }
    }

    public void RetouchPinBall(List<PinBallSO> pinBalls, int value)
    {
        if (pinBalls.Count != 0)
        {
            foreach (var item in pinBalls)
            {
                PinballInventory.Instance.inventory.Add(item);
            }
        }
        else
        {
            int removeCount = PinballInventory.Instance.inventory.Count > value ? value : PinballInventory.Instance.inventory.Count;

            for (int i = 0; i < removeCount; i++)
            {
                int r = Random.Range(0, PinballInventory.Instance.inventory.Count);
                PinballInventory.Instance.inventory.RemoveAt(r);
            }
        }
    }

}
