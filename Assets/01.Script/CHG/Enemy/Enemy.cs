using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Agent
{
    public Action<Enemy> OnEnemyDead;
    public int Attack { get; private set; }
    public EnemyDataSO EnemyData {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
    }
    //health, attack따로 두기
    public void Init(EnemyDataSO enemyData)
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
    //public bool EnemyMove(EnemySlot nextSlot, Action onComplete)
    //{

    //    StartCoroutine(EnemyMoved(nextSlot, onComplete));

        
    //    return false;


    //}

    private IEnumerator EnemyMoved(EnemySlot nextSlot)
    {
        bool endMove = false;

        gameObject.transform.DOMove(nextSlot.Pos.position, 0.5f)
                    .OnComplete(() => endMove = true);

        yield return new WaitUntil(() => endMove);
    }
    public void EnemyDead()
    {
        _spriteRen.DOFade(0f, 0.7f).OnComplete(() => OnEnemyDead?.Invoke(this));
        HealthCompo.OnDead -= EnemyDead;
    }
}
