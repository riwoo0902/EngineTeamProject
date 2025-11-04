using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._01.Script.CHG
{
    public class C_StageManager : MonoSingleton<C_StageManager>
    {
        [SerializeField] private Image TurnImage;

        [SerializeField]
        private PlayerManager _playerManager;
        private int _curLevel;
        public bool CurTurn { get; private set; } = true; //true일시 플레이어 턴

        //테스트용
        [SerializeField] private C_EnemyStageDataSO[] _stageData;

        #region BattleScene
        [ContextMenu("BattleStageLoad")]
        private void BattleStageLoad()
        {
            Debug.Assert(_stageData != null, "StageData is null!");
            PlayerTurnSet(); //플레이어 턴으로 시작
            GameObject.Find("BattleStageContext").GetComponent<BattleStageContext>().Init(_stageData[0], _playerManager);
        }

        public void EnemyTurnSet()
        {
            CurTurn = false;
            TurnImage.DOColor(Color.red, 0);
            
        }
        public void PlayerTurnSet() 
        {
            CurTurn = true;
            TurnImage.DOColor(Color.blue, 0);
        }
        #endregion


        private void SceneUnLoad()
        {

        }
    }
}