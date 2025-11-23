using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;


public class Enemy : Agent
{
    public Action<Enemy> OnEnemyDead { get; set; }
    public int Power { get; private set; }
    public EnemyDataSO EnemyData { get; private set; }
    public AttackType EnemyType { get; private set; } = AttackType.Normal;

    public GameObject EnemyInfoBar { get; set; }
    private Image _healthBarImg;
    private TextMeshProUGUI _healthText;
    private TextMeshProUGUI _powerText;
    private Image _typeImg;
    private CanvasGroup _canvasGroup;

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
            Image[] imgs = EnemyInfoBar.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] texts = EnemyInfoBar.GetComponentsInChildren<TextMeshProUGUI>();

            _contect = contect;
            _healthBarImg = imgs[0];
            _healthText = texts[0];
            _powerText = texts[1];
            _typeImg = imgs[1];
            _canvasGroup = EnemyInfoBar.GetComponent<CanvasGroup>();
        }


        _contect.UIManager.EnemyHealthBarMove(EnemyInfoBar.transform, transform);
        _contect.UIManager.EnemyInfoBarShow(_healthBarImg, _healthText, _powerText, _typeImg, _canvasGroup, enemyData);

        _spriteRen.DOFade(1, 0.7f);

    }

    public IEnumerator EnemyMove(EnemySlot nextSlot)
    {
        bool endMove = false;

        _moveSeq?.Kill();
        _moveSeq = DOTween.Sequence();


        _moveSeq.Append(_contect.UIManager.EnemyHealthBarMove(EnemyInfoBar.transform, nextSlot.Pos));
        _moveSeq.Join(gameObject.transform.DOMove(nextSlot.Pos.position, 0.5f));
        _moveSeq.AppendCallback(() => endMove = true);

        yield return new WaitUntil(() => endMove);
    }



    public void OnDamage()
    {
        Debug.Log("Damage실행");
        _contect.UIManager.EnemyTakeDamage(_healthBarImg, _healthText, HealthCompo.MaxHp, HealthCompo.CurHp);
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

        _healthBarImg.fillAmount = 0;
        _healthText.text = "0/0";

        _deadSeq.Append(_contect.UIManager.EnemyInfoBarHide(_canvasGroup));
        _deadSeq.Join(_spriteRen.DOFade(0f, 0.5f));

        _deadSeq.AppendCallback(() => OnEnemyDead?.Invoke(this));

        HealthCompo.OnDead -= OnDead;
        HealthCompo.OnDamage -= OnDamage;
    }
}