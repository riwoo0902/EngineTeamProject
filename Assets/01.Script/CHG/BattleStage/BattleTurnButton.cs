using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using UnityEngine;

public class BattleTurnButton : MonoBehaviour
{
    private BattleTurnManager _turnManager;
    private BattleStageContect _contect;
    public void Init(BattleStageContect contect)
    {
        _contect = contect;
        _turnManager = contect.TurnManager;
    }

    public void PlayerTurnButton()
    {
        if (!_turnManager.CurTurn || _contect.Player.PlayerTarget == null) return;
        if(GameManager.Instance.state != PinBallStates.Idle) return;
        //Debug.Log($"CurTurn: {(_turnManager.CurTurn ? "Player" : "Enemy")}");
        //PlayerTurnGA playerTurnGA = new();
        //ActionSystem.Instance.Perform(playerTurnGA);
        _contect.Player.AgentAnimatorCompo.AttackPlay();
    }

    public void EnemyTurnButton()
    {
        if (_turnManager.CurTurn) return;

        Debug.Log($"CurTurn: {(_turnManager.CurTurn ? "Player" : "Enemy")}");
        EnemyMoveGA enemyMoveGA = new();
        ActionSystem.Instance.Perform(enemyMoveGA); //EnemyTurn����

    }
}
