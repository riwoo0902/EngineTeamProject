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

        PinBallBtnSetting(_pinBallBtn, pinballs
        .OrderBy(item => Random.value)
        .Take(_pinBallBtn.Count)
        .ToArray());

        ItemBtnSetting(_itemBtn, items
        .OrderBy(item => Random.value)
        .Take(_itemBtn.Count)
        .ToArray());

        HealBtn.GetComponent<StoreHealBtn>().Init();
    }

    private void PinBallBtnSetting(List<StorePinBallBtn> btns, PinBallSO[] datas)
    {
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].Init(datas[i]);
        }
    }
    private void ItemBtnSetting(List<StoreItemBtn> btns, ItemSO[] datas)
    {
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].Init(datas[i]);
        }
    }
}
