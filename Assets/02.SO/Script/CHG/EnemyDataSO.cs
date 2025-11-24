using Lrw_PinBall;
using UnityEngine;

public enum AttackType
{
    None,
    Normal,
    Fire,
    Water,
    Grass
}

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "C_SO/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite EnemySprite { get; private set; }
    [field: SerializeField] public string EnemyName { get; private set; }
    [field: SerializeField] public int EnemyMaxHP { get; private set; }
    [field: SerializeField] public int EnemyPower { get; private set; }
    [field: SerializeField] public AttackType EnemyType { get; private set; } = AttackType.Normal;
    [field: SerializeField] public int LootCoin { get; private set; }
    [field: SerializeField] public PinBallSO LootPinBall { get; private set; }
    [field: SerializeField] public ItemSO LootItem { get; private set; }

    private void OnValidate()
    {
        name = EnemyName;
    }

}
