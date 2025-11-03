internal class EnemyAttackGA : GameAction
{
    public C_Enemy AttackEnemy { get; set; }

    public EnemyAttackGA (C_Enemy enemy)
    {
        AttackEnemy = enemy;
    }
}
