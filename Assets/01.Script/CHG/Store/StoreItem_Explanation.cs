using System;
using System.Text;
using TMPro;
using UnityEngine;

public class StoreItem_Explanation : MonoBehaviour
{
    [SerializeField] private GameObject ImgRoot;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private float padding = 10f; 

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Canvas _rootCanvas; 
    private Vector3[] _buttonCorners = new Vector3[4]; 

    private void OnEnable()
    {
        BtnEvents.OnItemButtonEnter += ShowInfo;
        BtnEvents.OnItemButtonExit += HideInfo;
    }

    private void OnDisable()
    {
        BtnEvents.OnItemButtonEnter -= ShowInfo;
        BtnEvents.OnItemButtonExit -= HideInfo;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _rootCanvas = GetComponentInParent<Canvas>();

        HideInfo();
        _canvasGroup.interactable = false;
    }

    private void ShowInfo(MoreInfoUIData data, RectTransform buttonRect)
    {
        NameText.text = data.NameText;
        PriceText.text = data.PriceText;
        DescriptionText.text = data.DescriptionText;


        float canvasScale = _rootCanvas.scaleFactor;

        buttonRect.GetWorldCorners(_buttonCorners);

        float scaledPadding = padding * canvasScale;

        Vector2 newPosition;


        //피벗 설정 왼쪽 
        _rectTransform.pivot = new Vector2(0f, 0.7f);


        newPosition = new Vector2(_buttonCorners[2].x + scaledPadding, buttonRect.position.y);

        _rectTransform.position = newPosition;


        _canvasGroup.alpha = 1;
    }

    private void HideInfo()
    {
        _canvasGroup.alpha = 0;
    }
}