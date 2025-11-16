using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "C_SO/StageDataSO")]
public class BattleStageDataSO : ScriptableObject
{
    public int EmergeCount;
    public List<C_EnemyDataSO> EmergeEnemy;
    //public  PinBallBord; //ÇÉº¼Ä­
}
