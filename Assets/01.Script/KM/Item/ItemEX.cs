using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemEX : MonoBehaviour
{
    static public ItemEX Instance;
    public GameObject ItemExUI;
    public TMP_Text NameText;
    public TMP_Text ExText;
    public GameObject Values;
    public GameObject GridParent;
    public Action<ItemSO> EnterAC;
    public Action ExitAC;
    public Dictionary<ItemType, Sprite> TypeSprites = new Dictionary<ItemType, Sprite>();
    public Dictionary<ItemType, string> TypeShortText = new Dictionary<ItemType, string>();
    [Header("Lists Order")]
    [Header("Same to")]
    [Header("ItemType Order")]
    [Header("Ya_Re")]
    [SerializeField] private List<Sprite> typeSpriteList = new List<Sprite>();
    [SerializeField] private List<string> typeToText = new List<string>();
    private void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        for (int i = 0; i < typeSpriteList.Count; i++)
        {
            TypeSprites.Add((ItemType)i, typeSpriteList[i]);
            TypeShortText.Add((ItemType)i, typeToText[i]);
        }
        EnterAC += Enter;
        ExitAC += Exit;
    }

    public void Enter(ItemSO item)
    {
        ItemExUI.SetActive(true);
        for (int i = 0; i < item.itemSetting.Count; i++)
        {
            Instantiate(Values, GridParent.transform).GetComponent<ItemValueSetting>().Setting(item, i);
        }
        NameText.text = item.itemName;
        ExText.text = item.itemDescription;
        Debug.Log(NameText.text);
    }

    public void Exit()
    {
        ItemExUI.SetActive(false);
        for (int i = 0; i < GridParent.transform.childCount; i++)
        {
            Destroy(GridParent.transform.GetChild(i).gameObject);
        }
    }
}
