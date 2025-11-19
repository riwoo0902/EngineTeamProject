using System;
using UnityEngine;

public class EventReaction : MonoSingleton<EventReaction>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void RetouchMaxHealth(int n)
    {
        if (n > 0)
            PlayerManager.Instance.AddMaxHealth(n);
        else
            PlayerManager.Instance.SpendMaxHealth(n);
    }

    public void RetouchCurrentHealth(int n)
    {
        if (n > 0)
            PlayerManager.Instance.AddCurrentHealth(n);
        else
            PlayerManager.Instance.SpendCurrentHealth(n);
    }


}
