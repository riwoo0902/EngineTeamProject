using System;
using System.Collections.Generic;
using System.Linq;
using Lrw_PinBall;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BattleStageDatas
{
    public List<BattleStageDataSO> StageData;
}

public class StageDataManager : MonoBehaviour
{
    
    public List<BattleStageDatas> EnemyStageData; //���� �� ��������Data
    public List<C_EventSO> EventData;
    public List<PinBallSO> PinBallData; //���Ǵ� �ɺ�
    public List<ItemSO> ItemData; //���Ǵ� ������
    public BattleStageDatas BossStageData;
    //���� ������ ���� Enemy��ȯ
    public BattleStageDataSO GetBattleData()
    {
        int n;
        if (StageManager.Instance.Level / 4 > 2)
            n = 3;
        else
            n = StageManager.Instance.Level / 4;

            BattleStageDatas stageDatas = EnemyStageData[n];
        int r = Random.Range(0, stageDatas.StageData.Count);
        return stageDatas.StageData[r];
    }

    //�������� ��� ��ȯ, ��ȯ�� Event�� ����
    public C_EventSO GetEventData()
    {
        int r = Random.Range(0, EventData.Count);
        C_EventSO data = EventData[r];
        EventData.RemoveAt(r);
        return data;
    }

    //��ǰ�� �ߺ����� �ʰ� ��ȯ
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

    public BattleStageDataSO GetBossData()
    {
        return BossStageData.StageData[0];
    }
}
