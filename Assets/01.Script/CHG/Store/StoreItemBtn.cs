using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StoreItemBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private ItemSO _itemData;
    private int _price;
    private Vector3 _scale;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _nameText;
    [field: SerializeField] private float UpSize { get; set; } = 1.3f;

    public void Init(ItemSO itemData)
    {
        _itemData = itemData;
        _img.sprite = _itemData.itemIcon;
        _price = _itemData.itemPrice;
        _nameText.text = _itemData.itemName;

        _button = GetComponent<Button>();
        _scale = transform.localScale;
    }

    public void BtnClick()
    {
        Debug.Log(PlayerManager.Instance.Gold);
        Debug.Log(_price);
        if (PlayerManager.Instance.SpendGold(_price))
        {
            Debug.Log($"{_itemData.itemName} 획득");
            //아이템 획득 추가


            _nameText.text = "SoldOut!";
            _button.interactable = false;

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable == false) return;

        gameObject.transform.DOScale(_scale * UpSize, 0.1f);

        MoreInfoUIData infoData = new MoreInfoUIData(
            Price: _itemData.itemPrice.ToString(),
            description: _itemData.itemDescription
        );

        BtnEvents.PointeEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.interactable == false) return;

        gameObject.transform.DOScale(_scale, 0.1f);
        BtnEvents.PointeExit();
    }
}
