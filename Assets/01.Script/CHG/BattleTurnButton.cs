using Assets._01.Script.CHG;
using Unity.VisualScripting;
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
        Debug.Log($"CurTurn: {(C_StageManager.Instance.CurTurn ? "Player" : "Enemy")}");
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA); //EnemyTurn½ÇÇà
    }
}
