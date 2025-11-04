using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 0;
    private int _gold;
    private Player _stagePlayer;
    private PlayerTurnManager _playerTurnManager;
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            _gold = Mathf.Clamp(_gold, 0, 9999);
        }

    }

    public void AddMoney(int index)
    {
        _gold += index;
    }

    public bool SpendMoney(int index)
    {
        if (index <= 0 || _gold < index) return false;

        _gold -= index;

        return true;
    }

    //어떤 스테이지냐에 따라 나누기
    public void SceneLoaded()
    {
        _stagePlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    public void AddMaxHealth(int value) => MaxHealth += value;

    public void SubtractMaxHealth(int value) => MaxHealth -= value;




}
