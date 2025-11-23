using _01.Script.Lrw.PinBallMap;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class BattleStageContect : MonoBehaviour
{
    [field: SerializeField] public BattleEnemyManager EnemyManager { get; private set; }
    [field: SerializeField] public EnemyTurnManager EnemyTurnManager { get; private set; }
    [field: SerializeField] public BattleTurnManager TurnManager { get; private set; }
    [field: SerializeField] public BattleUIManager UIManager { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public PlayerTurnManager PlayerTurnManager { get; private set; }
    [field: SerializeField] public BattleTurnButton TurnButton { get; private set; }
    
    public PinBallStageCreateManager PinBallStageCreateManager => PinBallStageCreateManager.Instance;
    [field: SerializeField] public Button ClearBtn;
    [field: SerializeField] public GameObject StageChoiceMap;
    public void Init(BattleStageDataSO stageData)
    {
        EnemyManager.Init(stageData, this);
        TurnManager.Init(this);
        Player.Init(this);
        TurnButton.Init(this);
        UIManager.Init();
        TurnManager.PlayerTurnSet();

        PinBallStageCreateManager.CreatMap(stageData.PinBallMap);
        if (ClearBtn != null) ClearBtn.onClick.AddListener(() => StageManager.Instance.SceneChange(MapType.MapChoice));
    }

    public void ChoiceMapCreate()
    {
        UIManager.ClearUI.gameObject.SetActive(false);  
        PinBallStageCreateManager.CreatMap(StageChoiceMap);
    }


}
