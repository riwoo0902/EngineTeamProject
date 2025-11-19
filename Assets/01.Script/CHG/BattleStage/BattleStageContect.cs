using UnityEngine;

public class BattleStageContect : MonoBehaviour
{
    [field: SerializeField] public BattleEnemyManager EnemyManager { get; private set; }
    [field: SerializeField] public EnemyTurnManager  EnemyTurnManager { get; private set; }
    [field: SerializeField] public BattleTurnManager TurnManager { get; private set; }
    [field: SerializeField] public BattleUIManager UIManager { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public PlayerTurnManager PlayerTurnManager { get; private set; }
    [field: SerializeField] public BattleTurnButton TurnButton { get; private set; }
    public void Init(BattleStageDataSO enemyData)
    {
        EnemyManager.Init(enemyData, this);
        TurnManager.Init(this);
        Player.Init(this);
        TurnButton.Init(this);
        UIManager.Init();

        TurnManager.PlayerTurnSet();
        

    }


}
