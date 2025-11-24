using DG.Tweening;
using UnityEngine;

public class StoreInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject ItemInventory;
    [SerializeField] private GameObject PinBallInventory;
    [SerializeField] private Transform FirstMovePos;
    [SerializeField] private Transform SecondMovePos;

    private bool _firstItemInventoryShow = false;
    private bool _secondItemInventoryShow = false;
    private bool _firstPinBallInventoryShow = false;
    private bool _secondPinBallInventoryShow = false;

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

        if (!_firstItemInventoryShow && !_secondItemInventoryShow)
        {
            if (_firstPinBallInventoryShow)
            {
                _itemInventoryTween = ItemInventory.transform.DOMoveX(SecondMovePos.position.x, 0.3f);
                _secondItemInventoryShow = true;
            }
            else
            {
                _itemInventoryTween = ItemInventory.transform.DOMoveX(FirstMovePos.position.x, 0.3f);
                _firstItemInventoryShow = true;

            }
        }
        else
        {
            _itemInventoryTween = ItemInventory.transform.DOMoveX(_originalPos.x, 0.3f);
            _firstItemInventoryShow = false;
            _secondItemInventoryShow = false;
        }
    }

    public void PinBallInventoryShowHide()
    {
        _PinBallInventoryTween.Kill();

        if (!_firstPinBallInventoryShow && !_secondPinBallInventoryShow)
        {
            if (_firstItemInventoryShow)
            {
                _PinBallInventoryTween = PinBallInventory.transform.DOMoveX(SecondMovePos.position.x, 0.3f);
                _secondPinBallInventoryShow = true;
            }
            else
            {
                _PinBallInventoryTween = PinBallInventory.transform.DOMoveX(FirstMovePos.position.x, 0.3f);
                _firstPinBallInventoryShow = true;

            }

        }
        else
        {
            _PinBallInventoryTween = PinBallInventory.transform.DOMoveX(_originalPos.x, 0.3f);
            _firstPinBallInventoryShow = false;
            _secondPinBallInventoryShow = false;
        }
    }
}
