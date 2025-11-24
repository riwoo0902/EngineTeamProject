using TMPro;
using UnityEngine;

public class StorePinBall_Explanation : MonoBehaviour
{
    [SerializeField] private GameObject ImgRoot;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI pinBall_MassText;
    [SerializeField] private TextMeshProUGUI pinBall_FrictionText;
    [SerializeField] private TextMeshProUGUI pinBall_BouncilessText;
    [SerializeField] private float padding = 10f;

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Canvas _rootCanvas;
    private Vector3[] _buttonCorners = new Vector3[4];

    private void OnEnable()
    {
        BtnEvents.OnPinBallBtnEnter += ShowInfo;
        BtnEvents.OnPinBallBtnExit += HideInfo;
    }

    private void OnDisable()
    {
        BtnEvents.OnPinBallBtnEnter -= ShowInfo;
        BtnEvents.OnPinBallBtnExit -= HideInfo;
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
        pinBall_MassText.text = data.Mass.ToString();
        pinBall_FrictionText.text = data.Friction.ToString();
        pinBall_BouncilessText.text = data.Bounciless.ToString();

        float canvasScale = _rootCanvas.scaleFactor;

        buttonRect.GetWorldCorners(_buttonCorners);

        float scaledPadding = padding * canvasScale;

        Vector2 newPosition;


        //ÇÇ¹þ ¼³Á¤ ¿ÞÂÊ 
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
