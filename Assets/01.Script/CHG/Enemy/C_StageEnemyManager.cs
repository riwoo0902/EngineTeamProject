using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._01.Script.CHG.Enemy
{
    //스테이지 데이터에 있는 에너미들 랜덤 생성, 남는 애들 UI로, 에너미 죽을 시 남은 자리에 푸쉬

    public class StageEnemyManager : MonoBehaviour
    {
        public Stack<C_EnemyDataSO> CurEnemy = new Stack<C_EnemyDataSO>();
        private C_StageDataSO _stageData;
        //private Transform _playerSpawnPoint; 플레이어 스크립트로 찾기
        private C_Enemy[] EnemySpawnPoints; //나중에 Transform으로 바꿔야할듯
        //생성되는 애들의 EnemyScript에 정보 넣어주기
        public void Init(C_StageDataSO stageData)
        {
            //처음 시작 할 때 Enemy 세팅
            bool flowControl = EnemySetting(stageData);
            if (!flowControl) return;

            //Enemy스크립트에 CurEnemy에 있는 EnemyData넣어주기

        }

        private bool EnemySetting(C_StageDataSO stageData)
        {
            this._stageData = stageData;
            try
            {
                Transform posGroup = GameObject.Find("PosGroup").transform;
                //_playerSpawnPoint = posGroup.GetComponentInChildren<Transform>();  //일단 Enemy 먼저
                EnemySpawnPoints = posGroup.GetComponentsInChildren<C_Enemy>();

                //출현 에너미중 랜덤으로 골라 스테이지 등장 Enemy에 푸쉬
                for (int i = 0; i < _stageData.EmergeCount; i++)
                    CurEnemy.Push(_stageData.EmergeEnemy[Random.Range(0, _stageData.EmergeEnemy.Count)]);

                Console.WriteLine(CurEnemy.Count);

                //씬의 Enemy 스크립트에 EnemyData 넣어주기 + 죽었을 때 이벤트 등록
                for (int i = 0; i < EnemySpawnPoints.Length; i++)
                {
                    if (CurEnemy.Count == 0) break;
                    EnemySpawnPoints[i].Init(CurEnemy.Pop());

                    EnemySpawnPoints[i].OnEnemyDead += EnemyRePlace;
                }
            }
            catch (NullReferenceException n)
            {
                Debug.Log("Enemy 생성 및 적용 실패");
                return false;
            }

            return true;
        }

        //스테이지 데이터 리셋
        public void ClearData()
        {
            _stageData = null;
            EnemySpawnPoints = null;
            Array.Clear(EnemySpawnPoints, 0, EnemySpawnPoints.Length);

            for (int i = 0; i < EnemySpawnPoints.Length; i++)
                EnemySpawnPoints[i].OnEnemyDead -= EnemyRePlace;

        }

        //죽은 Enemy 채워넣기
        private void EnemyRePlace(C_Enemy enemy)
        {
            if (CurEnemy.Count == 0) return;
            enemy.Init(CurEnemy.Pop());
        }
    }
}