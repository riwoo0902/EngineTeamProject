using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreRemoveBallBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI HealValueText;
    [SerializeField] private int AddMaxHealthValue = 10;

    [SerializeField] private int _price = 0;
    private Button _button;
    private Tween _failTween;
    public void Init()
    {
        _button = GetComponent<Button>();
    }
    public void BuyBttonClick()
    {
        if (PlayerManager.Instance.SpendGold(_price))
        {
            PlayerManager.Instance.AddMaxHealth(AddMaxHealthValue);

            HealValueText.text = "SoldOut!";
            _button.interactable = false;

        }
        else
        {
            Quaternion rotation = gameObject.transform.rotation;

            _failTween.Kill();

            _failTween = gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z + 5), 0.1f).OnComplete
                (() => gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z - 5), 0.1f)
                ).SetLoops(2);

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        MoreInfoUIData infoData = new MoreInfoUIData(
            name: "HealthHeal",
            price: _price.ToString(),
            description: $"구매 시 체력을 {AddMaxHealthValue}만큼 회복한다."
            );
        BtnEvents.PointeEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        BtnEvents.PointeExit();

    }

}