using Assets._01.Script.CHG;
using Unity.VisualScripting;
using UnityEngine;

public class BattleTurnButton : MonoBehaviour
{
    public void PlayerTurnButton()
    {
        if (!C_StageManager.Instance.CurTurn) return;

        PlayerTurnGA playerTurnGA = new();
        ActionSystem.Instance.Perform(playerTurnGA);

    }
}
