using System;
using UnityEngine;

public class BtnEvents
{
    public static event Action<MoreInfoUIData, RectTransform> OnItemButtonEnter;

    public static event Action OnItemButtonExit;

    public static event Action<MoreInfoUIData, RectTransform> OnPinBallBtnEnter;

    public static event Action OnPinBallBtnExit;

    public static event Action<MoreInfoUIData, RectTransform> OnEtcBtnEnter;

    public static event Action OnEtcBtnExit;

    public static void ItemPointeEnter(MoreInfoUIData data, RectTransform rect)
    {
        OnItemButtonEnter?.Invoke(data, rect);
    }

    public static void ItemPointeExit()
    {
        OnItemButtonExit?.Invoke();
    }
    
    public static void PinBallPointEnter(MoreInfoUIData data, RectTransform rect)
    {
        OnPinBallBtnEnter?.Invoke(data,rect);
    }

    public static void PinBallPointExit()
    {
        OnPinBallBtnExit?.Invoke();
    }

    public static void EtcPointEnter(MoreInfoUIData data, RectTransform rect)
    {
        OnEtcBtnEnter?.Invoke(data, rect);
    }

    public static void EtcPointExit()
    {
        OnEtcBtnExit?.Invoke();
    }
}
