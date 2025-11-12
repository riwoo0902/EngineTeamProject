using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreHealBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] private int AddMaxHealthValue = 10;
 
    private int _price = 0;
    private Button _button;

    public void Init()
    {
        _button = GetComponent<Button>();
    }
    public void BuyBttonClick()
    {
        if (PlayerManager.Instance.SpendMoney(_price))
        {
            PlayerManager.Instance.AddMaxHealth(AddMaxHealthValue);
            _button.interactable = false;
        }
        else
        {
            //구매 실패
        }
    }
}
