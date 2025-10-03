using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(C_Enemy))]
public class C_Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRen;
    private int _maxHp;
    private int _curHp;
    private int _attack;
    public Action<C_Enemy> OnEnemyDead;
    private void Awake()
    {
        _spriteRen = GetComponent<SpriteRenderer>();
    }
    //health, attack따로 두기
    public void Init(C_EnemyDataSO enemyData)
    {
        gameObject.name = enemyData.Name;
        _spriteRen.sprite = enemyData.Sprite;
        _maxHp = enemyData.MaxHP;
        _attack = enemyData.Attack;
        _curHp = _maxHp;
        _spriteRen.DOFade(1, 0.4f);
    }

    [ContextMenu("InitCheack")]
    private void InitCheack()
    {
        Debug.Log($"Health: {_curHp}");
        Debug.Log($"Attack: {_attack}");
    }

    [ContextMenu("EnemyDead")]
    private void EnemyDead()
    {
        _spriteRen.DOFade(0f, 0.4f);
        OnEnemyDead?.Invoke(this);
    }

}
