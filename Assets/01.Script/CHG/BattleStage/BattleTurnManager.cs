using System.Collections;
using UnityEngine;

public class BattleTurnManager : MonoBehaviour
{
    private BattleStageContect _contect;

    private bool _curTurn = true;
    public bool CurTurn
    {
        get
        {
            return _curTurn;
        }
        private set
        {
            _curTurn = value;
            
        }
    } //true일시 플레이어 턴

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
        CurTurn = true;
        TurnCount += 1;

    End:;
    }



}
