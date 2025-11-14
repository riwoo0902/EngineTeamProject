using System;
using System.Collections.Generic;
using System.Linq;
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
    public List<PinBallSO> pinBallData;
    public List<ItemSO> ItemData;

    //현재 레벨에 맞춰 Enemy반환
    public BattleStageDataSO GetBattleData()
    {
        BattleStageDatas stageDatas = EnemyStageData[C_StageManager.Instance.Level];
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
        if (n >= ItemData.Count)
        {
            //모든 아이템을 순서 무작위로 해서 반환
            return ItemData.OrderBy(item => Guid.NewGuid()).ToArray();
        }

        //랜덤으로 배열 후 n개만큼 가져오기
        ItemSO[] items = ItemData.OrderBy(item => Guid.NewGuid())
                                 .Take(n)
                                 .ToArray();
        return items;
    }


    public PinBallSO[] GetPinBallData(int n)
    {
        List<PinBallSO> pinBalls = new List<PinBallSO>();
        while (pinBalls.Count < n)
        {
            int r = Random.Range(0, pinBallData.Count);
            if (pinBalls.Contains(pinBallData[r]))
            {
                pinBalls.Add(pinBallData[r]);
            }
        }

        return pinBalls.ToArray();
    }
}
