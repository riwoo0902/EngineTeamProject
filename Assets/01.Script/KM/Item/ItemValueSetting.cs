using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemValueSetting : MonoBehaviour
{
    public Image TypeImage;
    public TMP_Text ValueText;
    public TMP_Text ValueNameText;
    public void Setting(ItemSO item, int index)
    {
        if (item.itemSetting[index].itemValue > 0)
        {
            ValueText.color = new Color(93 / 255, 255 / 255, 130 / 255);
        }
        else
        {
            ValueText.color = new Color(255 / 255, 108 / 255, 94 / 255);
        }
        ValueText.text = item.itemSetting[index].itemValue.ToString();
        TypeImage.sprite = ItemEX.Instance.TypeSprites.TryGetValue(item.itemSetting[index].itemType, out Sprite sprite) ? sprite : null;
        ValueNameText.text = ItemEX.Instance.TypeShortText.TryGetValue(item.itemSetting[index].itemType, out string shortText) ? shortText : "";
    }
}
