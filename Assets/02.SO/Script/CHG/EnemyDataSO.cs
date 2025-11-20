using Lrw_PinBall;
using UnityEngine;

public enum EnemyType
{
    None,
    Normal,
    Fire,
    Water,
    Gress
}

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "C_SO/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite EnemySprite { get; private set; }
    [field: SerializeField] public string EnemyName { get; private set; }
    [field: SerializeField] public int EnemyMaxHP { get; private set; }
    [field: SerializeField] public int EnemyPower { get; private set; }
    [field: SerializeField] public EnemyType EnemyType { get; private set; } = EnemyType.Normal;
    [field: SerializeField] public int LootCoin { get; private set; }
    [field: SerializeField] public PinBallSO LootPinBall { get; private set; }
    [field: SerializeField] public ItemSO LootItem { get; private set; }
    
}
