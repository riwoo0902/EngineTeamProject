using UnityEngine;

public class BattleTurnButton : MonoBehaviour
{
    private BattleTurnManager _turnManager;

    public void Init(BattleTurnManager turnManager)
    {
        _turnManager = turnManager;
    }

    public void PlayerTurnButton()
    {
        if (!_turnManager.CurTurn) return;

        Debug.Log($"CurTurn: {(_turnManager.CurTurn ? "Player" : "Enemy")}");
        PlayerTurnGA playerTurnGA = new();
        ActionSystem.Instance.Perform(playerTurnGA);
    }

    public void EnemyTurnButton()
    {
        if (_turnManager.CurTurn) return;

        Debug.Log($"CurTurn: {(_turnManager.CurTurn ? "Player" : "Enemy")}");
        EnemyMoveGA enemyMoveGA = new();
        ActionSystem.Instance.Perform(enemyMoveGA); //EnemyTurn½ÇÇà

    }
}
