using UnityEngine;

public class BattleStageContext : MonoBehaviour
{
    public EnemyStageManager EnemyManager { get; private set; }

    public void Init(BattleStageDataSO enemyData)
    {
        EnemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyStageManager>();

        Player player = GameObject.Find("Player").GetComponent<Player>();


        EnemyManager.Init(enemyData, player);
       
    }


}
