using System;
using System.Collections.Generic;
using Assets._01.Script.CHG.Enemy;
using UnityEngine;

namespace Assets._01.Script.CHG
{
    public class C_StageManager : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private int _curLevel;
        
        //테스트용
        [SerializeField] private C_EnemyStageDataSO[] _stageData;

        private void Awake()
        {
            _playerManager = GetComponentInChildren<PlayerManager>();
        }

        [ContextMenu("BattleStageLoad")]
        private void BattleStageLoad()
        {
            Debug.Assert(_stageData != null, "StageData is null!");
            GameObject.Find("BattleStageContext").GetComponent<BattleStageContext>().Init(_stageData[0], _playerManager);
        }

        private void SceneUnLoad()
        {

        }
    }
}