using UnityEngine;

public class BattleStageContext : MonoBehaviour
{
    public StageEnemyManager EnemyManager { get; private set; }

    public void Init(C_EnemyStageDataSO enemyData)
    {
        EnemyManager = GameObject.Find("EnemyManager").GetComponent<StageEnemyManager>();

        Player player = GameObject.Find("Player").GetComponent<Player>();


        EnemyManager.Init(enemyData, player);
       
    }


}
