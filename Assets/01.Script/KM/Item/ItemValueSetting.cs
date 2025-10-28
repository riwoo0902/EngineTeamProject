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
        ValueText.text = item.itemSetting[index].itemValue.ToString();
        TypeImage.sprite = ItemEX.Instance.TypeSprites.TryGetValue(item.itemSetting[index].itemType, out Sprite sprite) ? sprite : null;
        ValueNameText.text = ItemEX.Instance.TypeShortText.TryGetValue(item.itemSetting[index].itemType, out string shortText) ? shortText : "";
    }
}
