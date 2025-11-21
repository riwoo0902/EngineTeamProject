using System;
using System.Collections.Generic;
using System.Linq;
using _01.Script.Lrw.Inventory;
using Lrw_PinBall;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BattleStageDatas
{
    public List<BattleStageDataSO> EnemyData;
}

public class StageDataManager : MonoBehaviour
{
    
    public List<BattleStageDatas> EnemyStageData; //레벨 당 스테이지Data
    public List<C_EventSO> EventData;
    public List<PinBallSO> PinBallData;
    public List<ItemSO> ItemData;

    //현재 레벨에 맞춰 Enemy반환
    public BattleStageDataSO GetBattleData()
    {
        BattleStageDatas stageDatas = EnemyStageData[StageManager.Instance.Level];
        int r = Random.Range(0, stageDatas.EnemyData.Count);
        return stageDatas.EnemyData[r];
    }

    //랜덤으로 골라서 반환, 반환한 Event는 삭제
    public C_EventSO GetEventData()
    {
        int r = Random.Range(0, EventData.Count);
        C_EventSO data = EventData[r];
        EventData.RemoveAt(r);
        return data;
    }

    //상품이 중복되지 않게 반환
    public ItemSO[] GetItemData(int n)
    {
        return ItemData
        .OrderBy(item => Random.value)
        .Take(n)
        .ToArray();
    }


    public PinBallSO[] GetPinBallData(int n)
    {
        List<PinBallSO> pinBalls = new List<PinBallSO>();
        while (pinBalls.Count < n)

        {
            int r = Random.Range(0, PinBallData.Count);
            if (pinBalls.Contains(PinBallData[r]))
            {
                pinBalls.Add(PinBallData[r]);
            }
        }

        return pinBalls.ToArray();
    }
}
