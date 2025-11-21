using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StoreReRollBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int Price;
    [SerializeField] private float UpSize = 1.3f;
    [SerializeField] private StoreStageManager _stageStoreManager;
    [SerializeField] private TextMeshProUGUI PriceText;
    private Vector3 _scale;

    [SerializeField] private bool _thisItemButton;

    private void Start()
    {
        _scale = gameObject.transform.localScale;
        PriceText.text = Price.ToString();
    }

    private Tween _failTween;
    public void BtnClick()
    {
        if (PlayerManager.Instance.SpendGold(Price))
        {
            if (_thisItemButton)
                _stageStoreManager.ItemBtnSetting();
            else _stageStoreManager.PinBallBtnSetting();
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
        gameObject.transform.DOScale(_scale * UpSize, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_scale, 0.1f);
    }
}
