using System;
using System.Text;
using TMPro;
using UnityEngine;

public class MoreInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject ImgRoot;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    private StringBuilder _sb; //내용 저장
    private RectTransform _rectTransform;

    private void OnEnable()
    {
        BtnEvents.OnButtonEnter += ShowInfo;
        BtnEvents.OnButtonExit += HideInfo;

    }

    private void OnDisable()
    {
        BtnEvents.OnButtonEnter -= ShowInfo;
        BtnEvents.OnButtonExit -= HideInfo;
    }
    private void Start()
    {
        HideInfo();
    }

    private void ShowInfo(MoreInfoUIData data)
    {
        PriceText.text = data.PriceText;
        DescriptionText.text = data.DescriptionText;

        ImgRoot.SetActive(true);
    }


    private void HideInfo()
    {
        ImgRoot.SetActive(false);
    }
}
