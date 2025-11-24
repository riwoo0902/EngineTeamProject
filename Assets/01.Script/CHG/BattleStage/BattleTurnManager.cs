using System.Collections;
using _01.Script.Lrw.PinBallMap;
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
            Debug.Log("Cur" + _curTurn);
        }
    } //true�Ͻ� �÷��̾� ��

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
        Debug.Log("ChangePlayerTurn");
        if (_contect.Player.HealthCompo.CurHp <= 0) goto End;
        bool textMoveEnd = false;

        _contect.UIManager.TurnTextSet(TurnCount, () => textMoveEnd = true);

        yield return new WaitUntil(() => textMoveEnd);

        //yield return new WaitForSeconds(2);
        
        CurTurn = true;
        TurnCount += 1;

    End:;
    }



}
