using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets._01.Script.CHG.Enemy
{
    //스테이지 데이터에 있는 에너미들 랜덤 생성, 남는 애들 UI로, 에너미 죽을 시 남은 자리에 푸쉬

    public class StageEnemyManager : MonoBehaviour
    {
        [SerializeField] private Image EnemyUIPrefab;
        public Stack<C_EnemyDataSO> StageEnemy = new Stack<C_EnemyDataSO>(); //다음 나올 Enemy Stack

        private C_StageDataSO _stageData;
        //private Transform _playerSpawnPoint; 플레이어 스크립트로 찾기
        private C_Enemy[] sqawnEnemy; //나중에 Transform으로 바꿔야할듯
        private Stack<Image> _nextEnemyUI = new Stack<Image>(); //현재 생성된 UIStack

        //생성되는 애들의 EnemyScript에 정보 넣어주기
        public void Init(C_StageDataSO stageData)
        {
            //처음 시작 할 때 Enemy 세팅
            bool flowControl = EnemySetting(stageData);
            if (!flowControl) return;

            //EnemyUI 생성
            NextEnemyUISetting();

        }


        //Enemy스크립트에 Stagenemy에 있는 EnemyData넣어주기
        private bool EnemySetting(C_StageDataSO stageData)
        {

            this._stageData = stageData;
            try
            {
                Transform posGroup = GameObject.Find("PosGroup").transform;
                //_playerSpawnPoint = posGroup.GetComponentInChildren<Transform>();  //일단 Enemy 먼저
                sqawnEnemy = posGroup.GetComponentsInChildren<C_Enemy>();

                //출현 에너미중 랜덤으로 골라 스테이지 등장 Enemy에 푸쉬
                for (int i = 0; i < _stageData.EmergeCount; i++)
                    StageEnemy.Push(_stageData.EmergeEnemy[Random.Range(0, _stageData.EmergeEnemy.Count)]);


                //씬의 Enemy 스크립트에 EnemyData 넣어주기 + 죽었을 때 이벤트 등록
                for (int i = 0; i < sqawnEnemy.Length; i++)
                {
                    sqawnEnemy[i].Init(StageEnemy.Pop());

                    sqawnEnemy[i].OnEnemyDead += EnemyRePlace;
                }


            }
            catch (NullReferenceException n)
            {
                Debug.LogWarning($"Enemy 생성 및 적용 실패: {n}");
                return false;
            }

            return true;
        }

        private void NextEnemyUISetting()
        {
            Transform NextEnemyGroup = GameObject.Find("NextEnemyGroup").transform;

            // EnemyUI 생성 및 Sprite변경, nextEnemyUiStack에 푸쉬
            foreach (C_EnemyDataSO enemyData in StageEnemy.Reverse())
            {
                Image image = Instantiate(EnemyUIPrefab, NextEnemyGroup);
                image.sprite = enemyData.Sprite;
                _nextEnemyUI.Push(image);
            }
        }


        //죽은 Enemy 채워넣기
        private void EnemyRePlace(C_Enemy enemy)
        {
            //NextEnemy가 있으면 죽은 Enemy에 NextEnemy를 Pop해서 생성, NextEnemyList도 가장 끝 UI를 삭제
            if (StageEnemy.Count == 0) return;
            enemy.Init(StageEnemy.Pop());

            //생성된 EnemyUI 투명화 
            if (_nextEnemyUI.Count > 0)
            {
                Image img = _nextEnemyUI.Pop();
                Color color = img.color;
                color.a = 0f;
                img.color = color;
            }
        }
        //스테이지 데이터 리셋
        public void ClearData()
        {
            _stageData = null;
            sqawnEnemy = null;
            Array.Clear(sqawnEnemy, 0, sqawnEnemy.Length);

            for (int i = 0; i < sqawnEnemy.Length; i++)
                sqawnEnemy[i].OnEnemyDead -= EnemyRePlace;

        }
    }
}