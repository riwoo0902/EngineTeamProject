using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Game/Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public string characterName;

    [Header("기본 스탯")]
    public float maxHealth;
    public float adDefense; // 물리를 얼마나 막을 수 있는가
    public float apDefense; // 마법을 얼마나 막을 수 있는가
}
