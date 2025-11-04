using System.Collections.Generic;
using System.Linq;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private List<PinBallSO> PinBallData;
    [SerializeField]
    private List<ItemSO> ItemData;

    [SerializeField]
    private GameObject PinBallGroup;
    [SerializeField]
    private GameObject ItemGroup;

    private List<Image> _pinBallImgs;
    private List<Image> _itemImgs;
    
    [ContextMenu("Init")]
    public void InIt()
    {
        _pinBallImgs = PinBallGroup.GetComponentsInChildren<Image>().ToList();
        _itemImgs = ItemGroup.GetComponentsInChildren<Image>().ToList();

        RandomImg(_pinBallImgs, PinBallData);
        RandomImg(_itemImgs, ItemData);
    }

    private void RandomImg(List<Image> images, List<PinBallSO> datas)
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (datas.Count < 0) return;

            int r = Random.Range(0, datas.Count);
            images[i].sprite = datas[r].PinBallImage;
            datas.RemoveAt(r);
        }
    }
    private void RandomImg(List<Image> images, List<ItemSO> datas)
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (datas.Count < 0) return;

            int r = Random.Range(0, datas.Count);
            images[i].sprite = datas[r].itemIcon;
            datas.RemoveAt(r);
        }
    }
}
