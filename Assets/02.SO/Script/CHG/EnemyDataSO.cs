using UnityEngine;

public enum EnemyType
{
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
    
}
