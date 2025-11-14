using UnityEngine;

public class Player : Agent
{
    [HideInInspector]
    public C_Enemy PlayerTarget;
    private PlayerTurnManager _playerTurnManager;
    private EnemyTargeting _enemyTargeting;
    public int AttackDamage;

    
    protected override void Awake()
    {
        base.Awake();
    }
    public void Init(BattleTurnManager turnManager)
    {
        base.Awake();
        Debug.Assert(PlayerManager.Instance != null, "PlayerManager is Null");
        HealthCompo.Init(PlayerManager.Instance.MaxHealth);

        _enemyTargeting = GetComponent<EnemyTargeting>();
        _enemyTargeting.Init(this);

        AgentAnimatorCompo.Init(_animator);

        _playerTurnManager = GameObject.Find("PlayerTurnManager").GetComponent<PlayerTurnManager>();
        _playerTurnManager.Init(this, _enemyTargeting, turnManager);

        HealthCompo.OnDead += PlayerDead;
    }
    
    public void ChangeTarget(C_Enemy enemy)
    {
        PlayerTarget = enemy;
    }

    

    //가하는 데미지 계산
    public void AttackDamageCalculation(EnemyType type, int damage)
    {
        if (PlayerTarget.EnemyData.EnemyType == type)
        {
            AttackDamage = damage * 2;
        }
        AttackDamage = damage;
    }

    public int GetAttackDamage()
    {
        int damage = AttackDamage;
        AttackDamage = 0;
        return damage;
       
    }

    private void PlayerDead()
    {
        Debug.Log("PlayerDead");
    }

    public void PlayPlayerAttack()
    {
        PlayerTurnGA playerTurnGA = new();
        ActionSystem.Instance.Perform(playerTurnGA);
    }
}
