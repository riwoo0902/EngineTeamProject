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

    [Header("CurrentLevel")]
    [SerializeField] private TextMeshProUGUI _levelText;

    private void Start()
    {
        TurnTextOriginalPos = MoveTurnText.transform.position;

        ItemInventoryOriginalPos = ItemInventory.transform.position;
        ItemInventoryScale = ItemInventory.transform.localScale;
        ItemInventory.transform.position = ItemInventoryMovePos.position;
        ItemInventory.transform.localScale = Vector3.zero;

        _levelText.text = "Level:" + StageManager.Instance.Level;
    }
    public void TurnTextMove(int turn)
    {
        MoveTurnText.text = $"Turn {turn}";
        TurnText.text = $"Turn:{turn}";

        _MoveTurnTextSeq?.Kill();

        _MoveTurnTextSeq = DOTween.Sequence();

        _MoveTurnTextSeq.Append(MoveTurnText.transform.DOMove(TurnTextMovePos.transform.position, 1f).SetEase(Ease.OutQuint));
        _MoveTurnTextSeq.Append(MoveTurnText.transform.DOMove(TurnTextOriginalPos, 1f).SetEase(Ease.OutQuint));
    }

    public void HealthUIChange(int maxHealth, int curHealth)
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

    public void ItemInventoryAdd()
    {
        //추가
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
    }

    public void TargetingImgShow(Transform target)
    {
        _targetingImgSeq?.Kill();

        _targetingImgSeq = DOTween.Sequence();

        Vector3 targetPos = Camera.main.WorldToScreenPoint(target.position);
        _targetingImgSeq.Append(TargetingImg.transform.DOMove(targetPos, 0.3f).SetEase(Ease.OutQuint));
        _targetingImgSeq.Join(TargetingImg.DOFade(1, 0.3f));
    }
    
    public void TargetingImgHide()
    {
        _targetingImgSeq?.Kill();
        _targetingImgSeq = DOTween.Sequence();

        _targetingImgSeq.Append(TargetingImg.DOFade(0, 0.3f));
    }
}
