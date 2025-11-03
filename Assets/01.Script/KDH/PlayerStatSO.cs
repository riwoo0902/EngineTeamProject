using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "SO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    [Header("기본 스탯")]
    public float maxHealth;
    public float maxStamina;
    public float maxMana;

    [Header("공격 / 방어")]
    public float ADattack;
    public float APPower;
    public float ADdefense;
    public float APDefense;
}
