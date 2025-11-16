using System;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public Action OnDead;

    private int _maxHp;
    public int _curHp;
    

    public void Init(int value)
    {
        _maxHp = value;
        _curHp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        _curHp -= damage;
        
        if (_curHp <= 0) OnDead?.Invoke();    
    }

    public void Heal(int heal)
    {
        _curHp += heal;

        Mathf.Clamp(_curHp, 0, _maxHp);
    }

    //테스트용
    [ContextMenu("Dead")]
    private void Dead()
    {
        OnDead?.Invoke();
    }
}
