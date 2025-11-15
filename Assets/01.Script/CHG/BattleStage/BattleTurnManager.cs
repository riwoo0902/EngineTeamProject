using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnManager : MonoBehaviour
{
    private BattleStageContect _contect;

    public bool CurTurn { get; private set; } = true; //true일시 플레이어 턴
    public int TurnCount { get; private set; } = 1;

    public void Init(BattleStageContect contect)
    {
        _contect = contect;
    }
    public void EnemyTurnSet()
    {
        CurTurn = false;
    }
    public void PlayerTurnSet()
    {
        CurTurn = true;
        TurnCount += 1;
        _contect.UIManager.TurnTextMove(TurnCount);
    }



}
