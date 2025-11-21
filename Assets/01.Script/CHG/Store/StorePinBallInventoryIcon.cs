using DG.Tweening;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorePinBallInventoryIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public PinBallSO PinBallData;
    private void Start()
    {
        if (PinBallData == null)
        {
            Destroy(this);
            return;
        }

        gameObject.GetComponent<Image>().sprite = PinBallData.PinBallImage;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MoreInfoUIData infoData = new MoreInfoUIData(
            name: PinBallData.BallName,
            price: "<sprite=0> " + PinBallData.BallPrice.ToString(),
            description: PinBallData.BallExplanation,
            mass: PinBallData.Mass,
            friction: PinBallData.Friction,
            bounciless: PinBallData.Bounciness

        );

        BtnEvents.PinBallEnter(infoData, gameObject.GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BtnEvents.PinBallExit();
    }
}
