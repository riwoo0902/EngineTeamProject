using System;
using System.Collections.Generic;
using Assets._01.Script.CHG.Enemy;
using UnityEngine;

namespace Assets._01.Script.CHG
{
    public class C_StageManager : MonoSingleton<C_StageManager>
    {
        [SerializeField]
        private PlayerManager _playerManager;
        private int _curLevel;
        public bool CurTurn { get; private set; } = true; //true일시 플레이어 턴

        //테스트용
        [SerializeField] private C_EnemyStageDataSO[] _stageData;

        [ContextMenu("BattleStageLoad")]
        private void BattleStageLoad()
        {
            Debug.Assert(_stageData != null, "StageData is null!");
            CurTurn = true; //플레이어 턴으로 시작
            GameObject.Find("BattleStageContext").GetComponent<BattleStageContext>().Init(_stageData[0], _playerManager);
        }

        public void EnemyTurnSet() => CurTurn = false;
        public void PlayerTurnSet() => CurTurn = true;

        private void SceneUnLoad()
        {

        }
    }
}