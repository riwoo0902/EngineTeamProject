using System;
using System.Collections.Generic;
using _01.Script.CHG;
using Custom.MonoSingleton;
using UnityEngine;

public class PlayerManager : Custom.MonoSingleton.MonoSingleton<PlayerManager>
{
    public Action OnValueChanged;

    [field: SerializeField] public List<ItemSO> HaveItem { get; private set; }
    [SerializeField] private int _maxHealth = 0;
    private int _curHealth = 0;
    private int _power = 1;
    private int _gold = 0;
    private Player _stagePlayer;
    private PlayerTurnManager _playerTurnManager;
    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public int CurrentHealth
    {
        get { return _curHealth; }
    }

    public int Power
    {
        get
        {
            return _power;
        }
    }

    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = Mathf.Clamp(_gold + value, 0, 9999);
        }

    }

    protected override void Awake()
    {
        base.Awake();
        _curHealth = _maxHealth;
    }

    public void AddMaxHealth(int value)
    {
        _maxHealth = Mathf.Clamp(_maxHealth + value, 1, 999);
        OnValueChanged?.Invoke();
    }

    public void SpendMaxHealth(int value)
    {
        _maxHealth = Mathf.Clamp(_maxHealth - value, 1, 999);
        _curHealth = Mathf.Clamp(_curHealth, 1, _maxHealth);
        OnValueChanged?.Invoke();
    }

    public void AddCurrentHealth(int value)
    {
        _curHealth = Mathf.Clamp(_curHealth + value, 1, _maxHealth);
        OnValueChanged?.Invoke();
    }

    public void SpendCurrentHealth(int value)
    {
        _curHealth = Mathf.Clamp(_curHealth - value, 1, _maxHealth);
        OnValueChanged?.Invoke();
    }

    public void AddPower(int value)
    {
        _power += value;
        OnValueChanged?.Invoke();
    }

    public void SpendPower(int value)
    {
        _power -= value;
        OnValueChanged?.Invoke();
    }

    public void AddGold(int value)
    {
        _gold = Mathf.Clamp(_gold + value, 0, 9999);
        OnValueChanged?.Invoke();
    }

    public bool SpendGold(int value)
    {
        if (value < 0 || _gold < value) return false;

        _gold -= value;
        OnValueChanged?.Invoke();
        return true;
    }

    public void SetHealth(int maxHealth, int curHelath)
    {
        _maxHealth = Mathf.Clamp(maxHealth, 1, 999);
        _curHealth = Mathf.Clamp(curHelath, 1, _maxHealth);
    }

    private int itemValue_Health = 0;
    private int itemValue_Gold = 0;
    private int itemValue_Damage = 0;
    public void AddItemValue(List<ItemSO> item)
    {
        for(int i = 0; i < item.Count; i++)
        {
            for(int j = 0; j < item[i].itemSetting.Count; j++)
            {
                switch (item[i].itemSetting[j].itemType)
                {
                    case ItemType.Heal:
                        itemValue_Health += item[i].itemSetting[j].itemValue;
                        Debug.Log(itemValue_Health);
                        break;
                    case ItemType.Gold:
                        itemValue_Gold += item[i].itemSetting[j].itemValue;
                        break;
                    case ItemType.Damage:
                        itemValue_Damage += item[i].itemSetting[j].itemValue;
                        break;
                }
            }
        }
        SetItemValue();
    }
    public void RemoveItemValue(List<ItemSO> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            for (int j = 0; j < item[i].itemSetting.Count; j++)
            {
                switch (item[i].itemSetting[j].itemType)
                {
                    case ItemType.Heal:
                        Debug.Log("dd");
                        itemValue_Health -= item[i].itemSetting[j].itemValue;
                        break;
                    case ItemType.Gold:
                        Debug.Log("dd");
                        itemValue_Gold -= item[i].itemSetting[j].itemValue;
                        break;
                    case ItemType.Damage:
                        Debug.Log("dd");
                        itemValue_Damage -= item[i].itemSetting[j].itemValue;
                        break;
                }
            }
        }
        SetItemValue();
    }

    public void SetItemValue()
    {
        AddMaxHealth(itemValue_Health);
        AddCurrentHealth(itemValue_Health);
        AddGold(itemValue_Gold);
        AddPower(itemValue_Damage);

        itemValue_Damage = 0;
        itemValue_Health = 0;
        itemValue_Gold = 0;

        OnValueChanged?.Invoke();
        
    }


    #region Test

    [ContextMenu("SetItemValue")]
    public void TestSetItemValue()
    {
        AddItemValue(HaveItem);
    }
    [ContextMenu("SetItemRemoveValue")]
    public void TestRemoveItemValue()
    {
        RemoveItemValue(HaveItem);
    }
    [ContextMenu("AddGold")]
    private void AddGoldTest()
    {
        AddGold(1000);
    }

    [ContextMenu("AddHealth")]
    private void AddHealthTest()
    {
        AddMaxHealth(100);
        AddCurrentHealth(100);
    }
    #endregion
}