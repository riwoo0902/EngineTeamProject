using UnityEngine;

public class Player : Agent
{
    [HideInInspector]
    public C_Enemy PlayerTarget;
    private PlayerTurnManager _playerTurnManager;
    private EnemyTargeting _enemyTargeting;

    protected override void Awake()
    {
        base.Awake();
    }
    public void Init()
    {
        base.Awake();
        Debug.Assert(PlayerManager.Instance != null, "PlayerManager is Null");
        HealthCompo.Init(PlayerManager.Instance.MaxHealth);

        _enemyTargeting = GetComponent<EnemyTargeting>();
        _enemyTargeting.Init(this);

        _playerTurnManager = GameObject.Find("PlayerTurnManager").GetComponent<PlayerTurnManager>();
        _playerTurnManager.Init(this, _enemyTargeting);
    }
    
    public void ChangeTarget(C_Enemy enemy)
    {
        PlayerTarget = enemy;
    }


}
