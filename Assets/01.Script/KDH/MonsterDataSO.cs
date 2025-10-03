using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Scriptable Object / Monster Data", order = int.MaxValue)]
public class MonsterDataSO : ScriptableObject
{
    [SerializeField] private int monsterTribe;
    [SerializeField] private string monsterType;
    [SerializeField] private int monsterHealth;
    [SerializeField] private int monsterAttack;
    [SerializeField] private int monsterDamage;
    [SerializeField] private MonsterSpecialEffectSO specialEffects;

     public int MonsterTribe => monsterTribe;
    public string MonsterType => monsterType;
    public int MonsterHealth => monsterHealth;
    public int MonsterAttack => monsterAttack;
    public int MonsterDamage => monsterDamage;
    public MonsterSpecialEffectSO SpecialEffects => specialEffects;

    public void SetData(int Tribe, string Type, int Health, int Attack, int Damage, MonsterSpecialEffectSO specialEffects)
    {
        this.monsterTribe = Tribe;
        this.monsterType = Type;
        this.monsterHealth = Health;
        this.monsterAttack = Attack;
        this.monsterDamage = Damage;
        this.specialEffects = specialEffects;
    }
}
