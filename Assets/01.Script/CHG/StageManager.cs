using System;
using System.Collections;
using _01.Script.CHG;
using Custom.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MapType
{
    None,
    Battle,
    Boss,
    Store,
    Event,
    MapChoice
}

public class StageManager : Custom.MonoSingleton.MonoSingleton<StageManager>
{
    protected override void Awake()
    {
        base.Awake();
        
    }

    public StageDataManager StageDataManager { get; private set; }
    [field: SerializeField] public int Level { get; set; } = 0;

    [SerializeField] private MapType _nextMapType;

    private void Start()
    {
        StageDataManager = GetComponent<StageDataManager>();
        BattleStageLoad();
    }

    public void SceneChange(MapType type)
    {
        _nextMapType = type;

        SceneManager.sceneLoaded += OnSceneLoaded;

        //씬 로드 실행
        switch (_nextMapType)
        {
            case MapType.Battle:
                {
                    SceneManager.LoadScene("GameScene");//여기서부터 다시 테스트
                    break;
                }
            case MapType.Boss:
                {
                    SceneManager.LoadScene("C Boss");//여기서부터 다시 테스트
                    break;
                }
            case MapType.Store:
                {
                    SceneManager.LoadScene("Store");//여기서부터 다시 테스트
                    break;
                }
            case MapType.Event:
                {
                    SceneManager.LoadScene("Event");//여기서부터 다시 테스트
                    break;
                }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //여기서 특정 씬일 때 반응 실행
        switch (_nextMapType)
        {
            case MapType.Battle:
                {
                    Invoke("BattleStageLoad", 0.1f);
                }
                break;
            case MapType.Store:
                {
                    Invoke("StoreStageLoad", 0.1f);
                    StoreStageLoad();
                }
                break;
            case MapType.Event:
                {
                    Invoke("EventStageLoad", 0.1f);
                    EventStageLoad();
                }
                break;
            case MapType.Boss:
                {
                    Invoke("BossStageLoad", 0.1f);
                }
                break;
            default:
                break;
        }
    }
    private IEnumerator SafeLoad<T>(string objName, Action<T> onLoad)
{
    while (true)
    {
        try
        {
            GameObject go = GameObject.Find(objName);
            if (go != null)
            {
                T comp = go.GetComponent<T>();
                if (comp != null)
                {
                    onLoad(comp);
                    yield break;
                }
            }
        }
        catch { /* NullReference 무시하고 재시도 */ }

        yield return null; // 다음 프레임까지 대기 후 재시도
    }
}

    #region SceneLoad
    [ContextMenu("BattleStageLoad")]
private void BattleStageLoad()
{
    StartCoroutine(SafeLoad<BattleStageContect>(
        "BattleStageContext",
        comp => comp.Init(StageDataManager.GetBattleData())));
}

[ContextMenu("BossStageLoad")]
private void BossStageLoad()
{
    StartCoroutine(SafeLoad<BattleStageContect>(
        "BattleStageContext",
        comp => comp.Init(StageDataManager.GetBossData())));
}

[ContextMenu("EventStageLoad")]
private void EventStageLoad()
{
    StartCoroutine(SafeLoad<EventStageManager>(
        "EventStageManager",
        comp => comp.Init(StageDataManager.GetEventData())));
}

[ContextMenu("StoreStageLoad")]
private void StoreStageLoad()
{
    StartCoroutine(SafeLoad<StoreStageManager>(
        "StoreStageManager",
        comp => comp.InIt(StageDataManager.ItemData.ToArray(),
                          StageDataManager.PinBallData.ToArray())));
}
    #endregion
}
