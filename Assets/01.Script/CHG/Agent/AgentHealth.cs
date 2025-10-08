using System;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public Action OnDead;

    private int _maxHp;
    private int _curHp;
    

    public void Init(C_EnemyDataSO enemyData)
    {
        _maxHp = enemyData.MaxHP;
    }

    public void GetDamage(int damage)
    {
        _curHp -= damage;

        if (_curHp <= 0) OnDead?.Invoke();
            
    }

    //테스트용
    [ContextMenu("EnemyDead")]
    private void EnemyDead()
    {
        OnDead?.Invoke();
    }
}
