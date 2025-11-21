using _01.Script.Lrw.Inventory;
using DG.Tweening;
using Febucci.UI;
using Lrw_PinBall;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorePinBallBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private PinBallSO _pinBallData;
    private int _price;
    private Vector3 _scale;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextAnimator_TMP _textAnimator;
    private StoreInventory _pinBallInventory;
    [SerializeField] private float UpSize = 1.3f;
    private Tween _failTween;

    public void Init(PinBallSO pinBallData)
    {
        _pinBallData = pinBallData;
        _img.sprite = _pinBallData.PinBallImage;
        _price = _pinBallData.BallPrice;
        _nameText.text = _pinBallData.BallName;

        _button = GetComponent<Button>();
        _scale = transform.localScale;
        _button = GetComponent<Button>();
        _scale = transform.localScale;

        _pinBallInventory = GameObject.Find("Inventorys").GetComponent<StoreInventory>();
    }

    public void BtnClick()
    {
        if (PlayerManager.Instance.SpendGold(_price))
        {
            
            _nameText.text = "SoldOut!";
            _button.interactable = false;
            gameObject.transform.DOScale(_scale, 0.1f);
            BtnEvents.PointeExit();
            _img.gameObject.SetActive(false);

            _pinBallInventory.AddPinBall?.Invoke(_pinBallData);
        }
        else
        {
            Quaternion rotation = gameObject.transform.rotation;

            _failTween.Kill();

            _failTween = gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z + 5), 0.1f).OnComplete(() =>
            gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z - 5), 0.1f)
                ).SetLoops(2).OnComplete(() =>
               gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z), 0.1f));
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable == false) return;

        gameObject.transform.DOScale(_scale * UpSize, 0.1f);

        MoreInfoUIData infoData = new MoreInfoUIData(
            name: _pinBallData.BallName,
            price: "<sprite=0> " + _pinBallData.BallPrice.ToString(),
            description: _pinBallData.BallExplanation,
            mass: _pinBallData.Mass,
            friction: _pinBallData.Friction,
            bounciless: _pinBallData.Bounciness

        );

        BtnEvents.PinBallEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        gameObject.transform.DOScale(_scale, 0.1f);
        BtnEvents.PinBallExit();
    }
}
