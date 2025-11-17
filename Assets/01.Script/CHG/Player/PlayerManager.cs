using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private int _maxHealth = 0;
    private int _curHealth = 0;
    private int _power;
    private int _gold;
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

    #region Test
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