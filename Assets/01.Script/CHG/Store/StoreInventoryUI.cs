using DG.Tweening;
using UnityEngine;

public class StoreInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject ItemInventory;
    [SerializeField] private GameObject PinBallInventory;
    [SerializeField] private Transform MovePos;

    private bool _itemInventoryShow = false;
    private bool _pinBallInventoryShow = false;

    private Vector3 _originalPos;

    private Tween _itemInventoryTween;
    private Tween _PinBallInventoryTween;

    private void Start()
    {
        _originalPos = ItemInventory.transform.position;

    }

    public void ItemInvemtoryShowHide()
    {
        _itemInventoryTween.Kill();

        if (!_itemInventoryShow)
        {
            Debug.Log("aa");
            _itemInventoryTween = ItemInventory.transform.DOMoveX(MovePos.position.x, 0.3f);
            _itemInventoryShow = true;
        }
        else
        {
            Debug.Log("bb");
            _itemInventoryTween = ItemInventory.transform.DOMoveX(_originalPos.x, 0.3f);
            _itemInventoryShow = false;
        }
    }

    public void PinBallInventoryShowHide()
    {
        _PinBallInventoryTween.Kill();

        if (!_pinBallInventoryShow)
        {
            _PinBallInventoryTween = PinBallInventory.transform.DOMoveX(MovePos.position.x, 0.3f);
            _pinBallInventoryShow = true;
        }
        else
        {
            _PinBallInventoryTween = PinBallInventory.transform.DOMoveX(_originalPos.x, 0.3f);
            _pinBallInventoryShow= false;
        }
    }
}
