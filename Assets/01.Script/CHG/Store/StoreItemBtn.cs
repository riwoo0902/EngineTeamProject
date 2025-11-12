using System;
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

    public static event Action<ItemSO> OnItemBtnEnter;
    public static event Action OnItemBtnExit;

    public void Init(ItemSO itemData)
    {
        _itemData = itemData;
        _img.sprite = _itemData.itemIcon;
        _price = _itemData.itemPrice;
        Debug.Log(_itemData.itemName);
        _nameText.text = _itemData.itemName;

        _button = GetComponent<Button>();
        _scale = transform.localScale;
        Debug.Log("InitClear");
    }

    public void BtnClick()
    {
        if (PlayerManager.Instance.Gold < _price) return;

        Debug.Log($"{_itemData.itemName} 획득");
        //아이템 획득 추가

        PlayerManager.Instance.Gold -= _price;

        _nameText.text = "SoldOut!";
        _button.interactable = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_scale * UpSize, 0.3f);

        MoreInfoUIData infoData = new MoreInfoUIData(
            Price: _itemData.itemPrice.ToString(),
            description: _itemData.itemDescription
        );

        BtnEvents.PointeEnter(infoData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_scale, 0.3f);

        BtnEvents.PointeExit();
    }
}
