using _01.Script.Lrw.CustomSoundManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class StoreHealBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI HealValueText;
    [SerializeField] private int AddMaxHealthValue = 10;
    [SerializeField] private int Price = 0;
    [SerializeField] private float UpSize = 1.3f;
    private Button _button;
    private Tween _failTween;
    private Vector3 _scale;
    private SoundPlayer _soundPlayer;

    public void Init()
    {
        _scale = gameObject.transform.localScale;
        _button = GetComponent<Button>();
        HealValueText.text = AddMaxHealthValue.ToString();
        _soundPlayer = GetComponent<SoundPlayer>();
    }
    public void BuyBttonClick()
    {
        if (PlayerManager.Instance.SpendGold(Price))
        {
            PlayerManager.Instance.AddMaxHealth(AddMaxHealthValue);

            HealValueText.text = "SoldOut!";
            _button.interactable = false;
            _soundPlayer.SoundPlay();
        }
        else
        {
            Quaternion rotation = gameObject.transform.rotation;

            _failTween.Kill();

            _failTween = gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z + 5), 0.1f).OnComplete(() => 
            gameObject.transform.DORotate(new Vector3(rotation.x, rotation.y, rotation.z - 5), 0.1f)
                ).SetLoops(2).OnComplete(() =>
               gameObject.transform.DORotate(new Vector3(rotation.x,rotation.y,rotation.z),0.1f));

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_button.interactable) return;
        gameObject.transform.DOScale(_scale * UpSize, 0.1f);
        MoreInfoUIData infoData = new MoreInfoUIData(
            name: "HealthHeal",
            price: "<Sprite=0>" + Price.ToString(),
            description: $"구매 시 체력을 {AddMaxHealthValue}만큼 회복한다."
            );
        BtnEvents.EtcPointEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_scale, 0.1f);
        BtnEvents.EtcPointExit();

    }

}
