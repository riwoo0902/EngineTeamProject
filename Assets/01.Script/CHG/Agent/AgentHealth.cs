using System;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public Action OnDead;
    public Action OnDamage;

    private int _maxHp;
    private int _curHp;

    public int MaxHp
    {
        get { return _maxHp; }
    }
    public int CurHp
    {
        get { return _curHp; }
    }

    public void Init(int maxHealth)
    {
        ChangeMaxHp(maxHealth);
        _curHp = _maxHp;
    }
    public void Init(int maxHealth, int curHealth)
    {
        ChangeMaxHp(maxHealth);
        ChangeCurrentHp(curHealth);
    }

    public void ChangeMaxHp(int newValue)
    {
        int newMaxHp = Mathf.Clamp(newValue, 0, 999);

        if (newMaxHp < _curHp)
        {
            _curHp = newMaxHp;
        }

        _maxHp = newMaxHp;
    }

    private void ChangeCurrentHp(int newValue)
    {
        _curHp = Mathf.Clamp(newValue, 0, _maxHp);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        int newHp = _curHp - damage;

        ChangeCurrentHp(newHp);

        if (_curHp <= 0)
        {
            OnDead?.Invoke();
        }
        else
            OnDamage?.Invoke();
    }

    public void Heal(int heal)
    {
        if (heal <= 0) return;

        int newHp = _curHp + heal;

        ChangeCurrentHp(newHp);

    }

    //테스트용
    [ContextMenu("Dead")]
    private void Dead()
    {
        OnDead?.Invoke();
    }
}