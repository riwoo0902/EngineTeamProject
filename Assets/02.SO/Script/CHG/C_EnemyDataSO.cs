using UnityEngine;

public enum EnemyType
{
    Normal,
    Fire,
    Water,
    Gress
}

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "C_SO/EnemyDataSO")]
public class C_EnemyDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int MaxHP { get; private set; }
    [field: SerializeField] public int Attack { get; private set; }
    [field: SerializeField] public EnemyType EnemyType { get; private set; } = EnemyType.Normal;
    
}
