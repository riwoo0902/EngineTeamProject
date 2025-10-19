using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(C_Enemy))]
public class C_Enemy : Agent
{
    public Action<C_Enemy> OnEnemyDead;
    private int _attack;

    protected override void Awake()
    {
        base.Awake();
    }
    //health, attack따로 두기
    public void Init(C_EnemyDataSO enemyData)
    {
        if (HealthCompo == null || enemyData == null) return;

        gameObject.name = enemyData.Name;
        _spriteRen.sprite = enemyData.Sprite;
        HealthCompo.Init(enemyData.MaxHP);
        HealthCompo.OnDead += EnemyDead;

        _spriteRen.DOFade(1, 0.7f);
    }

    public void EnemyDead()
    {
        _spriteRen.DOFade(0f, 0.7f).OnComplete(() => OnEnemyDead?.Invoke(this));
        HealthCompo.OnDead -= EnemyDead;
    }
}
