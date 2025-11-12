using System;

public class BtnEvents
{
    public static event Action<MoreInfoUIData> OnButtonEnter;

    public static event Action OnButtonExit;

    public static void PointeEnter(MoreInfoUIData data)
    {
        OnButtonEnter?.Invoke(data);
    }

    public static void PointeExit()
    {
        OnButtonExit?.Invoke();
    }
}
