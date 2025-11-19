using System.Collections;
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
        StartCoroutine(ChangePlayerTurn());
        
    }

    private IEnumerator ChangePlayerTurn()
    {
        if (_contect.Player.HealthCompo.CurHp <= 0) goto End;
        bool textMoveEnd = false;

        _contect.UIManager.TurnTextSet(TurnCount, () => textMoveEnd = true);

        yield return new WaitUntil(() => textMoveEnd);
        Debug.Log("Change Turn");
        CurTurn = true;
        TurnCount += 1;

    End:;
    }



}
