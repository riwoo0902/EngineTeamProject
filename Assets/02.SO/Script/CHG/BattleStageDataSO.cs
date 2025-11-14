using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "C_SO/StageDataSO")]
public class BattleStageDataSO : ScriptableObject
{
    [field: SerializeField] public int EmergeCount { get; private set; } //스테이지 등장 Enemy수
    [field: SerializeField] public List<C_EnemyDataSO> EmergeEnemy { get; private set; } //스테이지 등장 Enemy 종류
    //public  PinBallBord; //핀볼칸
}
