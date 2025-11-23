using System;
using System.Linq;
using _01.Script.Lrw.Inventory;
using DG.Tweening;
using Lrw_PinBall;
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
    private Vector3 _itemInventoryOriginalPos;
    private Vector3 _itemInventoryScale;
    private Sequence _itemInventorySeq;
    private bool _itemInventoryShow = false;
    [SerializeField] private GameObject PinBallInventory;
    [SerializeField] private Transform PinBallInventoryMovePos;
    private Vector3 _pinBallInventoryOriginalPos;
    private Vector3 _pinBallInventoryScale;
    private Sequence _pinBallInventorySeq;
    private bool _pinBallInventoryShow = false;

    [Header("EnemyTargeting")]
    [SerializeField] private Image TargetingImg;
    private Sequence _targetingImgSeq;

    [Header("PlayerInfo")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _powerText;

    [Header("Damage")]
    [SerializeField] private TextMeshProUGUI _damageText;

    [Header("Item")]
    [SerializeField] private GameObject InventoryObj;
    [SerializeField] private GameObject ImgPrefab;

    [Header("EnemyInfoBar")]
    [SerializeField] private GameObject EnemyInfoBarPrefab;
    [SerializeField] private Sprite NomalImg;
    [SerializeField] private Sprite FireImg;
    [SerializeField] private Sprite GressImg;
    [SerializeField] private Sprite WaterImg;

    [Header("ClearStage")]
    [SerializeField] private GameObject ClearUI;


    [SerializeField] private Image GameOverImg;
    public void Init()
    {

        TurnTextOriginalPos = MoveTurnText.transform.position;

        _itemInventoryOriginalPos = ItemInventory.transform.position;
        _itemInventoryScale = ItemInventory.transform.localScale;
        ItemInventoryUIAdd();

        _pinBallInventoryOriginalPos = PinBallInventory.transform.position;
        _pinBallInventoryScale = PinBallInventory.transform.localScale;
        PinBallInventory.transform.position = PinBallInventoryMovePos.transform.position;
        PinBallInventory.transform.localScale = Vector3.zero;

        _levelText.text = "Level:" + (StageManager.Instance.Level + 1);
        _coinText.text = "<sprite=0>" + PlayerManager.Instance.Gold;
        _powerText.text = PlayerManager.Instance.Power.ToString();

    }
    #region StageClear
    public void StageClear(int lootCoin, ItemSO item, PinBallSO pinBall)
    {
        ClearUI.SetActive(true);
        Button[] _lootBtns = ClearUI.GetComponentsInChildren<Button>();
        Image[] btnIcons = new Image[3];
        Image[] btnBG = new Image[3];
        TextMeshProUGUI[] texts = new TextMeshProUGUI[3];

        btnBG = ClearUI.GetComponentsInChildren<Image>()
            .Where(t => t != ClearUI.transform)
            .ToArray();

        for (int i = 0; i < _lootBtns.Length - 1; i++)
        {
            btnIcons[i] = _lootBtns[i].GetComponentInChildren<Image>();
            texts[i] = _lootBtns[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        try
        {
            btnIcons[1].sprite = item.itemIcon;
            btnIcons[2].sprite = pinBall.PinBallImage;

            texts[0].text = lootCoin.ToString();
            texts[1].text = item.itemName;
            texts[2].text = pinBall.BallName;
            Debug.Log("dd");

            _lootBtns[0].onClick.AddListener(() =>
            {
                PlayerManager.Instance.AddGold(lootCoin);
                foreach (Transform child in _lootBtns[0].transform)
                    Destroy(child.gameObject);
                Destroy(btnBG[1]);
            });

        }
        catch (Exception e) { Debug.LogException(e); }

        _lootBtns[1].onClick.AddListener(() =>
        {
            PlayerManager.Instance.HaveItem.Add(item);
            foreach (Transform child in _lootBtns[1].transform)
                Destroy(child.gameObject);
            Destroy(btnBG[2]);
        });
        _lootBtns[2].onClick.AddListener(() =>
        {
            foreach (Transform child in _lootBtns[2].transform)
                Destroy(child.gameObject);
            Destroy(btnBG[3]);
            PinballInventory.Instance.inventory.Add(pinBall);
        }); //핀볼 추가 만들기

    }
    #endregion

    #region GameOver
    public void GameOver()
    {
        GameOverImg.gameObject.SetActive(true);
        GameOverImg.DOFade(1, 0.6f);
    }
    #endregion

    #region TurnText
    public void TurnTextSet(int turn, Action OnEndMove)
    {
        Debug.Log("TurnTextSet");
        if (ClearUI.activeSelf) return;

        MoveTurnText.text = $"Turn {turn}";
        TurnText.text = $"Turn:{turn}";
        _MoveTurnTextSeq?.Kill();

        _MoveTurnTextSeq = DOTween.Sequence();

        _MoveTurnTextSeq.Append(MoveTurnText.transform.DOMove(TurnTextMovePos.transform.position, 1f).SetEase(Ease.OutQuint));
        _MoveTurnTextSeq.Append(MoveTurnText.transform.DOMove(TurnTextOriginalPos, 1f).SetEase(Ease.InQuint));
        _MoveTurnTextSeq.OnComplete(() => OnEndMove?.Invoke());

    }
    #endregion

    #region PlayerHealthUI
    public void PlayerHealthUIChange(int maxHealth, int curHealth) //체占쏙옙 占쏙옙占쏙옙
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

    #region InvetoryUI
    public void ItemInventoryUIAdd()
    {
        foreach (var item in PlayerManager.Instance.HaveItem)
        {
            GameObject obj = Instantiate(ImgPrefab);
            Image img = obj.GetComponent<Image>();
            obj.GetComponent<ItemInventoryIcon>().ItemData = item;
            img.sprite = item.itemIcon;
            img.transform.SetParent(InventoryObj.transform, true);

        }

        ItemInventory.transform.position = ItemInventoryMovePos.position;
        ItemInventory.transform.localScale = Vector3.zero; 
    }

    public void ItemInventoryUIShowHide()
    {
        _itemInventorySeq?.Kill();

        _itemInventorySeq = DOTween.Sequence();

        if (!_itemInventoryShow) 
        {
            _itemInventorySeq.Append(ItemInventory.transform.DOMove(_itemInventoryOriginalPos, 0.3f));
            _itemInventorySeq.Join(ItemInventory.transform.DOScale(_itemInventoryScale, 0.3f));

            _itemInventoryShow = true;
        }
        else
        {
            _itemInventorySeq.Append(ItemInventory.transform.DOMove(ItemInventoryMovePos.position, 0.3f));
            _itemInventorySeq.Join(ItemInventory.transform.DOScale(Vector3.zero, 0.3f));

            _itemInventoryShow = false;
        }
    } 
    public void PinBallInventoryShowHide()
    {
        _pinBallInventorySeq?.Kill();

        _pinBallInventorySeq = DOTween.Sequence();

        if (!_pinBallInventoryShow) 
        {
            _pinBallInventorySeq.Append(PinBallInventory.transform.DOMove(_pinBallInventoryOriginalPos, 0.3f));
            _pinBallInventorySeq.Join(PinBallInventory.transform.DOScale(_pinBallInventoryScale, 0.3f));

            _pinBallInventoryShow = true;
        }
        else
        {
            _pinBallInventorySeq.Append(PinBallInventory.transform.DOMove(PinBallInventoryMovePos.position, 0.3f));
            _pinBallInventorySeq.Join(PinBallInventory.transform.DOScale(Vector3.zero, 0.3f));

            _pinBallInventoryShow = false;
        }
    }

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

    #region DamageText
    public void DamageTextChange(int damage) //占쏙옙占쏙옙치 표占쏙옙 占쏙옙占쏙옙
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
            case AttackType.Normal:
                typeImg.sprite = NomalImg;
                break;
            case AttackType.Fire:
                typeImg.sprite = FireImg;
                break;
            case AttackType.Water:
                typeImg.sprite = WaterImg;
                break;
            case AttackType.Grass:
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
        text.text = $"{curHealth}/{maxHealth}";
        healthBar.DOFillAmount((float)curHealth / maxHealth, 0.3f);

    }


    #endregion
}


