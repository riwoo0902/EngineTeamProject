using UnityEngine;
using UnityEngine.UI;

public class ItemSetting : MonoBehaviour
{
    public ItemSO MyitemSO;
    public Image image;
    private void Start()
    {
        image.sprite = MyitemSO.itemIcon;
    }
    public void Enter()
    {
        ItemEX.Instance.EnterAC?.Invoke(MyitemSO);
    }
    public void Exit()
    {
        ItemEX.Instance.ExitAC.Invoke();
    }
}
