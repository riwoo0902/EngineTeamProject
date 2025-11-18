using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;


public class Enemy : Agent
{
    public Action<Enemy> OnEnemyDead { get; set; }
    public int Power { get; private set; }
    public EnemyDataSO EnemyData { get; private set; }
    public EnemyType EnemyType { get; private set; } = EnemyType.Normal;

    public GameObject HealthBar { get; set; }
    private Image HealthBarImg { get; set; }
    private TextMeshProUGUI HealthText { get; set; }

    private BattleStageContect _contect = null;
    private Sequence _moveSeq;
    private Sequence _spriteRenSeq;
    private Sequence _deadSeq;
    protected override void Awake()
    {
        base.Awake();
    }
    //health, attack따로 두기
    public void Init(EnemyDataSO enemyData, BattleStageContect contect)
    {
        if (HealthCompo == null || enemyData == null) return;

        EnemyData = enemyData;
        gameObject.name = enemyData.EnemyName;
        _spriteRen.sprite = enemyData.EnemySprite;
        Power = enemyData.EnemyPower;
        EnemyType = enemyData.EnemyType;

        HealthCompo.Init(enemyData.EnemyMaxHP);
        HealthCompo.OnDead += OnDead;
        HealthCompo.OnDamage += OnDamage;

        if (_contect == null)
        {
            _contect = contect;
            HealthBarImg = HealthBar.GetComponent<Image>();
            HealthText = HealthBar.GetComponentInChildren<TextMeshProUGUI>();
        }


        _contect.UIManager.EnemyHealthBarMove(HealthBar.transform, transform);
        _contect.UIManager.EnemyHealthBarShow(HealthBarImg, HealthText, EnemyData.EnemyMaxHP);

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

        _moveSeq.Append(_contect.UIManager.EnemyHealthBarMove(HealthBar.transform, nextSlot.Pos));
        _moveSeq.Join(gameObject.transform.DOMove(nextSlot.Pos.position, 0.5f));
        _moveSeq.AppendCallback(() => endMove = true);

        yield return new WaitUntil(() => endMove);

        onComplite?.Invoke();

    }

    public void OnDamage()
    {
        Debug.Log("Damage실행");
        _contect.UIManager.EnemyTakeDamage(HealthBarImg, HealthText, HealthCompo.MaxHp, HealthCompo.CurHp);
        _spriteRenSeq?.Kill();

        _spriteRenSeq = DOTween.Sequence();

        _spriteRenSeq.Append(_spriteRen.DOColor(Color.red, 0.1f));
        _spriteRenSeq.Append(_spriteRen.DOColor(Color.white, 0.1f));
        _spriteRenSeq.SetLoops(loops: 4, LoopType.Restart);
        _spriteRenSeq.Play();
    }

    public void OnDead()
    {
        _deadSeq?.Kill();
        _deadSeq = DOTween.Sequence();

        HealthBarImg.fillAmount = 0;
        HealthText.text = "0/0";

        _deadSeq.Append(_contect.UIManager.EnemyHealthBarHide(HealthBarImg, HealthText));
        _deadSeq.Join(_spriteRen.DOFade(0f, 0.5f));
        _deadSeq.AppendCallback(() => OnEnemyDead?.Invoke(this));
 
        HealthCompo.OnDead -= OnDead;
        HealthCompo.OnDamage -= OnDamage;
    }
}
