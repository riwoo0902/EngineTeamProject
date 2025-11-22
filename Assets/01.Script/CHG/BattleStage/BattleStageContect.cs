using _01.Script.Lrw.PinBallMap;
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
    [field: SerializeField] public PinBallStageCreateManager PinBallStageCreateManager { get; private set; }
    public void Init(BattleStageDataSO stageData)
    {
        EnemyManager.Init(stageData, this);
        TurnManager.Init(this);
        Player.Init(this);
        TurnButton.Init(this);
        UIManager.Init();
        TurnManager.PlayerTurnSet();

        PinBallStageCreateManager.CreatMap(stageData.PinBallMap);

    }


}
