using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BattleStageDatas
{
    public List<BattleStageDataSO> EnemyData; 
}

public class StageDataManager : MonoBehaviour
{
    private int _level = 0;
    public List<BattleStageDatas> EnemyStageData; //레벨 당 스테이지Data
    public List<C_EventSO> EventData;

    //현재 레벨에 
    public BattleStageDataSO GetBattleData()
    {
        BattleStageDatas stageDatas = EnemyStageData[_level];
        int r = Random.Range(0,stageDatas.EnemyData.Count);
        return stageDatas.EnemyData[r];
    }

    //랜덤으로 골라서 반환, 반환한 Event는 삭제
    public C_EventSO GetEventData()
    {
        int r = Random.Range(0,EventData.Count);
        C_EventSO data = EventData[r];
        EventData.RemoveAt(r);
        return data;
    }
}
