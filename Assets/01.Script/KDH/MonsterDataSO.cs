using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class MonsterDataSO : ScriptableObject
{
    [SerializeField] private string monsterTribe;       // 종족
    [SerializeField] private string monsterType;        // 속성/타입
    [SerializeField] private string monsterHealth;      // 체력
    [SerializeField] private string monsterAttack;      // 공격력
    [SerializeField] private string monsterDamage;      // 데미지
    [SerializeField] private string specialEffect;      // 특수효과

    public string MonsterTribe => monsterTribe;
    public string MonsterType => monsterType;
    public string MonsterHealth => monsterHealth;
    public string MonsterAttack => monsterAttack;
    public string MonsterDamage => monsterDamage;
    public string SpecialEffect => specialEffect;

    public void SetData(string tribe, string type, string health, string attack, string damage, string effect)
    {
        monsterTribe = tribe;
        monsterType = type;
        monsterHealth = health;
        monsterAttack = attack;
        monsterDamage = damage;
        specialEffect = effect;
    }
}