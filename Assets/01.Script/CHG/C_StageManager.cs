using System.Collections.Generic;
using Assets._01.Script.CHG.Enemy;
using UnityEngine;

namespace Assets._01.Script.CHG
{
    public class C_StageManager : MonoBehaviour
    {
        //스테이지가 시작하면 -> 적 생성 ->  적 수 UI 정리 
        //나중에 씬마다 배치할지, 게임 매니저에 붙여둘지에 따라 바꿔야함
        private  int _curLevel;
        [SerializeField] private C_StageDataSO[] _stageData;

        //테스트용
        [ContextMenu("TestSceneLoad")]
        private void SceneLoad()
        {
            Debug.Assert(_stageData != null, "StageData is null!");
            StageEnemyManager enemyManager = GameObject.Find("EnemyManager").GetComponent<StageEnemyManager>();
            enemyManager.Init(_stageData[0]); 
        }
    }
}