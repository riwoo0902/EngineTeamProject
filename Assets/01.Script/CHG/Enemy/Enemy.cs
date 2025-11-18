using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : Agent
{
    public Action<Enemy> OnEnemyDead;
    public int Power { get; private set; }
    public EnemyDataSO EnemyData { get; private set; }
    public EnemyType EnemyType { get; private set; } = EnemyType.Normal;

    public GameObject HealthBar { get; set; }
    private Image HealthBarImg { get; set; }
    private TextMeshProUGUI HealthText { get; set; }

    private Sequence _moveSeq;
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
        Power = enemyData.EnemyPower;
        EnemyType = enemyData.EnemyType;
        
        _spriteRen.DOFade(1, 0.7f);
        
    }
    public void EnemyMove(EnemySlot nextSlot, Action onComplete)
    {
        StartCoroutine(EnemyMoved(nextSlot, onComplete));
    }

    private IEnumerator EnemyMoved(EnemySlot nextSlot, Action onComplite)
    {
        bool endMove = false;

        _moveSeq?.Kill();
        _moveSeq = DOTween.Sequence();

        _moveSeq.Append(HealthBar.transform.DOMoveX(Camera.main.WorldToScreenPoint(nextSlot.Pos.position).x, 0.5f));
        _moveSeq.Join(gameObject.transform.DOMove(nextSlot.Pos.position, 0.5f));
        _moveSeq.AppendCallback(() => endMove = true); 

        yield return new WaitUntil(() => endMove);

        onComplite?.Invoke();

    }

    public void EnemyDead()
    {
        _spriteRen.DOFade(0f, 0.7f).OnComplete(() => OnEnemyDead?.Invoke(this));
        HealthBarImg.DOFade(0, 0.7f);
        HealthCompo.OnDead -= EnemyDead;
    }
}
