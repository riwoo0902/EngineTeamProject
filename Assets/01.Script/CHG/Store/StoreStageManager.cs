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
    private List<Image> _pinBallImgs; // 버튼 스크립트로 수정
    private List<StoreItemBtn> _itemBtn;
    
    [ContextMenu("Init")]
    public void InIt(ItemSO[] items)
    {

        _pinBallImgs = PinBallGroup.GetComponentsInChildren<Image>().ToList();
        _itemBtn = ItemGroup.GetComponentsInChildren<StoreItemBtn>().ToList();
        //PinBallSO[] pinballs = C_StageManager.Instance.StageDataManager.GetPinBallData(_pinBallImgs.Count);

        //PinBallBtnSetting(_pinBallImgs, pinballs);
        ItemBtnSetting(_itemBtn, items);

        HealBtn.GetComponent<StoreHealBtn>().Init();
    }

    private void PinBallBtnSetting(List<Image> images, PinBallSO[] datas)
    {
        foreach (var item in datas)
        {
            
        }

        for (int i = 0; i < images.Count; i++)
        {
            if (datas.Length < 0) return;

            int r = Random.Range(0, datas.Length);
            images[i].sprite = datas[r].PinBallImage;
        }
    }
    private void ItemBtnSetting(List<StoreItemBtn> btns, ItemSO[] datas)
    {
        Debug.Log("ButtonSetting");
        Debug.Log($"{btns.Count} items");
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].Init(datas[i]);
        }
    }
}
