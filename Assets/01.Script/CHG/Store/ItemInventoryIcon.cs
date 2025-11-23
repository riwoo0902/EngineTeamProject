using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventoryIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public ItemSO ItemData;
    private void Start()
    {

        if (ItemData == null)
        {
            Destroy(this);
            return;
        }
        gameObject.GetComponent<Image>().sprite = ItemData.itemIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        MoreInfoUIData infoData = new MoreInfoUIData(
            name: ItemData.itemName,
            price: "<sprite=0> " + ItemData.itemPrice.ToString(),
            description: ItemData.itemDescription
        );

        BtnEvents.ItemPointeEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BtnEvents.ItemPointeExit();
    }
}
