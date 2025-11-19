using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [Header("Turn")]
    [SerializeField] private TextMeshProUGUI MoveTurnText;
    [SerializeField] private TextMeshProUGUI TurnText;
    [SerializeField] private Transform TurnTextMovePos;
    private Sequence _MoveTurnTextSeq;
    private Vector3 TurnTextOriginalPos;

    [Header("Health")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private TextMeshProUGUI HealthText;

    [Header("ItemInventory")]
    [SerializeField] private GameObject ItemInventory;
    [SerializeField] private Transform ItemInventoryMovePos;
    private Vector3 ItemInventoryOriginalPos;
    private Vector3 ItemInventoryScale;
    private Sequence _itemInventorySeq;
    private bool _ItemInventoryShow = false;

    [Header("EnemyTargeting")]
    [SerializeField] private Image TargetingImg;
    private Sequence _targetingImgSeq;

    [Header("PlayerInfo")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _powerText;

    [Header("Damage")]
    [SerializeField] private TextMeshProUGUI _damageText;

    [Header("Spell")]
    [SerializeField] private Image PlayerSpellImg;
    [SerializeField] private Image PlayerSpellImgBG;
    [SerializeField] private Image PinBallSpellImg;
    [SerializeField] private Image PinBallSpellImgBG;

    [Header("Item")]
    [SerializeField] private GameObject InventoryObj;
    [SerializeField] private GameObject ImgPrefab;

    [Header("EnemyInfoBar")]
    [SerializeField] private GameObject EnemyInfoBarPrefab;
    [SerializeField] private Sprite NomalImg;
    [SerializeField] private Sprite FireImg;
    [SerializeField] private Sprite GressImg;
    [SerializeField] private Sprite WaterImg;

    [SerializeField] private Image GameOverImg;
    public void Init()
    {

        TurnTextOriginalPos = MoveTurnText.transform.position;

        ItemInventoryOriginalPos = ItemInventory.transform.position;
        ItemInventoryScale = ItemInventory.transform.localScale;
        ItemInventoryAdd();

        _levelText.text = "Level:" + (StageManager.Instance.Level + 1);
        _coinText.text = PlayerManager.Instance.Gold.ToString();
        _powerText.text = PlayerManager.Instance.Power.ToString();

    }

    public void GameOver()
    {
        Debug.Log("AA");
        GameOverImg.gameObject.SetActive(true);
        GameOverImg.DOFade(1, 0.6f);
    }
    #region TurnText
    public void TurnTextSet(int turn, Action OnEndMove)
    {
        MoveTurnText.text = $"Turn {turn}";
        TurnText.text = $"Turn:{turn}";
        _MoveTurnTextSeq?.Kill();

        _MoveTurnTextSeq = DOTween.Sequence();

        _MoveTurnTextSeq.Append(MoveTurnText.transform.DOMove(TurnTextMovePos.transform.position, 1f).SetEase(Ease.OutQuint));
        _MoveTurnTextSeq.Append(MoveTurnText.transform.DOMove(TurnTextOriginalPos, 1f).SetEase(Ease.InQuint));
        _MoveTurnTextSeq.AppendCallback(() => OnEndMove?.Invoke());

    }
    #endregion

    #region PlayerHealthUI
    public void PlayerHealthUIChange(int maxHealth, int curHealth) //체력 변경
    {
        HealthText.text = $"{curHealth}/{maxHealth}";
        if (maxHealth == 0)
        {
            HealthBar.DOFillAmount(0f, 0.3f);
        }
        else
        {
            HealthBar.DOFillAmount((float)curHealth / maxHealth, 0.3f);
        }
    }
    #endregion

    #region ItemInvetory
    public void ItemInventoryAdd()
    {
        //추가

        foreach (var item in PlayerManager.Instance.testItems)
        {
            GameObject obj = Instantiate(ImgPrefab);
            Image img = obj.GetComponent<Image>();
            img.sprite = item.itemIcon;
            img.transform.SetParent(InventoryObj.transform, true);

        }

        ItemInventory.transform.position = ItemInventoryMovePos.position;
        ItemInventory.transform.localScale = Vector3.zero; //추가 후 닫기
    }

    public void ItemInventoryShowHide()
    {
        _itemInventorySeq?.Kill();

        _itemInventorySeq = DOTween.Sequence();

        if (!_ItemInventoryShow) //UI열기
        {
            _itemInventorySeq.Append(ItemInventory.transform.DOMove(ItemInventoryOriginalPos, 0.3f));
            _itemInventorySeq.Join(ItemInventory.transform.DOScale(ItemInventoryScale, 0.3f));

            _ItemInventoryShow = true;
        }
        else
        {
            _itemInventorySeq.Append(ItemInventory.transform.DOMove(ItemInventoryMovePos.position, 0.3f));
            _itemInventorySeq.Join(ItemInventory.transform.DOScale(Vector3.zero, 0.3f));

            _ItemInventoryShow = false;
        }
    } //인벤토리 열려있으면 닫고 닫혀있으면 열고
    #endregion

    #region TargetImg
    public void TargetingImgShow(Transform target)
    {
        _targetingImgSeq?.Kill();

        _targetingImgSeq = DOTween.Sequence();

        Vector3 targetPos = Camera.main.WorldToScreenPoint(target.position);
        _targetingImgSeq.Append(TargetingImg.transform.DOMoveX(targetPos.x, 0.3f).SetEase(Ease.OutQuint));
        _targetingImgSeq.Join(TargetingImg.DOFade(1, 0.3f));
    }

    public void TargetingImgHide()
    {
        _targetingImgSeq?.Kill();
        _targetingImgSeq = DOTween.Sequence();

        _targetingImgSeq.Append(TargetingImg.DOFade(0, 0.3f));
    }
    #endregion

    #region SpellImg
    public void SpellImgSet(Sprite playerIcon, Sprite pinBallIcon) //스펠 이미지 처음 세팅
    {
        PlayerSpellImg.sprite = playerIcon;
        PlayerSpellImgBG.sprite = playerIcon;

        PinBallSpellImg.sprite = pinBallIcon;
        PinBallSpellImgBG.sprite = pinBallIcon;
    }
    #endregion

    #region DamageText
    public void DamageTextChange(int damage) //데미치 표시 변경
    {
        float n = (float)damage / 100;

        _damageText.text = $"<shake a={n}>{damage}";
    }
    #endregion

    #region EnemyHealthBar
    public void EnemyInfoBarSet(GameObject enemyObj, Enemy enemy)
    {
        GameObject enemyHPbar = Instantiate(EnemyInfoBarPrefab, GameObject.Find("Canvas").transform);
        Vector3 vec3 = new Vector3(enemyObj.transform.position.x, enemyObj.transform.position.y, enemyObj.transform.position.z);
        enemyHPbar.transform.position = Camera.main.WorldToScreenPoint(vec3);

        enemy.EnemyInfoBar = enemyHPbar;
    }

    public Tween EnemyInfoBarHide(CanvasGroup canvasGroup)
    {
        return canvasGroup.DOFade(0, 0.3f);
    }

    public Tween EnemyInfoBarShow(Image healthBarImg, TextMeshProUGUI helathText, TextMeshProUGUI powerText, 
        Image typeImg, CanvasGroup canvasGroup, EnemyDataSO enemyData)
    {
        healthBarImg.fillAmount = 1;
        helathText.text = $"{enemyData.EnemyMaxHP}/{enemyData.EnemyMaxHP}";
        powerText.text = enemyData.EnemyPower.ToString();
        switch (enemyData.EnemyType)
        {
            case EnemyType.Normal:
                typeImg.sprite = NomalImg;
                break;
            case EnemyType.Fire:
                typeImg.sprite = FireImg;
                break;
            case EnemyType.Water:
                typeImg.sprite = WaterImg;
                break;
            case EnemyType.Gress:
                typeImg.sprite = GressImg;
                break;
        }


        return canvasGroup.DOFade(1, 0.3f);
    }

    public Tween EnemyHealthBarMove(Transform moveTrns, Transform target)
    {
        return moveTrns.DOMoveX(Camera.main.WorldToScreenPoint(target.position).x, 0.5f);
    }

    public void EnemyTakeDamage(Image healthBar, TextMeshProUGUI text, int maxHealth, int curHealth)
    {
        Debug.Log(curHealth);
        text.text = $"{curHealth}/{maxHealth}";
        healthBar.DOFillAmount((float)curHealth / maxHealth, 0.3f);

    }

    //으히히 너 털린거야
    /// <summary>
    /// 너 털린거야
    /// </summary>
    public void 너털린거야()
    {

    }
    #endregion
}


