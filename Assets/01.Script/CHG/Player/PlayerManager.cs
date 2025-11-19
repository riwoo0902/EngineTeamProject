using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
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

    public void AddMaxHealth(int value)
    {
        _maxHealth = Mathf.Clamp(_maxHealth + value, 1, 999);
        _curHealth = Mathf.Clamp(_curHealth, 1, _maxHealth);
    }

    public void SpendMaxHealth(int value)
    {
        _maxHealth = Mathf.Clamp(_maxHealth - value, 1, 999);
        _curHealth = Mathf.Clamp(_curHealth, 1, _maxHealth);
    }

    public void AddCurrentHealth(int value)
    {
        _curHealth = Mathf.Clamp(_curHealth + value, 1, _maxHealth);
    }

    public void SpendCurrentHealth(int value)
    {
        _curHealth = Mathf.Clamp(_curHealth - value, 1, _maxHealth);
    }

    public void AddPower(int value)
    {
        _power += value;
    }

    public void SpendPower(int value)
    {
        _power -= value;
    }

    public void AddGold(int value)
    {
        _gold = Mathf.Clamp(_gold + value, 0, 9999);
    }

    public bool SpendGold(int value)
    {
        if (value <= 0 || _gold < value) return false;

        _gold -= value;

        return true;
    }

    private int itemValue_Health = 0;
    private int itemValue_Gold = 0;
    private int itemValue_Damage = 0;
    public void AddItemValue(ItemSO[] item)
    {
        for(int i = 0; i < item.Length; i++)
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
    public void RemoveItemValue(ItemSO[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            for (int j = 0; j < item[i].itemSetting.Count; j++)
            {
                switch (item[i].itemSetting[j].itemType)
                {
                    case ItemType.Heal:
                        itemValue_Health -= item[i].itemSetting[j].itemValue;
                        break;
                    case ItemType.Gold:
                        itemValue_Gold -= item[i].itemSetting[j].itemValue;
                        break;
                    case ItemType.Damage:
                        itemValue_Damage -= item[i].itemSetting[j].itemValue;
                        break;
                }
            }
        }
    }

    private void SetItemValue()
    {
        AddMaxHealth(itemValue_Health);
        AddCurrentHealth(itemValue_Health);
        AddGold(itemValue_Gold);
        AddPower(itemValue_Damage);
    }


    #region Test
    [field:SerializeField] public ItemSO[] testItems { get; private set; }
    [ContextMenu("SetItemValue")]
    public void TestSetItemValue()
    {
        AddItemValue(testItems);
    }
    [ContextMenu("SetItemRemoveValue")]
    public void TestRemoveItemValue()
    {
        RemoveItemValue(testItems);
    }
    [ContextMenu("AddGold")]
    private void AddGoldTest()
    {
        AddGold(1000);
    }

    [ContextMenu("AddHealth")]
    private void AddHealthTest()
    {
        AddMaxHealth(5);
        AddCurrentHealth(5);
    }
    #endregion
}