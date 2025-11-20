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
            //구매 실패
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