using UnityEngine;

public class BattleStageContext : MonoBehaviour
{
    public StageEnemyManager EnemyManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }

    public void Init(C_EnemyStageDataSO enemyData, PlayerManager playerManager)
    {
        EnemyManager = GameObject.Find("EnemyManager").GetComponent<StageEnemyManager>();
        PlayerManager = playerManager;

        Player player = GameObject.Find("Player").GetComponent<Player>();


        EnemyManager.Init(enemyData, player);
        player.Init(PlayerManager);
    }


}
