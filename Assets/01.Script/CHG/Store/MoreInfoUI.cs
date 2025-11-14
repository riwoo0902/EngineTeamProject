using System;
using System.Text;
using TMPro;
using UnityEngine;

public class MoreInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject ImgRoot;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private float padding = 10f; 

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Canvas _rootCanvas; 
    private Vector3[] _buttonCorners = new Vector3[4]; 

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
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _rootCanvas = GetComponentInParent<Canvas>();

        HideInfo();
        _canvasGroup.interactable = false;
    }

    private void ShowInfo(MoreInfoUIData data, RectTransform buttonRect)
    {
        PriceText.text = data.PriceText;
        DescriptionText.text = data.DescriptionText;

        
        float canvasScale = _rootCanvas.scaleFactor;
       
        float panelWidth = _rectTransform.rect.width * canvasScale;

        buttonRect.GetWorldCorners(_buttonCorners);

        float spaceOnRight = Screen.width - _buttonCorners[2].x; //화면 오른쪽과 버튼 오른쪽 사이 거리
        float scaledPadding = padding * canvasScale; //

        Vector2 newPosition;
        Debug.Log($"Right: {spaceOnRight}, width: {panelWidth + scaledPadding}");
        if (spaceOnRight >= panelWidth + scaledPadding)
        {
            // [오른쪽에 표시]
            // 5. 정보창의 피벗을 왼쪽-중앙 (0, 0.5)으로 설정합니다.
            _rectTransform.pivot = new Vector2(0f, 0.5f);

            // 6. 위치를 버튼의 '오른쪽 위' 모서리 X좌표 + 여백으로 설정합니다.
            //    Y좌표는 버튼의 원래 position(중심)을 그대로 사용합니다.
            newPosition = new Vector2(_buttonCorners[2].x + scaledPadding, buttonRect.position.y);
        }
        else
        {
            // [왼쪽에 표시]
            // 5. 정보창의 피벗을 오른쪽-중앙 (1, 0.5)으로 설정합니다.
            _rectTransform.pivot = new Vector2(1f, 0.5f);

            // 6. 위치를 버튼의 '왼쪽 위' 모서리 X좌표 - 여백으로 설정합니다.
            newPosition = new Vector2(_buttonCorners[1].x - scaledPadding, buttonRect.position.y);
        }

        // 7. 계산된 위치를 적용합니다.
        _rectTransform.position = newPosition;

        // --- 계산 로직 끝 ---

        _canvasGroup.alpha = 1;
    }

    private void HideInfo()
    {
        _canvasGroup.alpha = 0;
    }
}