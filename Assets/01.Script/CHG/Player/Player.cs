using UnityEngine;

public class Player : Agent
{
    [HideInInspector]
    public C_Enemy PlayerTarget;
    private PlayerTurnManager _playerTurnManager;


    protected override void Awake()
    {
        
    }
    public void Init(PlayerManager playerManager)
    {
        base.Awake();
        Debug.Assert(playerManager != null, "PlayerManager is Null");
        HealthCompo.Init(playerManager.MaxHealth);
        _playerTurnManager = GameObject.Find("PlayerTurnManager").GetComponent<PlayerTurnManager>();

        _playerTurnManager.Init(this);
    }
    
    public void ChangeTarget(C_Enemy enemy)
    {
        PlayerTarget = enemy;
    }
}
