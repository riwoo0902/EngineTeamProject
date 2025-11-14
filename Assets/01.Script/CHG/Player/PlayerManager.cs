using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private int _maxHealth = 0;
    private int _curHealth = 0;
    private int _gold;
    private Player _stagePlayer;
    private PlayerTurnManager _playerTurnManager;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = Mathf.Clamp(_maxHealth + value, 1, 999);
        }
    }
    public int CurrentHealth
    {
        get { return _curHealth; }
        set
        {
            _curHealth = Mathf.Clamp(_maxHealth + value, 1, 999);
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

    public void AddGold(int value)
    {
        _gold += value;
    }

    public bool SpendGold(int value)
    {
        if (value <= 0 || _gold < value) return false;

        _gold -= value;

        return true;
    }

    //어떤 스테이지냐에 따라 나누기
    public void SceneLoaded()
    {
        _stagePlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    public void AddMaxHealth(int value) => MaxHealth += value;

    public void SpendMaxHealth(int value) => MaxHealth -= value;

    public void AddCurrentHealth(int value) => CurrentHealth += value;

    public void SpendCurrentHealth(int value) => CurrentHealth -= value;

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
    }
    #endregion

}
