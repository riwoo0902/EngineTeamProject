using UnityEngine;

public class BattleTurnButton : MonoBehaviour
{
    public void PlayerTurnButton()
    {
        if (!C_StageManager.Instance.CurTurn) return;

        Debug.Log($"CurTurn: {(C_StageManager.Instance.CurTurn ? "Player" : "Enemy")}");
        PlayerTurnGA playerTurnGA = new();
        ActionSystem.Instance.Perform(playerTurnGA);
    }

    public void EnemyTurnButton()
    {
        if (C_StageManager.Instance.CurTurn) return;

        Debug.Log($"CurTurn: {(C_StageManager.Instance.CurTurn ? "Player" : "Enemy")}");
        EnemyMoveGA enemyMoveGA = new();
        ActionSystem.Instance.Perform(enemyMoveGA); //EnemyTurn½ÇÇà

    }
}
