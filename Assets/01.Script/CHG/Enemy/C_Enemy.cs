using System;
using DG.Tweening;
using UnityEngine;


public class C_Enemy : Agent
{
    public Action<C_Enemy> OnEnemyDead;
    public int Attack { get; private set; }
    public C_EnemyDataSO EnemyData {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
    }
    //health, attack따로 두기
    public void Init(C_EnemyDataSO enemyData)
    {
        if (HealthCompo == null || enemyData == null) return;
        EnemyData = enemyData;
        gameObject.name = enemyData.EnemyName;
        _spriteRen.sprite = enemyData.EnemySprite;
        HealthCompo.Init(enemyData.EnemyMaxHP);
        HealthCompo.OnDead += EnemyDead;
        Attack = enemyData.EnemyAttack;

        _spriteRen.DOFade(1, 0.7f);
    }

    public void EnemyDead()
    {
        _spriteRen.DOFade(0f, 0.7f).OnComplete(() => OnEnemyDead?.Invoke(this));
        HealthCompo.OnDead -= EnemyDead;
    }
}
