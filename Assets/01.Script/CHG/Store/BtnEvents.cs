using System;
using UnityEngine;

public class BtnEvents
{
    public static event Action<MoreInfoUIData, RectTransform> OnButtonEnter;

    public static event Action OnButtonExit;

    public static void PointeEnter(MoreInfoUIData data, RectTransform rect)
    {
        OnButtonEnter?.Invoke(data, rect);
    }

    public static void PointeExit()
    {
        OnButtonExit?.Invoke();
    }
}
