using UnityEngine;

[RequireComponent(typeof(C_Enemy))]
public class C_Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRen;
    private int _maxHp;
    private int _curHp;
    private int _attack;
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
    }

    [ContextMenu("InitCheack")]
    private void IniCheack()
    {
        Debug.Log($"Health: {_curHp}");
        Debug.Log($"Attack: {_attack}");
    }

}
