using System;
using UnityEngine;

public class Player : Agent
{
    public Action<Player> PlayerDead;

    [HideInInspector]
    public Enemy PlayerTarget;
    private PlayerTurnManager _playerTurnManager;
    private EnemyTargeting _enemyTargeting;
    [SerializeField] private GameObject AttackEffack;
    [SerializeField] private GameObject AttackDamageEffack;

    private int _power = 1;
    public int AttackDamage { get; private set; } = 0;
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

        _power = PlayerManager.Instance.Power;
        HealthCompo.Init(PlayerManager.Instance.MaxHealth, PlayerManager.Instance.CurrentHealth);

        _enemyTargeting = GetComponent<EnemyTargeting>();
        _enemyTargeting.Init(this, contect);

        AgentAnimatorCompo.Init(_animator);

        _playerTurnManager = contect.PlayerTurnManager;
        _playerTurnManager.Init(this, _enemyTargeting, contect.TurnManager);

        HealthCompo.OnDamage += OnDamaged;
        HealthCompo.OnDead += OnDead;

        _contect.UIManager.PlayerHealthUIChange(HealthCompo.MaxHp, HealthCompo.CurHp);

        
    }

    private void OnDamaged()
    {
        _contect.UIManager.PlayerHealthUIChange(HealthCompo.MaxHp, HealthCompo.CurHp);
        Instantiate(AttackEffack, transform.position, Quaternion.identity);
    }

    public void ChangeTarget(Enemy enemy)
    {
        if (enemy == null)
        {
            PlayerTarget = null;
            return;
        }
        PlayerTarget = enemy;
        //AttackDamageCalculation(PlayerTarget.EnemyType, AttackDamage);
    }

    public void SetAttackDamage(int a)
    {
        AttackDamage = a;
        _contect.UIManager.DamageTextChange(AttackDamage);
    }

    public void AttackDamageCalculation(AttackType attackType, int baseDamage)
    {

        //AttackDamage += baseDamage;

        float powerMultiplier = 1 + (_power / 200f);

        float calculatedDamage = baseDamage * powerMultiplier;

        int finalDamage = Mathf.RoundToInt(calculatedDamage);

        AttackDamage = finalDamage;
        Debug.Log($"[Damage Calc] Base: {baseDamage} x Mult: {powerMultiplier} (P:{_power}) = Final: {finalDamage}");
        _contect.UIManager.DamageTextChange(finalDamage);


    }


    private void OnDead()
    {
        AgentAnimatorCompo.DeadPlay();
        _contect.UIManager.PlayerHealthUIChange(HealthCompo.MaxHp, 0);
        
    }
    public void DeadUiShow()
    {
        _contect.UIManager.GameOver();
    }
    public void PlayPlayerAttack()
    {
        PlayerTurnGA playerTurnGA = new();
        ActionSystem.Instance.Perform(playerTurnGA);
    }

    public void EnemyAttackEffactPlay()
    {
        Vector3 targetPos = PlayerTarget.transform.position;

        Instantiate(AttackEffack, new Vector2(targetPos.x, targetPos.y), Quaternion.identity);
        GameObject attackDamageEffact = Instantiate(AttackDamageEffack, new Vector2(targetPos.x, targetPos.y), Quaternion.identity);
        attackDamageEffact.GetComponent<AttackEffact>().AttackDamage(AttackDamage);
        _contect.UIManager.DamageTextChange(AttackDamage);
        
    }
    public void PlayerTurnStart()
    {
        _contect.TurnManager.PlayerTurnSet();
    }


    public void TakeDamage(int damage)
    {
        HealthCompo.TakeDamage(damage);
    }

    public void PlayerHealthReturn()
    {
        PlayerManager.Instance.SetHealth(HealthCompo.MaxHp, HealthCompo.CurHp);
    }

    #region test
    [ContextMenu("AddPower")]
    public void AddPower()
    {
        AttackDamageCalculation(AttackType.Normal, 400);
    }
    #endregion
}
