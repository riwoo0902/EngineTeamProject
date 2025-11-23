using System;
using Lrw_PinBall;
using UnityEngine;

public class StoreInventory : MonoBehaviour
{
    public Action<ItemSO> AddItem; //add
    public Action<ItemSO> RemoveItem; //remove

    public Action<PinBallSO> AddPinBall;
    public Action<PinBallSO> RemovePinBall;

    public Transform ItemParent;
    public Transform PinBallParent;
    public GameObject IconPrefab;

    public void Start()
    {
        AddItem += Add;
        AddPinBall += Add;

    }

    private void Add(ItemSO item)
    {
        Instantiate(IconPrefab, ItemParent).GetComponent<ItemInventoryIcon>().ItemData = item;
    }
    private void Add(PinBallSO pinBall)
    {
        Instantiate(IconPrefab,PinBallParent).GetComponent<StorePinBallInventoryIcon>().PinBallData = pinBall;
    }

    private void OnDestroy()
    {
        AddItem -= Add;
        AddPinBall -= Add;
    }
}
