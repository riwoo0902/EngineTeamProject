using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//스테이지 데이터에 있는 에너미들 랜덤 생성, 남는 애들 UI로, 에너미 죽을 시 남은 자리에 푸쉬
public class BattleEnemyManager : MonoBehaviour
{
    [SerializeField] private Image EnemyUIPrefab;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject EnemyPosGroup;

    private Stack<EnemyDataSO> _nextEnemy = new Stack<EnemyDataSO>(); //다음 나올 Enemy Stack
    private Stack<Image> _nextEnemyUI = new Stack<Image>(); //다음 나올 Enemy UIStack
    private BattleStageDataSO _stageData; //스테이지 정보
    private int _startEnemyCount; //처음에 등장하는 Enemy 수
    private int _lootCoin = 0;
    private BattleStageContect _contect;

    public Dictionary<int, EnemySlot> EnemySlots = new(); //Enemy위치들과 위치에 Enemy존재 여부

    public Player Player { get; private set; }
    private EnemyTurnManager _turnManager;
    private int _enemyKillCount = 0;

    private Queue<Enemy> _deadEnemiesToReplace = new Queue<Enemy>();

    //생성되는 애들의 EnemyScript에 정보 넣어주기, ActionSystem에 EnemyTurn연결
    public void Init(BattleStageDataSO stageData, BattleStageContect contect)
    {
        _contect = contect;
        Player = contect.Player;

        //처음 시작 할 때 Enemy 세팅
        _startEnemyCount = stageData.StartEnemyCount;
        bool flowControl = EnemySetting(stageData);
        if (!flowControl) return;

        _turnManager = contect.EnemyTurnManager;
        _turnManager.Init(this, contect.TurnManager);

        //EnemyUI 생성
        NextEnemyUISetting();
        _enemyKillCount = stageData.EmergeCount - 1;
    }

    //Enemy스크립트에 Stagenemy에 있는 EnemyData넣어주기
    private bool EnemySetting(BattleStageDataSO stageData)
    {
        this._stageData = stageData;
        try
        {
            //출현 에너미중 랜덤으로 골라 스테이지 등장 Enemy에 푸쉬
            for (int i = 0; i < _stageData.EmergeCount; i++)
                _nextEnemy.Push(_stageData.EmergeEnemy[Random.Range(0, _stageData.EmergeEnemy.Count)]);

            foreach (var item in _nextEnemy)
            {
                _lootCoin += item.LootCoin;
            }

            //EnemyPos 위치 가져오기
            Transform[] enemyPos = EnemyPosGroup.GetComponentsInChildren<Transform>()
                .Where(t => t != EnemyPosGroup.transform) //PosGroup 자신은 제외 
                .ToArray();

            //EnemySlots 기본 설정
            for (int i = 0; i < enemyPos.Length; i++)
            {
                EnemySlots[i] = new EnemySlot
                {
                    Pos = enemyPos[i],
                    CurUse = null
                };
            }

            //씬의 Enemy 스크립트에 EnemyData 넣어주기,죽었을 때 이벤트 등록, 현재 생성된 Enemy 목록에 Add
            //첫번째 몬스터가 앞에 오도록
            for (int i = _startEnemyCount; i > 0; i--)
            {
                if (_nextEnemy.Count <= 0) continue;
                var slot = EnemySlots[i - 1];

                GameObject enemyObj = Instantiate(EnemyPrefab, slot.Pos.position, Quaternion.identity);
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                _contect.UIManager.EnemyInfoBarSet(enemyObj, enemy);

                enemy.Init(_nextEnemy.Pop(), _contect); //EnemyData 넣어주기

                slot.CurUse = enemy;
                EnemySlots[i - 1].CurUse = slot.CurUse;

                enemy.OnEnemyDead += EnemyDeadHandler;
            }

        }
        catch (NullReferenceException n)
        {
            Debug.LogWarning($"Enemy 생성 및 적용 실패: {n}");
            return false;
        }

        return true;
    }


    // EnemyUI 생성 및 Sprite변경, nextEnemyUiStack에 푸쉬
    private void NextEnemyUISetting()
    {
        Transform NextEnemyGroup = GameObject.Find("NextEnemyGroup").transform;

        foreach (EnemyDataSO enemyData in _nextEnemy.Reverse())
        {
            Image image = Instantiate(EnemyUIPrefab, NextEnemyGroup);
            image.sprite = enemyData.EnemySprite;
            _nextEnemyUI.Push(image);
        }
    }

   
    private void EnemyDeadHandler(Enemy enemy)
    {
        _deadEnemiesToReplace.Enqueue(enemy);


        var pairEnemy = EnemySlots.FirstOrDefault(fod => fod.Value.CurUse == enemy);
        if (pairEnemy.Value != null)
        {
            EnemySlots[pairEnemy.Key].CurUse = null;
            StageClear();
            

        }
    }


    public IEnumerator HandleReplacementsRoutine()
    {
        while (_deadEnemiesToReplace.Count > 0)
        {
            Enemy enemy = _deadEnemiesToReplace.Dequeue();

            if (_enemyKillCount <= 0)
            {
                StageClear();
                continue;
            }
            _enemyKillCount--;

            if (_nextEnemy.Count == 0) continue;

            EnemySlot targetSlot = null;

            foreach (var slot in EnemySlots)
            {
                if (slot.Value.CurUse == null) // 슬롯이 비어 있다면
                {
                    targetSlot = slot.Value;
                    break;
                }
            }

            if (targetSlot == null) continue;

 
            yield return enemy.EnemyMove(targetSlot);

            targetSlot.CurUse = enemy;
            enemy.Init(_nextEnemy.Pop(), _contect);

            if (_nextEnemyUI.Count > 0)
            {
                Image img = _nextEnemyUI.Pop();
                Color color = img.color;
                color.a = 0f;
                img.color = color;
            }
        }
        
    }

    private void StageClear()
    {
        EnemyDataSO data = _stageData.EmergeEnemy[Random.Range(0, _stageData.EmergeEnemy.Count)];
        _contect.UIManager.StageClear(_lootCoin, data.LootItem, data.LootPinBall);
    }
}