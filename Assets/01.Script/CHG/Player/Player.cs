using UnityEngine;

public class Player : Agent
{
    [HideInInspector]
    public C_Enemy PlayerTarget;
    private PlayerTurnManager _playerTurnManager;
    private EnemyTargeting _enemyTargeting;
    [SerializeField] private GameObject AttackEffack;
    

    public int AttackDamage;
    private BattleStageContect _contect;
    protected override void Awake()
    {
        base.Awake();
    }
    public void Init(BattleStageContect contect)
    {
        _contect = contect;
        base.Awake();
        Debug.Assert(PlayerManager.Instance != null, "PlayerManager is Null");
        HealthCompo.Init(PlayerManager.Instance.MaxHealth, PlayerManager.Instance.CurrentHealth);

        _enemyTargeting = GetComponent<EnemyTargeting>();
        _enemyTargeting.Init(this);

        AgentAnimatorCompo.Init(_animator);

        _playerTurnManager = contect.PlayerTurnManager;
        _playerTurnManager.Init(this, _enemyTargeting, contect.TurnManager);

        HealthCompo.OnDead += PlayerDead;

        _contect.UIManager.HealthUIChange(HealthCompo.MaxHp, HealthCompo.CurHp);
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

    public void AttackEffactPlay()
    {
        Debug.Log("Attack");
        Instantiate(AttackEffack, PlayerTarget.transform.position, Quaternion.identity);
    }


    public void TakeDamage(int damage)
    {
        HealthCompo.TakeDamage(damage);
        _contect.UIManager.HealthUIChange(HealthCompo.MaxHp, HealthCompo.CurHp);
    }
}
