internal class EnemyAttackGA : GameAction
{
    public Enemy AttackEnemy { get; set; }

    public EnemyAttackGA (Enemy enemy)
    {
        AttackEnemy = enemy;
    }
}
