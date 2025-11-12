using System.Collections.Generic;
using System.Linq;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.UI;

public class StoreStageManager : MonoBehaviour
{
    

    
    private GameObject _pinBallGroup;
    
    private GameObject _itemGroup;

    private List<Image> _pinBallImgs; // 버튼 스크립트로 수정
    private List<StoreItemBtn> _itemBtn;
    
    [ContextMenu("Init")]
    public void InIt()
    {

        _pinBallGroup = GameObject.Find("PinBallGroup");
        _itemGroup = GameObject.Find("ItemGroup");

        _pinBallImgs = _pinBallGroup.GetComponentsInChildren<Image>().ToList();
        _itemBtn = _itemGroup.GetComponentsInChildren<StoreItemBtn>().ToList();
        //PinBallSO[] pinballs = C_StageManager.Instance.StageDataManager.GetPinBallData(_pinBallImgs.Count);
        ItemSO[] items = C_StageManager.Instance.StageDataManager.GetItemData(_itemBtn.Count);

        //PinBallBtnSetting(_pinBallImgs, pinballs);
        ItemBtnSetting(_itemBtn, items);
    }

    //버튼에 정보 넣기로 수정
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
            //if (datas.Length > i) return;

            btns[i].Init(datas[i]);
        }
    }
}
