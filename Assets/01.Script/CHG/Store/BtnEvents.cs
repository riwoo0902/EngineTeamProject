using System;
using UnityEngine;

public class BtnEvents
{
    public static event Action<MoreInfoUIData, RectTransform> OnButtonEnter;

    public static event Action OnButtonExit;

    public static event Action<MoreInfoUIData, RectTransform> OnPinBallBtnEnter;

    public static event Action OnPinBallBtnExit;

    public static void PointeEnter(MoreInfoUIData data, RectTransform rect)
    {
        OnButtonEnter?.Invoke(data, rect);
    }

    public static void PointeExit()
    {
        OnButtonExit?.Invoke();
    }
    
    public static void PinBallEnter(MoreInfoUIData data, RectTransform rect)
    {
        OnPinBallBtnEnter?.Invoke(data,rect);
    }

    public static void PinBallExit()
    {
        OnPinBallBtnExit?.Invoke();
    }
}
