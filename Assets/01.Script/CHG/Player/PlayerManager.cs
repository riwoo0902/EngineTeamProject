using UnityEngine;

public class    PlayerManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 0;
    private int _curGold;
    private Player _stagePlayer;
    private PlayerTurnManager _playerTurnManager;
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    //어떤 스테이지냐에 따라 나누기
    public void SceneLoaded()
    {
        _stagePlayer = GameObject.Find("Player").GetComponent<Player>();
       

    }

    public void AddMaxHealth(int value) => _maxHealth += value;

    public void SubtractMaxHealth(int value) => _maxHealth -= value;




}
