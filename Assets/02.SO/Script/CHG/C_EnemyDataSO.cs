using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "C_SO/EnemyDataSO")]
public class C_EnemyDataSO : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public int MaxHP;
    public int Attack;
}
