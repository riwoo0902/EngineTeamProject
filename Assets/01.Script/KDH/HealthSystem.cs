using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float helath;
    public float Health
    {
        get
        {
            return helath;
        }
        private set
        {
            helath = Mathf.Clamp(value, 0, _maxHealth);
        }
    }

    [SerializeField] private int _maxHealth;

    private bool _isDead => Health <= 0;

    public Action OnDamaged;
    public Action OnDead;
    public Action OnDeal;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void GetDamage(DamageData damage)
    {
        Health -= damage.amount;
        OnDamaged?.Invoke();

        if (_isDead)
            OnDead?.Invoke();
    }

    public float GetNormalizeHelath()
    {
        return (float)Health / _maxHealth;
    }

    public void Deal(DamageData damage)
    {
        OnDeal?.Invoke();
    }
}
