using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class C_StageManager : MonoSingleton<C_StageManager>
{
    [SerializeField] private Image TurnImage;

    public StageDataManager StageDataManager { get; private set; }
    public int Level { get; private set; }
    public bool CurTurn { get; private set; } = true; //true일시 플레이어 턴


    private void Start()
    {
        StageDataManager = GetComponent<StageDataManager>();
        
    }

    #region BattleScene
    [ContextMenu("BattleStageLoad")]
    private void BattleStageLoad()
    {
        GameObject.Find("BattleStageContext").GetComponent<BattleStageContext>().Init(StageDataManager.GetBattleData());
        PlayerTurnSet(); //플레이어 턴으로 시작
    }

    public void EnemyTurnSet()
    {
        CurTurn = false;
        TurnImage.DOColor(Color.red, 0);

    }
    public void PlayerTurnSet()
    {
        CurTurn = true;
        TurnImage.DOColor(Color.blue, 0);
    }
    #endregion
    [ContextMenu("EventStageLoad")]
    private void EventStageLoad()
    {
        GameObject.Find("EventStageManager").GetComponent<EventStageManager>().Init(StageDataManager.GetEventData());
    }

    private void SceneUnLoad()
    {

    }
}
