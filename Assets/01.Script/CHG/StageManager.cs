using _01.Script.CHG;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MapType
{
    None,
    Battle,
    Boss,
    Store,
    Event,
    MapChoice
}

public class StageManager : MonoSingleton<StageManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public StageDataManager StageDataManager { get; private set; }
    [field: SerializeField] public int Level { get; private set; } = 0;

    [SerializeField] private MapType nextMapType;

    private void Start()
    {
        StageDataManager = GetComponent<StageDataManager>();

    }

    public void SceneChange(MapType type)
    {
        nextMapType = type;
        SceneManager.sceneLoaded += OnSceneLoaded;

        //씬 로드 실행
        switch (type)
        {
            case MapType.Battle:
                {
                    
                }
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //여기서 특정 씬일 때 반응 실행
        switch (nextMapType)
        {
            case MapType.Battle:
                {
                    BattleStageLoad();
                }
                break;
            case MapType.Store:
                {
                    StoreStageLoad();
                }
                break;
            case MapType.Event:
                {
                    EventStageLoad();
                }
                break;
            default:
                break;
        }
    }

    #region SceneLoad
    [ContextMenu("BattleStageLoad")]
    private void BattleStageLoad()
    {
        GameObject.Find("BattleStageContext").GetComponent<BattleStageContect>().Init(StageDataManager.GetBattleData());
    }


    
    [ContextMenu("EventStageLoad")]
    private void EventStageLoad()
    {
        GameObject.Find("EventStageManager").GetComponent<EventStageManager>().Init(StageDataManager.GetEventData());
    }

    [ContextMenu("StoreStageLoad")]
    private void StoreStageLoad()
    {
        GameObject.Find("StoreStageManager").GetComponent<StoreStageManager>().
            InIt(StageDataManager.ItemData.ToArray(), StageDataManager.PinBallData.ToArray());
    }

    private void BossSceneLoad()
    {
        //gameObject
    }
    #endregion
}
