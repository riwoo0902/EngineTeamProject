using System.Text;
using _01.Script.Lrw.CustomSoundManager;
using DG.Tweening;
using Febucci.UI;
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
    [SerializeField] private TextAnimator_TMP _textAnimator;
    private StoreInventory _itemInventory;
    private SoundPlayer _soundPlayer;
    [field: SerializeField] private float UpSize { get; set; } = 1.3f;
    private Tween _failTween;
    public void Init(ItemSO itemData)
    {
        _button = GetComponent<Button>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _itemInventory = GameObject.Find("Inventorys").GetComponent<StoreInventory>();
        _img.gameObject.SetActive(true);
        _button.interactable = true;

        _itemData = itemData;
        _img.sprite = _itemData.itemIcon;
        _price = _itemData.itemPrice;
        _nameText.text = _itemData.itemName;

        _scale = transform.localScale;
    }

    public void BtnClick()
    {
        if (PlayerManager.Instance.SpendGold(_price))
        {
            _nameText.text = "SoldOut!";
            _button.interactable = false;
            gameObject.transform.DOScale(_scale, 0.1f);
            BtnEvents.ItemPointeExit();
            _img.gameObject.SetActive(false);

            PlayerManager.Instance.AddItemValue(new System.Collections.Generic.List<ItemSO> { _itemData });
            _itemInventory.AddItem?.Invoke(_itemData);
            _soundPlayer.SoundPlay();
        }
        else
        {
            Quaternion rotation = gameObject.transform.rotation;

            _failTween.Kill();

            _failTween = gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z + 5), 0.1f).OnComplete(() =>
            gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z - 5), 0.1f)
                ).SetLoops(2).OnComplete(() =>
               gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z), 0.1f));
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable == false) return;

        gameObject.transform.DOScale(_scale * UpSize, 0.1f);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < _itemData.itemSetting.Count; i++)
        {
            sb.Append($"{_itemData.itemSetting[i].itemType}: {_itemData.itemSetting[i].itemValue} / ");
        }
        sb.Length--;
        sb.Length--;

        MoreInfoUIData infoData = new MoreInfoUIData(
            name: _itemData.itemName,
            price: "<sprite=0> " + _itemData.itemPrice.ToString(),
            description: $"{_itemData.itemDescription}\n{sb.ToString()}"
        );

        BtnEvents.ItemPointeEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        gameObject.transform.DOScale(_scale, 0.1f);
        BtnEvents.ItemPointeExit();
    }
}
