using System.Collections.Generic;
using System.Linq;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.UI;

public class StoreStageManager : MonoBehaviour
{
    [SerializeField] private GameObject PinBallGroup;
    [SerializeField] private GameObject ItemGroup;
    [SerializeField] private GameObject HealBtn;
    private List<StorePinBallBtn> _pinBallBtn; // 버튼 스크립트로 수정
    private List<StoreItemBtn> _itemBtn;

    private ItemSO[] _items;
    private PinBallSO[] _pinballs;
    public void InIt(ItemSO[] items, PinBallSO[] pinballs)
    {
        _pinBallBtn = PinBallGroup.GetComponentsInChildren<StorePinBallBtn>().ToList();
        _itemBtn = ItemGroup.GetComponentsInChildren<StoreItemBtn>().ToList();

        _items = items;
        _pinballs = pinballs;

        PinBallBtnSetting();

        ItemBtnSetting();

        HealBtn.GetComponent<StoreHealBtn>().Init();
    }

    public void PinBallBtnSetting()
    {
        PinBallSO[] ranPinBalls = _pinballs
        .OrderBy(item => Random.value)
        .Take(_pinBallBtn.Count)
        .ToArray();

        for (int i = 0; i < _pinBallBtn.Count; i++)
        {
            _pinBallBtn[i].Init(ranPinBalls[i]);
        }
    }
    public void ItemBtnSetting()
    {
        ItemSO[] ranItems = _items
        .OrderBy(item => Random.value)
        .Take(_pinBallBtn.Count)
        .ToArray();

        for (int i = 0; i < _itemBtn.Count; i++)
        {
            _itemBtn[i].Init(ranItems[i]);
        }
    }
}
